
# Obtendo o source e gerando o build

FROM node:latest AS ng-builder
RUN mkdir -p /app
WORKDIR /app
#COPY frontend/package.json /app
COPY src/Frontend.Web/ .
RUN npm install
COPY . /app
RUN npm run build

# Subir o source para um servidor NGINX com a App Angular

FROM nginx
COPY src/Frontend.Web/nginx.conf /etc/nginx/nginx.conf
COPY --from=ng-builder /app/dist/frontend.web /usr/share/nginx/html

EXPOSE 80
