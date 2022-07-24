FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Memo.Notes/Memo.Notes.csproj", "Memo.Notes/"]
RUN dotnet restore "Memo.Notes/Memo.Notes.csproj"
COPY . .
WORKDIR "/src/Memo.Notes"
RUN dotnet build "Memo.Notes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Memo.Notes.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Memo.Notes.dll"]
