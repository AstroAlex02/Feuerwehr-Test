dotnet tool update --global dotnet-ef
dotnet tool update dotnet-ef
set ASPNETCORE_ENVIRONMENT='Production'
dotnet ef dbcontext scaffold Name=feuerwehr Microsoft.EntityFrameworkCore.SqlServer -o Data --no-pluralize --no-build --use-database-names --force --context ffwDb