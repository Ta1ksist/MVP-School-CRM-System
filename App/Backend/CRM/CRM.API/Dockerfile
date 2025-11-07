FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Backend/CRM/CRM.API/CRM.API.csproj CRM.API/
COPY Backend/CRM/CRM.Application/CRM.Application.csproj CRM.Application/
COPY Backend/CRM/CRM.Core/CRM.Core.csproj CRM.Core/
COPY Backend/CRM/CRM.DataAccess/CRM.DataAccess.csproj CRM.DataAccess/

RUN dotnet restore CRM.API/CRM.API.csproj

COPY Backend/CRM/ ./
WORKDIR /src/CRM.API
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "CRM.API.dll"]
