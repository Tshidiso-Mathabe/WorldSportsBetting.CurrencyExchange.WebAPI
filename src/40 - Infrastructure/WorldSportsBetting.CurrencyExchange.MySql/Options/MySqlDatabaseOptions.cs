namespace WorldSportsBetting.CurrencyExchange.MySql.Options
{
    public class MySqlDatabaseOptions
    {
        public const string SECTION = "MySQLConnectionString";

        public required string WSBCurrencyExchange { get; set; }
        public required bool UseInMemory { get; set; }
    }
}
