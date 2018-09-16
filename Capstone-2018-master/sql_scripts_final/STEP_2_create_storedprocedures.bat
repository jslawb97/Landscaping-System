echo off

rem batch file to run a script to create a db
rem 1/19/2018

sqlcmd -S localhost -E -i scripts/equipment_SPROCS.sql
sqlcmd -S localhost -E -i scripts/supply_SPROCS.sql
sqlcmd -S localhost -E -i scripts/employee_SPROCS.sql
sqlcmd -S localhost -E -i scripts/job_SPROCS.sql

ECHO .
ECHO if no error messages appear DB was created
PAUSE