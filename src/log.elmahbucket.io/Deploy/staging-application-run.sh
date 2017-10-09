#!/usr/bin/env bash
ASPNETCORE_ENVIRONMENT=Staging ASPNETCORE_URLS="http://0.0.0.0:5001" pm2 start --name "staging-log" dotnet -- /home/bitnami/apps/staging-log.elmahbucket.io/log.elmahbucket.io.dll 

pm2 save