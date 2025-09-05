using SQLite;
using tellahs_library.Constants;

namespace tellahs_library.Helpers;

public static class SqliteHelper
{
    public static SQLiteAsyncConnection GetAsyncSqlConnection() => new(SqliteDbConstants.DbPath);
    public static SQLiteConnection GetSqlConnection() => new(SqliteDbConstants.DbPath);
}
