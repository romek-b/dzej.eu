FROM node:alpine as build
WORKDIR /app
COPY . .
RUN npm ci && npm run build -- --prod

FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY /nginx /etc/nginx/conf.d
COPY --from=build /app/dist/dzej-eu-portal .
ENTRYPOINT ["nginx", "-g", "daemon off;"]