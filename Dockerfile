# ------------ Build Stage ------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY TKILSAPRFC.API/TKILSAPRFC.API.csproj TKILSAPRFC.API/
COPY TKILSAPRFC.Infrastructure/TKILSAPRFC.Infrastructure.csproj TKILSAPRFC.Infrastructure/
COPY TKILSAPRFC.Core/TKILSAPRFC.Core.csproj TKILSAPRFC.Core/
COPY NwRfcNet/NwRfcNet.csproj NwRfcNet/
RUN dotnet restore TKILSAPRFC.API/TKILSAPRFC.API.csproj

# Copy entire project and publish
COPY . .
RUN dotnet publish TKILSAPRFC.API/TKILSAPRFC.API.csproj -c Release -o /app/out

# ------------ Runtime Stage ------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Install SAP SDK dependencies
RUN apt-get update && apt-get install -y \
    libuuid1 \
    libssl-dev \
    libstdc++6 \
    libnsl-dev \
    unzip \
    && rm -rf /var/lib/apt/lists/*

# Copy published app
COPY --from=build /app/out ./

# Copy SAP & ICU .so libraries from actual project structure
COPY TKILSAPRFC.Infrastructure/nwrfcsdk/lib/libsapnwrfc.so /usr/lib/
COPY TKILSAPRFC.Infrastructure/nwrfcsdk/lib/libsapucum.so /usr/lib/
COPY TKILSAPRFC.Infrastructure/nwrfcsdk/lib/libicuuc.so.34 /usr/lib/
COPY TKILSAPRFC.Infrastructure/nwrfcsdk/lib/libicui18n.so.34 /usr/lib/
COPY TKILSAPRFC.Infrastructure/nwrfcsdk/lib/libicudata.so.34 /usr/lib/


# Export LD_LIBRARY_PATH
ENV LD_LIBRARY_PATH="/usr/lib"

EXPOSE 80
ENTRYPOINT ["dotnet", "TKILSAPRFC.API.dll", "--urls", "http://0.0.0.0:80"]
