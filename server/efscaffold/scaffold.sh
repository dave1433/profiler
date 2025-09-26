#!/bin/bash
set -a
source .env
set +a

dotnet tool install -g dotnet-ef
dotnet ef dbcontext scaffold "$CONN_STR" Npgsql.EntityFrameworkCore.PostgreSQL \
--output-dir ./Entities \
--context-dir . \
--context ProfilerDbContext \
 --no-onconfiguring \
 --namespace efscaffold.Entities \
 --context-namespace Infrastructure.Postgre.Scaffolding \
 --schema profilersystem \
 --force
