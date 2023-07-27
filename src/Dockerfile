FROM mcr.microsoft.com/dotnet/samples:aspnetapp AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

ENV NUGET_PACKAGES_TIMEOUT=300000

WORKDIR /src

COPY AthenasAcademy.Services.API/AthenasAcademy.Services.API.csproj AthenasAcademy.Services.API/
COPY AthenasAcademy.Services.Core/AthenasAcademy.Services.Core.csproj AthenasAcademy.Services.Core/
COPY AthenasAcademy.Services.Domain/AthenasAcademy.Services.Domain.csproj AthenasAcademy.Services.Domain/

RUN dotnet restore AthenasAcademy.Services.API/AthenasAcademy.Services.API.csproj

COPY . .

WORKDIR /src/AthenasAcademy.Services.API

RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish AthenasAcademy.Services.API.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "AthenasAcademy.Services.API.dll"]