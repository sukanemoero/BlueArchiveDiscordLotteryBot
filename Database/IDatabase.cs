using System;
using dcbot.Exceptions;
using dcbot.Exceptions.Database;

namespace dcbot.Database;

public interface IDatabase
{
    protected static Mysql Sql =>
        (Db ?? throw new DatabaseNotConnected()).IsConnected() ? Db : throw new DatabaseNotConnected();

    private static Mysql Db { get; set; }

    public static void SetDatabase(Mysql database)
    {
        Db = database ?? throw new ArgumentNullException(nameof(database));
    }
}