using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;

namespace RBXStudio.Models
{
    public class RBXDoc
    {
        List<RBXItem> items = new List<RBXItem>();
        public List<RBXItem> Items => items;
        public void Open(string file)
        {
            if (File.Exists(file))
            {
                XDocument xmlDoc = XDocument.Load(file);
                XElement rbxElement = xmlDoc.XPathSelectElement("roblox");
                foreach (XElement element in rbxElement.Elements())
                {
                    if (element.Name == "Item")
                    {
                        items.Add(new RBXItem(element));
                    }
                }
            }
            else
            {
                Console.WriteLine("XML file not found.");
            }
        }
    }
}
