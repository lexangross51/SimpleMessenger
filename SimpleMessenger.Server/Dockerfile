FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./SimpleMessenger.WebServer/SimpleMessenger.WebServer.csproj", "SimpleMessenger.WebServer/"]
COPY ["./SimpleMessenger.Logic/SimpleMessenger.Logic.csproj", "SimpleMessenger.Logic/"]
COPY ["./SimpleMessenger.DataAccess/SimpleMessenger.DataAccess.csproj", "SimpleMessenger.DataAccess/"]
RUN dotnet restore "./SimpleMessenger.WebServer/SimpleMessenger.WebServer.csproj"
COPY . .
WORKDIR "/src/SimpleMessenger.WebServer"
RUN dotnet publish "./SimpleMessenger.WebServer.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SimpleMessenger.WebServer.dll"]