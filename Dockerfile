FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY ./ShiftCodeRedeemer/ShiftCodeRedeemer.csproj ./ShiftCodeRedeemer/
COPY ./ShiftCodeRedeemer.Tests/ShiftCodeRedeemer.Tests.csproj ./ShiftCodeRedeemer.Tests/
RUN dotnet restore

# copy and publish app and libraries
COPY . .
RUN dotnet publish -c release -o /app --no-restore

FROM build as tests
ENTRYPOINT [ "dotnet", "test" ]

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ShiftCodeRedeemer.dll"]