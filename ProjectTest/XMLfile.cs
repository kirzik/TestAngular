using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace WebApplication
{
    public class XMLfile
    {

       
       
        public static List<Meter> GetSerialNumberAll()
        {
            List<Meter> serialNumbers = new List<Meter>();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("metersNew.xml");
            // получим корневой элемент
            XmlElement? xRoot = xDoc.DocumentElement;

            if (xRoot != null)
            {
                // обход всех узлов в корневом элементе
                foreach (XmlElement xnode in xRoot)
                {
                    // получаем атрибут serialNumber
                    XmlNode serialNumberAtr = xnode.Attributes.GetNamedItem("serialNumber");
                    var sn = Convert.ToInt32(serialNumberAtr.Value);
                    Meter meter = new Meter {
                        Name = "Прибор № " + sn,
                        SerialNumber = sn
                    };
                    serialNumbers.Add(meter);
                }
            }

            return serialNumbers;
        }

        public static List<Parametr> GetData(int serialNumber, DateTime dateTime)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("metersNew.xml");

            // получим корневой элемент
            XmlElement? xRoot = xDoc.DocumentElement;
            // время, А +, А -, Р +, Р -
            List <Parametr> mpData = new List<Parametr>();
            if (xRoot != null)
            {
                var xRootList = new List<XmlElement>();
                foreach (XmlElement xnode in xRoot)
                {
                    xRootList.Add(xnode);
                }

                var chilsdMeter = xRootList.FirstOrDefault(x => x.Attributes.GetNamedItem("serialNumber").Value == serialNumber.ToString()).ChildNodes;

                foreach (XmlNode chilM in chilsdMeter)
                {
                    var onlyDate = dateTime.Date;
                    XmlNode dateT = chilM.Attributes.GetNamedItem("dateTime");
                    DateTime longDate = Convert.ToDateTime(chilM.Attributes.GetNamedItem("dateTime").Value.Replace('T', ' ').Replace('-', ','));
                    DateTime shortDate = Convert.ToDateTime(dateT.Value.Remove(10).Replace('-', ','));

                    //if (shortDate != onlyDate)
                    //    continue;

                    if (((shortDate == onlyDate) && (longDate != onlyDate)) || longDate == dateTime.AddDays(1))
                    {
                        var par = new List<XmlElement>();
                        foreach (XmlElement item in chilM.ChildNodes)
                        {
                            par.Add(item);
                        }
                        par.OrderBy(x => Convert.ToDateTime(x.Attributes.GetNamedItem("dateTime").Value.Replace('T', ' ').Replace('-', ',')));
                        

                        var dTEnd = Convert.ToDateTime(dateT.Value.Replace('T', ' ').Replace('-', ','));
                        var dTStart = Convert.ToDateTime(dateT.Value.Replace('T', ' ').Replace('-', ',')).AddMinutes(-30);

                        var activePlusValue = par.FirstOrDefault(x=>x.Name == "activePlus") != null ? par.FirstOrDefault(x => x.Name == "activePlus").InnerXml : "";
                        var activeMinusValue = par.FirstOrDefault(x=>x.Name == "activeMinus") != null ? par.FirstOrDefault(x => x.Name == "activeMinus").InnerXml : "";
                        var rectivePlusValue = par.FirstOrDefault(x=>x.Name == "rectivePlus") != null ? par.FirstOrDefault(x => x.Name == "rectivePlus").InnerXml : "";
                        var rectiveMinusValue = par.FirstOrDefault(x=>x.Name == "rectiveMinus") != null ? par.FirstOrDefault(x => x.Name == "rectiveMinus").InnerXml : "";

                        mpData.Add(new Parametr()
                                {
                                    DataTimeStart = dTStart.ToString("t"),
                                    DataTimeEnd = dTEnd.ToString("t"),
                                    ActivePlus = activePlusValue,
                                    ActiveMinus = activeMinusValue,
                                    RectivePlus = rectivePlusValue,
                                    RectiveMinus = rectiveMinusValue
                                }
                         );

                    }
                }

            }
        return mpData;
        
        }
    }
}
