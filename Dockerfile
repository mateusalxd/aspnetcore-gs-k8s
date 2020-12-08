FROM mcr.microsoft.com/dotnet/sdk:3.1-buster as build
WORKDIR /app
COPY worker.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:3.1-buster-slim
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT [ "dotnet", "worker.dll" ]