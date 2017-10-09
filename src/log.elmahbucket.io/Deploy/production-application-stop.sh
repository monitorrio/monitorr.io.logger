#!/usr/bin/env bash

if ls /home/bitnami/.pm2/pids/production-log*.pid 1> /dev/null 2>&1;
then
   pm2 delete production-log
fi