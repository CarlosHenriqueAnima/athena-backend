FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src

COPY AthenasAcademy.Services.sln ./

COPY AthenasAcademy.Services.API/AthenasAcademy.Services.API.csproj ./src

COPY . .

RUN dotnet restore

RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final

WORKDIR /app

COPY --from=build /app .

EXPOSE 80

ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "AthenasAcademy.Services.API.dll"]    