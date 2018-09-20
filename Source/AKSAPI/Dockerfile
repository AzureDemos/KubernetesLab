FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY AKSAPI/AKSAPI.csproj AKSAPI/
RUN dotnet restore AKSAPI/AKSAPI.csproj
COPY . .
WORKDIR /src/AKSAPI
RUN dotnet build AKSAPI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish AKSAPI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AKSAPI.dll"]
