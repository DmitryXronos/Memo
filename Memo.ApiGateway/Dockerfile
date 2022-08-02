FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY Memo.ApiGateway.csproj Memo.ApiGateway.csproj
RUN dotnet restore Memo.ApiGateway.csproj
COPY . .
WORKDIR /src
RUN dotnet build Memo.ApiGateway.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Memo.ApiGateway.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Memo.ApiGateway.dll"]
