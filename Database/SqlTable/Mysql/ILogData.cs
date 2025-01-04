using System.Collections.Generic;
using System.Numerics;

namespace dcbot.Database.SqlTable.Mysql;

public interface ILogData : IDatabase
{
    protected record Tables
    {
        public const string Main = "log_user_value_changes";
        public static Database.Mysql.TableValue MainId = new(Main, "id");
        public static Database.Mysql.TableValue MainUserId = new(Main, "userID");
        public static Database.Mysql.TableValue MainPyroxene = new(Main, "pyroxene");
        public static Database.Mysql.TableValue MainLotteryAmount = new(Main, "lotteryAmount");
        public static Database.Mysql.TableValue MainGacha = new(Main, "gacha");
        public static Database.Mysql.TableValue MainBonusHundred = new(Main, "bonusHundred");
        public static Database.Mysql.TableValue MainBonusThreeHundred = new(Main, "bonusThreeHundred");
        public static Database.Mysql.TableValue MainBonusThousand = new(Main, "bonusThousand");
        public static Database.Mysql.TableValue MainTime = new(Main, "time");
    }

    protected static void LogValueChange<T>(ulong userId, Database.Mysql.TableValue tableValue, T value)
        where T : INumber<T>
    {
        if (tableValue.Table is not Tables.Main) return;
        var column = tableValue.Value;
        Sql.Insert(Tables.Main,
            new Dictionary<string, object> { { Tables.MainUserId.Value, userId }, { column, value } },
            [Tables.MainTime.Value], [column]);
    }
}