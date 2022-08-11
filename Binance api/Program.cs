using Binance.Net.Clients;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;

BinanceClient.SetDefaultOptions(new BinanceClientOptions()
{
    ApiCredentials = new ApiCredentials("QRrYQpHMTqN2WYpp35YJyL1YtMA2zwbTZDmXzxnyZdegcNWzQEe7pltTCtKZE0NY", "3XzbUjG2DirgLnUA7JzWzen5A4T2voprkjgWGW9LpsM0lgFg2jAdSEYZqozhHS3p"),
});

using (var binanceclient = new BinanceClient())
{
    bool IsTrue = true; int num = 1;
    while (IsTrue)
    {
        var balanceData = await binanceclient.UsdFuturesApi.Account.GetBalancesAsync();
        if (balanceData.Data == null) 
        {
            Console.WriteLine("Connection lost ... ");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"Reconnecting in {i}");
                Thread.Sleep(100);
            }
            //return;
        }

        foreach (var item in balanceData.Data)
        {
            if (item.AvailableBalance != 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"    AvailableBalance => {num} => " +
                            item.AvailableBalance + "\n    " + "WalletBalance => " +
                                item.WalletBalance);
                if (!item.CrossUnrealizedPnl.ToString().Contains("-") &&
                    item.CrossUnrealizedPnl.ToString() != "0,00000000")
                {
                    Console.Beep(400, 300);
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if(item.CrossUnrealizedPnl.ToString() == "0,00000000")
                        Console.ForegroundColor = ConsoleColor.White;
                else if(item.CrossUnrealizedPnl.ToString().Contains("-") &&
                    item.CrossUnrealizedPnl.ToString() != "0,00000000")
                    Console.ForegroundColor = ConsoleColor.Red;


                Console.WriteLine("\n    CrossUnrealizedPnl => " + item.CrossUnrealizedPnl +
                                "\n" + "-----------------------------------------");

            }
        }
        num++;
    }
}
