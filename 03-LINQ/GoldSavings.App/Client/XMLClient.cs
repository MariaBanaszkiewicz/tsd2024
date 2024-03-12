using GoldSavings.App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GoldSavings.App.Client
{
    public class XMLClient
    {

        public XMLClient() { }

        public void saveListOfPrices(List<GoldPrice> listOfPrices) { 
            XDocument xDocument = new XDocument();
            XElement root = new XElement("Root");

            foreach(GoldPrice price in listOfPrices)
            {
                root.Add(new XElement("GoldPrice", new XElement("Date", price.Date.ToString()), new XElement("Price", price.Price.ToString())));
            }

            xDocument.Add(root);

            xDocument.Save(@"C:\magisterka\1sem\tsd2024\03-LINQ\gold.xml");
        }

        public List<GoldPrice> readListOfPrices()
        {
            XElement element = XElement.Load(@"C:\magisterka\1sem\tsd2024\03-LINQ\gold.xml");

            //var root = from goldPrices in element.Elements("Root") select goldPrices;

            var prices = from goldPrice in element.Elements("GoldPrice")
                         select goldPrice;

            List<GoldPrice> result = new List<GoldPrice>();

            foreach(XElement gP in prices)
            {
                result.Add(new GoldPrice() { Date = Convert.ToDateTime(gP.Element("Date").Value), Price = Convert.ToDouble(gP.Element("Price").Value) });
            }

            return result;

        }

    }
}
