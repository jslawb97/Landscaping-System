echo off

rem batch file to run a script to create a db
rem 1/19/2018

sqlcmd -S crlandscaping.cvyxh3elzzol.us-east-1.rds.amazonaws.com,1723 -U fearlesspixel -P Passw0rd -i ../scripts/equipment_SPROCS.sql
sqlcmd -S crlandscaping.cvyxh3elzzol.us-east-1.rds.amazonaws.com,1723 -U fearlesspixel -P Passw0rd -i ../scripts/supply_SPROCS.sql
sqlcmd -S crlandscaping.cvyxh3elzzol.us-east-1.rds.amazonaws.com,1723 -U fearlesspixel -P Passw0rd -i ../scripts/employee_SPROCS.sql
sqlcmd -S crlandscaping.cvyxh3elzzol.us-east-1.rds.amazonaws.com,1723 -U fearlesspixel -P Passw0rd -i ../scripts/job_SPROCS.sql

ECHO .
ECHO if no error messages appear DB was created
PAUSE