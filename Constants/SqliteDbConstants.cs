using System;

namespace tellahs_library.Constants;

public class SqliteDbConstants
{
#if DEBUG
    public const string DbPath = "tl-data-db";
#else
    public const string DbPath = "../tl-data/tl-data-db";
#endif
}
