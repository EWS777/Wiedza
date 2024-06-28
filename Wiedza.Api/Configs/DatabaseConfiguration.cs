using Microsoft.Data.SqlClient;
using Wiedza.Api.Core.Extensions;

namespace Wiedza.Api.Configs;

internal sealed class DatabaseConfiguration
{
    public DatabaseConfiguration(IConfiguration configuration)
    {
        var section = configuration.GetSectionOrThrow("Database");

        Server = section.GetSectionOrThrow("Server").GetValueOrThrow<string>();
        User = section.GetSectionOrThrow("User").GetValueOrThrow<string>();
        Password = section.GetSectionOrThrow("Password").GetValueOrThrow<string>();
        Database = section.GetSectionOrThrow("DatabaseName").GetValueOrThrow<string>();

        ConnectionString = new SqlConnectionStringBuilder
        {
            UserID = User,
            Password = Password,
            DataSource = Server,
            InitialCatalog = Database,
            TrustServerCertificate = true
        }.ConnectionString;
    }

    public string Server { get; }
    public string User { get; }
    public string Password { get; }
    public string Database { get; }
    public string ConnectionString { get; }
}