FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 47001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY Memo.Auth.csproj Memo.Auth.csproj
RUN dotnet restore Memo.Auth.csproj
COPY . .
WORKDIR /src
RUN dotnet build Memo.Auth.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Memo.Auth.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Memo.Auth.dll"]
