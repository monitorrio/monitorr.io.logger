#!/usr/bin/env bash
ASPNETCORE_ENVIRONMENT=Production ASPNETCORE_URLS="http://0.0.0.0:5000" pm2 start --name "production-log" dotnet -- /home/bitnami/apps/monitorr.logger/monitorr.logger.dll 

pm2 save