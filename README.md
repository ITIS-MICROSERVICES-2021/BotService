# ITIS_2021_2_BotService
Telegram bot - @itis_microservices_2021_bot

## Getting started

### Prerequisites

* .NET 5 [download](https://dotnet.microsoft.com/download/dotnet/5.0)

### Running

#### Running with CLI

Run the following command in your solutiuon (`BotService.sln`) directory:

```
dotnet run ./BadSmellingBotServiceUsingCSharp/BadSmellingBotServiceUsingCSharp.csproj
```

#### Running with Visual Studio/Rider

1. Open `BotService.sln` with your preferred IDE
2. Set startup project in your build configuration to `BadSmellingBotServiceUsingCSharp.csproj`
3. Run the solution

### Configuration

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "Telegram" : {
    "Token" : "2018371884:AAEJbN3m1_NhaMP65Gv6ayUF_Lc3y2bdxvY"
  }
}
```

* `Telegram.Token`: telegram bot authentication token


You can change the token to connect to your own telegram bot.
