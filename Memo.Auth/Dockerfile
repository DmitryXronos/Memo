﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Memo.Auth/Memo.Auth.csproj", "Memo.Auth/"]
RUN dotnet restore "Memo.Auth/Memo.Auth.csproj"
COPY . .
WORKDIR "/src/Memo.Auth"
RUN dotnet build "Memo.Auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Memo.Auth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Memo.Auth.dll"]