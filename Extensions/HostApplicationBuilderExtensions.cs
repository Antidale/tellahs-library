using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace tellahs_library.Extensions;

public static class HostApplicationBuilderExtensions
{
    public static HostApplicationBuilder ConfigureEnvironmentVariables(this HostApplicationBuilder builder)
    {
#if DEBUG
        builder.Configuration.AddEnvironmentVariables(prefix: "SL_");
#else
        builder.Configuration.AddEnvironmentVariables(prefix: "TL_");
#endif

        builder.Configuration.AddEnvironmentVariables(prefix: "FE_GEN_");
        return builder;
    }
}
