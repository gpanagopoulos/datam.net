# datam.net

How to start:
1. Edit App.config of Datam.Host
  - ConnectionString
  - DBType (currently only SQLServer supported)
  - PatchesFolder to read migrations from, PatchesRegex to match migration script names
2. Create the Database in SQL Server (datam will not create it for you)
3. Run Datam.Host.exe with params
  - -o Migrate or -o Options
