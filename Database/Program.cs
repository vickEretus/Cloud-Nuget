using Database;
using System.Threading.Tasks;

internal class Program
{
    private static async Task Main(string[] args)
    {
        AbstractDatabase UserDB = new UserDB();

        _ = await UserDB.Reset();

    }
}
