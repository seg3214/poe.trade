using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
namespace poe.trade_trader
{
    static public class Currencies
    {
        public class curr
        {
            public string Name;
            public int stacksize;
            public Image Image;
        }
        static public List<curr> CurrencyList = new List<curr>();
        static Currencies()
        {
            XmlDocument xml = new XmlDocument();//.Load("XMLFile1.xml");
            xml.Load(@"assets\XMLFile1.xml");
            XmlNodeList xnList = xml.SelectNodes("/Currencies/Currency");
            foreach (XmlNode xn in xnList)
            {
                curr cc = new curr();
                cc.stacksize = Int32.Parse(xn["Stacksize"].InnerText);
                cc.Name = xn["Name"].InnerText;
                cc.Image = Image.FromFile(@"assets\images\" + xn["Image"].InnerText);
                Console.WriteLine("Name: {0} {1} {2}", cc.stacksize, cc.Name, cc.Image);
                CurrencyList.Add(cc);
            }
        }
    }
}
