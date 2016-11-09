using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TreeMapV2
{
    public class XmlReaderHelper
    {
        /*public string XmlContent { private get; set; } = "<breakfast_menu>" +
                "<food><name>Belgian Waffles</name><price>$5.95</price><description>Two of our famous Belgian Waffles with plenty of real maple syrup</description><calories>650</calories></food>" +
                "<food><name>Strawberry Belgian Waffles</name><price>$7.95</price><description>Light Belgian waffles covered with strawberries and whipped cream</description><calories>900</calories></food>" +
                "<food><name>Berry-Berry Belgian Waffles</name><price>$8.95</price><description>Light Belgian waffles covered with an assortment of fresh berries and whipped cream</description><calories>900</calories></food>" +
                "<food><name>French Toast</name><price>$4.50</price><description>Thick slices made from our homemade sourdough bread</description><calories>600</calories></food>" +
                "<food><name>Homestyle Breakfast</name><price>$6.95</price><description>Two eggs, bacon or sausage, toast, and our ever-popular hash browns</description><calories>950</calories></food>" +
                "</breakfast_menu>";*/

        /*public string XmlContent { private get; set; } = "<breakfast_menu>" +
        "<food>" +
           "<name>" +
               "<price>" +
                   "<quantity></quantity>" +
                   "<quantity></quantity>" +
               "</price>" +
               "<price></price>" +
           "</name>" +
           "<name></name>" +
        "</food>" +
        "<food><name></name></food>" +
        "</breakfast_menu>";
          public XmlNode XmlRoot { get; set; }*/


        /* public string XmlContent { private get; set; } = "<breakfast_menu>" +
          "<food>" +
                    "<name><title><let></let><let></let>" +
                      "</title></name><name></name><name></name>"+
          "</food>" +
          "<food></food><food></food>"+
          "</breakfast_menu>";
            public XmlNode XmlRoot { get; set; }*/


        /*public string XmlContent { private get; set; } = "<breakfast_menu>" +
           "<food><let><ok><pole><lesy></lesy></pole></ok></let></food>" +
          "<food><prava1><prava2><prava3></prava3></prava2></prava1></food>" +
    "</breakfast_menu>";*/

        /*public string XmlContent { private get; set; } = "<breakfast_menu>" +
       "<foodl><koala><koala></koala></koala></foodl>" +
        "<foodc><koala2><koala3></koala3></koala2></foodc><foodr><koala></koala></foodr><foodr><koalaLast></koalaLast></foodr>" +
"</breakfast_menu>";*/

        public string XmlContent { private get; set; } = "<breakfast_menu>" +
                                                         "<food1></food1>"+
                                                         "<food2></food2>"+
                                                         "<food3></food3>"+
                                                         "</breakfast_menu>";


        public XmlNode XmlRoot { get; set; }

        public XmlReaderHelper()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(XmlContent);
            XmlRoot = xmlDocument.FirstChild;
        }

        public void ReadXml(XmlNode node)
        {

            if (node.HasChildNodes)
            {
                Console.WriteLine($"Root {node.Name} Počet potomků {node.ChildNodes.Count}");
                ReadXml(node.FirstChild);
            }
            if (node.NextSibling != null)
            {
                ReadXml(node.NextSibling);
            }

        }

    }
}
