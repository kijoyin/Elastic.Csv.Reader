#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Elastic.Csv.Reader/Elastic.Csv.Reader.csproj", "Elastic.Csv.Reader/"]
RUN dotnet restore "Elastic.Csv.Reader/Elastic.Csv.Reader.csproj"
COPY . .
WORKDIR "/src/Elastic.Csv.Reader"
RUN dotnet build "Elastic.Csv.Reader.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Elastic.Csv.Reader.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Elastic.Csv.Reader.dll"]