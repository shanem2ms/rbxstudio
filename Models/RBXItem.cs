using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace RBXStudio.Models
{
    public class RBXItem
    {
        List<RBXItem> items;
        public string rbclass;

        public Dictionary<string, object> properties =
                new Dictionary<string, object>();
        public List<RBXItem> Items { get { return items; } }

        public string Name => properties["Name"] as string;
        public RBXItem(XElement element)
        {
            rbclass = element.Attribute("class")?.Value;
            foreach (XElement xitem in element.Elements("Item"))
            {
                if (items == null)
                    items = new List<RBXItem>();
                items.Add(new RBXItem(xitem));
            }
            XElement propertiesElem = element.Element("Properties");
            foreach (XElement xprop in propertiesElem.Elements())
            {
                string propname = xprop.Attribute("name")?.Value;
                string proptype = xprop.Name.LocalName;
                switch (proptype)
                {
                    case "bool":
                        properties[propname] = bool.Parse(xprop.Value);
                        break;
                    case "float":
                        properties[propname] = xprop.Value == "inf" ? float.PositiveInfinity : float.Parse(xprop.Value);
                        break;
                    case "int":
                        properties[propname] = int.Parse(xprop.Value);
                        break;
                    case "string":
                        properties[propname] = xprop.Value;
                        break;
                }
            }
        }
    }
}
