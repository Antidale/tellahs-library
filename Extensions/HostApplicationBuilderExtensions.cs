using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SQLite;
using tellahs_library.Constants;
using tellahs_library.Helpers;

namespace tellahs_library.Extensions;

public static class HostApplicationBuilderExtensions
{
    extension(HostApplicationBuilder builder)
    {
        public HostApplicationBuilder ConfigureEnvironmentVariables(BoundUrlSettings urlSettings)
        {

#if DEBUG
            builder.Configuration.AddEnvironmentVariables(prefix: "SL_");
#else
            builder.Configuration.AddEnvironmentVariables(prefix: "TL_");
#endif

            builder.Configuration.AddEnvironmentVariables(prefix: "FE_GEN_");
            builder.Configuration.Bind(urlSettings);
            return builder;
        }
    }

    public static HostApplicationBuilder SetupSqlite(this HostApplicationBuilder builder)
    {
        var db = SqliteHelper.GetSqlConnection();
        db.CreateTable<Entities.ActiveRace>();

        return builder;
    }
}
