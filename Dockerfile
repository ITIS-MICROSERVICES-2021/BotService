FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

EXPOSE 80

COPY BotService/*.csproj ./BotService/
COPY BotService.Tests/*.csproj ./BotService.Tests/

WORKDIR /app/BotService
RUN dotnet restore

WORKDIR /app

COPY BotService/*.csproj ./BotService/
COPY BotService.Tests/*.csproj ./BotService.Tests/

WORKDIR /app/BotService
RUN dotnet publish /property:PublishWithAspNetCoreTargetManifest=false	 -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

COPY --from=build-env /app/BotService/out ./
ENTRYPOINT ["dotnet", "BotService.dll"]