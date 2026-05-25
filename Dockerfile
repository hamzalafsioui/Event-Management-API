# Use the official .NET 8 SDK as build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution file and restore dependencies
COPY ["Event Management API.sln", "./"]
COPY ["EventManagement.API/EventManagement.API.csproj", "EventManagement.API/"]
COPY ["EventManagement.Core/EventManagement.Core.csproj", "EventManagement.Core/"]
COPY ["EventManagement.Data/EventManagement.Data.csproj", "EventManagement.Data/"]
COPY ["EventManagement.Infrustructure/EventManagement.Infrustructure.csproj", "EventManagement.Infrustructure/"]
COPY ["EventManagement.Service/EventManagement.Service.csproj", "EventManagement.Service/"]
COPY ["EventManagement.Tests/EventManagement.Tests.csproj", "EventManagement.Tests/"]

RUN dotnet restore "Event Management API.sln"

# Copy the rest of the code and build
COPY . .
WORKDIR "/app/EventManagement.API"
RUN dotnet build "EventManagement.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "EventManagement.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the ASP.NET 8 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose ports
EXPOSE 8080

# Run the application
ENTRYPOINT ["dotnet", "EventManagement.API.dll"]
