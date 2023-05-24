FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "TT.Service/TT.Service.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "TT.Service/TT.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TT.Service/TT.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TT.Service.dll"]
