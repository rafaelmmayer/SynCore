FROM node:lts-alpine3.19 as build-vue
WORKDIR /app
COPY src/SynCore.Vue/package*.json .
RUN npm install
COPY src/SynCore.Vue/ .
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-api
WORKDIR /app
COPY SynCore.sln .
COPY src/SynCore.Api/ ./src/SynCore.Api/
COPY src/SynCore.Core/ ./src/SynCore.Core/
RUN dotnet restore
RUN dotnet publish -c Relase -o dist/ ./src/SynCore.Api/

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-api /app/dist/ .
COPY --from=build-vue /app/dist/ ./wwwroot/

EXPOSE 80

CMD [ "dotnet", "SynCore.Api.dll" ]