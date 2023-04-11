FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FirstService/FirstService.csproj", "FirstService/"]
COPY ["Commands/Commands.csproj", "Commands/"]
RUN dotnet restore "FirstService/FirstService.csproj"
COPY . .
WORKDIR "/src/FirstService"
RUN dotnet build "FirstService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FirstService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FirstService.dll"]
