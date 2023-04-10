docker-compose -f stack.yml up
docker-compose -f stack.yml down
https://medium.com/@richardr39/using-angular-cli-to-serve-over-https-locally-70dab07417c8
openssl req -new -x509 -newkey rsa:2048 -sha256 -nodes -keyout localhost.key -days 3560 -out localhost.crt -config certificate.cnf

Development hist:
	dotnet ef migrations add InitialIdentityMigration --context AppDbContext -o .\Db\Migrations\Identity\
	dotnet ef migrations add InitialConfiguration --context ConfigurationDbContext -o .\Migrations\IdentityServer\Configuration\
	dotnet ef migrations add InitialPersistedGrant --context PersistedGrantDbContext -o .\Migrations\IdentityServer\PersistedGrant\
	
	local .env:
		Data Source=127.0.0.1,1401;Database=master;Trusted_Connection=False;TrustServerCertificate=True;MultipleActiveResultSets=true;User Id=sa;Password=yourStrong(!)Password