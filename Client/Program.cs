using Client;
using Common.Logging;
using System.Net;

internal class Program
{
    private static readonly FeaturedAPI Connection = new("https://localhost:7238/api/");

    private static readonly APIRequestErrorHandler LoginOn401 = new APIRequestErrorHandler().AddCondition(HttpStatusCode.Unauthorized, async () => await Connection.Login("admin", "root"));

    private static async Task Main(string[] args)
    {
        LogWriter.OutputLevel = Common.Logging.LogLevel.DEBUG;

        await Connection.Login("admin", "root");

        Thread.Sleep(1000);

        await Connection.Logout();

        Thread.Sleep(1000);
    }

}