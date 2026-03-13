using SQLite;
using tellahs_library.Constants;

namespace tellahs_library.Helpers;

public interface ISqliteHelper
{
    public ISQLiteAsyncConnection GetAsyncSqlConnection();
    public SQLiteConnection GetSqlConnection();
}


public class SqliteHelper : ISqliteHelper
{
    private SQLiteAsyncConnection GetAsyncConnection() => new(SqliteDbConstants.DbPath);
    private SQLiteConnection GetConnection() => new(SqliteDbConstants.DbPath);

    public ISQLiteAsyncConnection GetAsyncSqlConnection() => GetAsyncConnection();

    public SQLiteConnection GetSqlConnection() => GetConnection();
}
