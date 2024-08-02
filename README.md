# WorldSportsBetting.CurrencyExchange.WebAPI

1. After cloning repo, open solution in Visual Studio 2022.
2. Restore all nuget packages - no custom packages.
3. Run the following migrations scripts in the 'Package Manager Console' to get your database ready.
4. Ensure the Default project is set to 'WorldSportsBetting.CurrencyExchange.MySql'.
4.1 add-migration DbContextMigration000001.
4.1 update-database.
5. Run your solution in visual studio.

# Prerequisites

- Visual Studio 2022
- .NET 8
- C#

# Note

The appsettings.json file changes...

- Change your AppId - 'OpenExchangeRatesAPI:AppId' (you will see ADD_YOUR_APP_ID_HERE).

- Change your ConnectionString password 'MySQLConnectionString:WSBCurrencyExchange' (you will see ADD_YOUR_CONNECTION_STRING_PASSWORD_HERE), I doubt we are using the same MySQL server instance paswword. Also check the connection string an verify if it is pointing to an existing MySQL server database.

- Ensure Redis is pointing to the correct server
