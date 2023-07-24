# Etapa 1: Construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:latest AS build
WORKDIR /src

COPY AthenasAcademy.Services.API/AthenasAcademy.Services.API.csproj AthenasAcademy.Services.API/
COPY AthenasAcademy.Services.Core/AthenasAcademy.Services.Core.csproj AthenasAcademy.Services.Core/
COPY AthenasAcademy.Services.Domain/AthenasAcademy.Services.Domain.csproj AthenasAcademy.Services.Domain/
COPY AthenasAcademy.Services.Handling/AthenasAcademy.Services.Handling.csproj AthenasAcademy.Services.Handling/
COPY AthenasAcademy.Services.Test/AthenasAcademy.Services.Test.csproj AthenasAcademy.Services.Test/

RUN dotnet restore AthenasAcademy.Services.API/AthenasAcademy.Services.API.csproj
RUN dotnet restore AthenasAcademy.Services.Core/AthenasAcademy.Services.Core.csproj
RUN dotnet restore AthenasAcademy.Services.Domain/AthenasAcademy.Services.Domain.csproj
RUN dotnet restore AthenasAcademy.Services.Handling/AthenasAcademy.Services.Handling.csproj
RUN dotnet restore AthenasAcademy.Services.Test/AthenasAcademy.Services.Test.csproj

COPY . .

RUN dotnet publish AthenasAcademy.Services.API/AthenasAcademy.Services.API.csproj -c Release -o /app/AthenasAcademy.Services.API
RUN dotnet publish AthenasAcademy.Services.Core/AthenasAcademy.Services.Core.csproj -c Release -o /app/AthenasAcademy.Services.Core
RUN dotnet publish AthenasAcademy.Services.Domain/AthenasAcademy.Services.Domain.csproj -c Release -o /app/AthenasAcademy.Services.Domain
RUN dotnet publish AthenasAcademy.Services.Handling/AthenasAcademy.Services.Handling.csproj -c Release -o /app/AthenasAcademy.Services.Handling
RUN dotnet publish AthenasAcademy.Services.Test/AthenasAcademy.Services.Test.csproj -c Release -o /app/AthenasAcademy.Services.Test

FROM mcr.microsoft.com/dotnet/aspnet:latest AS final

COPY --from=build /app/AthenasAcademy.Services.API /app/AthenasAcademy.Services.API
COPY --from=build /app/AthenasAcademy.Services.Core /app/AthenasAcademy.Services.Core
COPY --from=build /app/AthenasAcademy.Services.Domain /app/AthenasAcademy.Services.Domain
COPY --from=build /app/AthenasAcademy.Services.Handling /app/AthenasAcademy.Services.Handling
COPY --from=build /app/AthenasAcademy.Services.Test /app/AthenasAcademy.Services.Test

#ENV SECRET_FILE /app/AthenasAcademy.Services.API/Secrets.yaml

EXPOSE 5000
EXPOSE 80

WORKDIR /app/AthenasAcademy.Services.API

ENTRYPOINT ["dotnet", "AthenasAcademy.Services.API.dll"]
