using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DzejEu.Api.Clients.Twitch;
using DzejEu.Api.Models;
using DzejEu.Api.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DzejEu.Api.Services
{
    public class UpdateStreamsService : IHostedService
    {
        private readonly IStreamsRepository _repository;
        private readonly ITwitchClient _twitch;
        private readonly IOptions<AppSettings> _settings;
        private readonly ILogger<UpdateStreamsService> _logger;

        private string _streamerName;

        private Timer _timer;

        public UpdateStreamsService(IStreamsRepository repository, ITwitchClient twitch,
            IOptions<AppSettings> settings, ILogger<UpdateStreamsService> logger)
        {
            _repository = repository;
            _twitch = twitch;
            _settings = settings;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _streamerName = _settings.Value.StreamerName;
            _timer = new Timer(async o => await UpdateStreams(),
                null, TimeSpan.Zero, TimeSpan.FromSeconds(_settings.Value.UpdateFrequency));
            _logger.LogInformation($"{nameof(UpdateStreamsService)} started");
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            _logger.LogInformation($"{nameof(UpdateStreamsService)} stopped");
        }

        public async Task UpdateStreams()
        {
            var newestSavedStream = await _repository.GetNewest(_streamerName);
            var currentTwitchStreams = await _twitch.GetStreams(_streamerName);
            var currentTwitchStream = currentTwitchStreams?.Data?.FirstOrDefault();

            if(newestSavedStream is { } && currentTwitchStream is { })
            {
                if(newestSavedStream.TwitchId != currentTwitchStream.Id
                    || newestSavedStream.GameName != currentTwitchStream.GameName)
                {
                    await _repository.MarkAsFinished(newestSavedStream.Id);
                    await AddStreamToDatabase(currentTwitchStream);
                    return;
                }
                _logger.LogInformation(
                    $"Stream {newestSavedStream.Id} is ongoing.");
                return;
            }

            if(newestSavedStream is { } && currentTwitchStream is null)
            {
                if(newestSavedStream.IsOngoing)
                {
                    await MarkStreamAsFinished(newestSavedStream);
                    return;
                }
                _logger.LogInformation($"No new streams found");
                return;
            }

            if(newestSavedStream is null && currentTwitchStream is { })
            {
                await AddStreamToDatabase(currentTwitchStream);
                return;
            }
            
            _logger.LogInformation($"No streams registered");
        }

        private async Task AddStreamToDatabase(Models.Twitch.Stream currentTwitchStream)
        {
            var streamToSave = new Models.Database.Stream
            {
                TwitchId = currentTwitchStream.Id,
                UserName = currentTwitchStream.UserName,
                GameName = currentTwitchStream.GameName,
                StartedAt = DateTime.Now,
                IsOngoing = true,
            };
            await _repository.Add(streamToSave);
            _logger.LogInformation(
                $"Added new {currentTwitchStream.GameName} stream");
        }

        private async Task MarkStreamAsFinished(Models.Database.Stream stream)
        {
            await _repository.MarkAsFinished(stream.Id);
            _logger.LogInformation($"Marked stream {stream.Id} as finished");
        }
    }
}