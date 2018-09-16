echo off

rem batch file to run a script to create a db
rem 1/19/2018

sqlcmd -S localhost -E -i scripts/sample_data.sql

ECHO .
ECHO if no error messages appear DB was created
PAUSE