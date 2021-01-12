FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

RUN curl --silent --location https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install --yes nodejs

WORKDIR /app
COPY *.sln .
COPY URLShortener/*.csproj ./URLShortener/
COPY URLShortener.Lib/*.csproj ./URLShortener.Lib/
COPY URLShortener.Tests/*.csproj ./URLShortener.Tests/

WORKDIR /app/URLShortener
RUN dotnet restore

WORKDIR /app

COPY URLShortener/. ./URLShortener/
COPY URLShortener.Lib/. ./URLShortener.Lib/
COPY URLShortener.Tests/. ./URLShortener.Tests/

WORKDIR /app/URLShortener
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS runtime

COPY --from=build /app/URLShortener/out ./
ENTRYPOINT ["dotnet", "URLShortener.dll"]
