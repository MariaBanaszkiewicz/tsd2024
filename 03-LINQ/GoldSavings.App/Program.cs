using GoldSavings.App.Model;
using GoldSavings.App.Client;
namespace GoldSavings.App;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Gold Saver!");

        GoldClient goldClient = new GoldClient();

        XMLClient xMLClient = new XMLClient();

        GoldPrice currentPrice = goldClient.GetCurrentGoldPrice().GetAwaiter().GetResult();
        Console.WriteLine($"The price for today is {currentPrice.Price}");

        List<GoldPrice> thisMonthPrices = goldClient.GetGoldPrices(new DateTime(2024, 03, 01), new DateTime(2024, 03, 11)).GetAwaiter().GetResult();
        foreach(var goldPrice in thisMonthPrices)
        {
            Console.WriteLine($"The price for {goldPrice.Date} is {goldPrice.Price}");
        }

        // 3

        List<GoldPrice> thisYearPrices = goldClient.GetGoldPrices(DateTime.Now.AddYears(-1), DateTime.Now).GetAwaiter().GetResult();
        List<GoldPrice> top3InThisYear = thisYearPrices.OrderByDescending(p => p.Price).Take(3).ToList();
        List<GoldPrice> lowest3InThisYear = thisYearPrices.OrderBy(p => p.Price).Take(3).ToList();

        Console.WriteLine("Top 3:");
        foreach(var goldPrice in top3InThisYear) {
            Console.WriteLine($"The price for {goldPrice.Date} is {goldPrice.Price}");
        }
        Console.WriteLine("Lowest 3:");
        foreach (var goldPrice in lowest3InThisYear)
        {
            Console.WriteLine($"The price for {goldPrice.Date} is {goldPrice.Price}");
        }

        // 4

        Console.WriteLine("Earnings above 5%");

        List<GoldPrice> pricesInJan2020 = goldClient.GetGoldPrices(new DateTime(2020, 01, 01), new DateTime(2020, 01, 31)).GetAwaiter().GetResult();
        List<GoldPrice> pricesFromJan2020 = goldClient.GetGoldPrices(new DateTime(2020, 01, 01), new DateTime(2020, 12, 31)).GetAwaiter().GetResult();

        foreach( var buyPrice in pricesInJan2020)
        {
            foreach( var soldPrice in pricesFromJan2020) { 
                if (buyPrice.Date <  soldPrice.Date && soldPrice.Price > buyPrice.Price *1.05) {

                    Console.WriteLine($"Earning from {buyPrice.Date} to {soldPrice.Date} : {(soldPrice.Price / buyPrice.Price - 1) * 100}%");

                }
            }
        }

        // 5

        List<int> yearsTask5 = new List<int>() { 2019, 2020, 2021, 2022 };

        List<GoldPrice> concated2019to2022 = new List<GoldPrice>();

        foreach(var year in yearsTask5)
        {
            List<GoldPrice> prices = goldClient.GetGoldPrices(new DateTime(year, 01, 01), new DateTime(year, 12, 31)).GetAwaiter().GetResult();
            concated2019to2022.AddRange(prices);
        }

        List<GoldPrice> threeFromSecondTen = concated2019to2022.OrderByDescending(p => p.Price).Take(13).TakeLast(3).ToList();
        Console.WriteLine("Top 3 from second ten:");
        foreach (var goldPrice in threeFromSecondTen)
        {
            Console.WriteLine($"The price for {goldPrice.Date} is {goldPrice.Price}");
        }

        // 6

        List<int> yearsTask6 = new List<int>() { 2021, 2022, 2023 };
        foreach (var year in yearsTask6)
        {
            List<GoldPrice> prices = goldClient.GetGoldPrices(new DateTime(year, 01, 01), new DateTime(year, 12, 31)).GetAwaiter().GetResult();

            double avg = prices.Sum(p => p.Price)/prices.Count;

            Console.WriteLine($"Avg for {year} : {avg} ");

        }

        // 7 

        List<int> yearsTask7 = new List<int>() { 2019, 2020, 2021, 2022, 2023 };

        List<GoldPrice> concated2019to2023 = new List<GoldPrice>();

        foreach (var year in yearsTask7)
        {
            List<GoldPrice> prices = goldClient.GetGoldPrices(new DateTime(year, 01, 01), new DateTime(year, 12, 31)).GetAwaiter().GetResult();
            concated2019to2023.AddRange(prices);
        }

        double maxReturn = 0;
        GoldPrice? maxBuy = null; GoldPrice? maxSell = null;

        for(int i = 0; i < concated2019to2023.Count; i++)
        {
            for (int j = i+1; j< concated2019to2023.Count; j++)
            {
                GoldPrice buyPrice = concated2019to2023[i];
                GoldPrice sellPrice = concated2019to2023[j];

                double investmentReturn = sellPrice.Price / buyPrice.Price - 1;

                if (investmentReturn > maxReturn)
                {
                    maxReturn = investmentReturn;
                    maxBuy = buyPrice;
                    maxSell = sellPrice;
                }


            }
        }

        Console.WriteLine($"Max investment return: {maxReturn*100}% for buy on {maxBuy.Date} and sell on {maxSell.Date} ");


        // 8

        xMLClient.saveListOfPrices(thisYearPrices);

        // 9

        List<GoldPrice> xmlResult = xMLClient.readListOfPrices();

        foreach (var goldPrice in xmlResult)
        {
            Console.WriteLine($"The price for {goldPrice.Date} is {goldPrice.Price}");
        }

        // lap year

        Func<int, bool> IsLeap = (year) => year%4 == 0;

        Console.WriteLine(IsLeap(2020));

        // Collection

        MyCollection<int> list = new MyCollection<int>(new List<int>() { 0, 1, 2, 3, 4 });

        Console.WriteLine(list.Get(2));

        MyCollection<int> empty = new MyCollection<int>(new List<int>());

        Console.WriteLine(empty.IsEmpty);

        empty.Add(1);
        empty.Add(2);
        empty.Add(3);
        empty.Add(4);
        empty.Add(5);

        foreach(var item in list)
        {
            Console.WriteLine(item.ToString());
        }

    }
}
