
# Get source and generate build

FROM node:latest AS ng-builder
RUN mkdir -p /app
WORKDIR /app
COPY src/Frontend.Web/ .
RUN npm install
COPY . /app
RUN npm run build

# Publish source on NGINX with an App Angular

FROM nginx
COPY src/Frontend.Web/nginx.conf /etc/nginx/nginx.conf
COPY --from=ng-builder /app/dist/frontend.web /usr/share/nginx/html

EXPOSE 80
