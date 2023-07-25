FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src

COPY /src/AthenasAcademy.Services.API/AthenasAcademy.Services.API.csproj AthenasAcademy.Services.API/
RUN dotnet restore AthenasAcademy.Services.API/AthenasAcademy.Services.API.csproj

COPY . .

RUN dotnet publish /src/AthenasAcademy.Services.API/AthenasAcademy.Services.API.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final

WORKDIR /app

COPY --from=build /app .

EXPOSE 80

ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "AthenasAcademy.Services.API.dll"]    