using System;

namespace EscortBookUser.Web.Config;

public static class Postgres
{
    public static readonly string ConnectionString = Environment.GetEnvironmentVariable("PG_DB_CONNECTION");
}
