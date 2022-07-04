SET SQLCMD=sqlcmd -S .\SQLEXPRESS -d NannyDB
for %%d in (*.sql) do %SQLCMD% -i%%d
for %%d in (.\DataScripts\*.sql) do %SQLCMD% -i%%d
for %%d in (.\StoredProcedures\*.sql) do %SQLCMD% -i%%d
pause