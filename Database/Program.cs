using Database;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

internal class Program
{
    private async static Task Main(string[] args)
    {
        AbstractDatabase UserDB = new UserDB();

        _ = await UserDB.Reset();


    }
}
