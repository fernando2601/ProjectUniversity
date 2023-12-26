# Use the official .NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official .NET Core SDK as a base image for building
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["ProjectUniversity/ProjectUniversity.csproj", "ProjectUniversity/"]
RUN dotnet restore "ProjectUniversity/ProjectUniversity.csproj"
COPY . .
WORKDIR "/src/ProjectUniversity"
RUN dotnet build "ProjectUniversity.csproj" -c Release -o /app/build

# Use the build image to publish the application
FROM build AS publish
RUN dotnet publish "ProjectUniversity.csproj" -c Release -o /app/publish

# Use the official .NET Core runtime as a base image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectUniversity.dll"]
