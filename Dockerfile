FROM mcr.microsoft.com/dotnet/sdk:5.0 AS development
WORKDIR /app

# Copy everything
COPY . ./

# Explicitly ask for path to be added
ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet tool install --global dotnet-ef --version 5.0.17

ENTRYPOINT dotnet watch run --project /app/GloboTicket.TicketManagement.Api --urls=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

# Copy everything
COPY . ./

# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS deployment
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "GloboTicket.TicketManagement.Api.dll"]