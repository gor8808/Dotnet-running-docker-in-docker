FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# "install" the dotnet 7 runtime
COPY --from=mcr.microsoft.com/dotnet/sdk:7.0 /usr/share/dotnet/shared /usr/share/dotnet/shared


WORKDIR /src