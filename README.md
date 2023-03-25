docker-compose -f stack.yml up
docker-compose -f stack.yml down


Development hist:
	dotnet ef migrations add InitialIdentityMigration --context AppDbContext -o .\Db\Migrations\Identity\
	dotnet ef migrations add InitialConfiguration --context ConfigurationDbContext -o .\Migrations\IdentityServer\Configuration\
	dotnet ef migrations add InitialPersistedGrant --context PersistedGrantDbContext -o .\Migrations\IdentityServer\PersistedGrant\
	
	local .env:
		Data Source=127.0.0.1,1401;Database=master;Trusted_Connection=False;TrustServerCertificate=True;MultipleActiveResultSets=true;User Id=sa;Password=yourStrong(!)Password