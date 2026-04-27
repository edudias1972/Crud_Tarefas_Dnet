FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o projeto e restaura
COPY CrudTarefas.csproj ./
RUN dotnet restore

# Copia todo o código da raiz (Controllers, Data, Service, Program.cs...)
COPY . .

# Publica
RUN dotnet publish "CrudTarefas.csproj" -c Release -o out

# Estágio de Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

ENTRYPOINT ["dotnet", "CrudTarefas.dll"]
