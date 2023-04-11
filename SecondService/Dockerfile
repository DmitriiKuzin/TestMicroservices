FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SecondService/SecondService.csproj", "SecondService/"]
COPY ["Commands/Commands.csproj", "Commands/"]
COPY ["DAL/DAL.csproj", "DAL/"]
COPY ["Model/Model.csproj", "Model/"]
RUN dotnet restore "SecondService/SecondService.csproj"
COPY . .
WORKDIR "/src/SecondService"
RUN dotnet build "SecondService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SecondService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecondService.dll"]
