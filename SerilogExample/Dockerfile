#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:5.0-buster AS build
WORKDIR /src
COPY SerilogExample/SerilogExample.csproj SerilogExample/
RUN dotnet restore "SerilogExample/SerilogExample.csproj"
COPY . .
WORKDIR "/src/SerilogExample"
RUN dotnet build "SerilogExample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SerilogExample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SerilogExample.dll"]
