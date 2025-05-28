#!/bin/bash
set -e

# Default to 8080 if not set
PORT=${APP_PORT:-8080}
exec dotnet TKILSAPRFC.API.dll --urls "http://0.0.0.0:${PORT}"
