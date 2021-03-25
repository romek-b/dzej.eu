FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src

COPY "src/DzejEu.Api.csproj" ./
RUN dotnet restore "DzejEu.Api.csproj"

COPY ./src .
RUN dotnet build "DzejEu.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DzejEu.Api.csproj" -c Release -o /app

FROM base as final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DzejEu.Api.dll"]