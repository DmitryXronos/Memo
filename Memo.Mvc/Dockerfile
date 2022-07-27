FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Memo.Mvc/Memo.Mvc.csproj", "Memo.Mvc/"]
RUN dotnet restore "Memo.Mvc/Memo.Mvc.csproj"
COPY . .
WORKDIR "/src/Memo.Mvc"
RUN dotnet build "Memo.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Memo.Mvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Memo.Mvc.dll"]
