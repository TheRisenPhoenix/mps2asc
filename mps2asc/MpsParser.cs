using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace mps2asc
{
    public static class MpsParser
    {
        public static List<Point> ParseXml(string path)
        {
            XmlDocument doc = new();
            doc.Load(path);

            var points = new List<Point>();
        
            var nodes = doc.DocumentElement?.SelectSingleNode("/point_set_file/point_set/time_series")?.SelectNodes("point");
            if (nodes != null)
            {
                foreach (XmlNode n in nodes)
                {
                    var xNode = n.SelectSingleNode("x");
                    var yNode = n.SelectSingleNode("y");
                    var zNode = n.SelectSingleNode("z");

                    if (xNode != null && yNode != null && zNode != null)
                    {
                        var x = double.Parse(xNode.InnerText, CultureInfo.InvariantCulture);
                        var y = double.Parse(yNode.InnerText, CultureInfo.InvariantCulture);
                        var z = double.Parse(zNode.InnerText, CultureInfo.InvariantCulture);
                        points.Add(new Point(x,y,z));
                    }
                    else
                    {
                        Console.WriteLine("Point not matching expected structure...");
                    }
                }
            }
            else
            {
                Console.WriteLine("Document not matching expected structure...");
            }

            return points;
        }
    }
}