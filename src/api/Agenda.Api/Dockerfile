#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY ["Agenda.Api.csproj", "./"]
COPY ["../Agenda.Application/Agenda.Application.csproj", "./Agenda.Application"]
COPY ["../Agenda.Domain/Agenda.Domain.csproj", "./Agenda.Domain"]
COPY ["../Agenda.Infrastructure/Agenda.Infrastructure.csproj", "./Agenda.Infrastructure"]
COPY ["../../common/Agenda.Common/Agenda.Common.csproj", "../../common/Agenda.Common"]
RUN dotnet restore "./api/Agenda.Api/Agenda.Api.csproj"
COPY . .
#WORKDIR "/src/api/Agenda.Api"
#RUN dotnet build "./Agenda.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Agenda.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Agenda.Api.dll"]