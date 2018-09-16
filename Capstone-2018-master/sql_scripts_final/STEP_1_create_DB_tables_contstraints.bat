echo off

rem batch file to run a script to create a db
rem 1/19/2018

sqlcmd -S localhost -E -i scripts/create_db_tables.sql
sqlcmd -S localhost -E -i scripts/create_tableconstraints.sql

ECHO .
ECHO if no error messages appear DB was created
PAUSE