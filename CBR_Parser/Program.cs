using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;

namespace CBR_Parser
{
    class Program
    {
        private const string ApiUrl = @"http://www.cbr.ru/DailyInfoWebServ/DailyInfo.asmx";
        private static readonly DateTime reqDate = DateTime.Today.AddDays(1);

        static void Main()
        {
            List<Currency> currencies = ExtractCurrencies();
            if (currencies == null) return;
            List<MetalCurs> metals = ExtractMetalCurs();
            if (metals == null) return;
            List<ReuterCurrency> reuterCurrencies = ExtractReutersCurs();
            if (reuterCurrencies == null) return;

            #region write CBR.csv
            if (!Directory.Exists("Reports\\"))
            {
                Directory.CreateDirectory("Reports\\");
            }
            FileStream fileStream = new FileStream($"Reports\\CBR{DateTime.Today:ddMMyy}.csv", FileMode.Create);
            using (StreamWriter fileWriter = new StreamWriter(fileStream, Encoding.UTF8))
            {
                foreach (Currency cur in currencies)
                {
                    Write(fileWriter, cur);
                }
                foreach (MetalCurs met in metals)
                {
                    Write(fileWriter, met);
                }
            }
            #endregion

            #region write NC.csv
            fileStream = new FileStream($"Reports\\NC{DateTime.Today:ddMMyy}.csv", FileMode.Create);
            using (StreamWriter fileWriter = new StreamWriter(fileStream, Encoding.UTF8))
            {
                foreach (ReuterCurrency reutCurrency in reuterCurrencies)
                {
                    Write(fileWriter, reutCurrency);
                }
            }
            #endregion
        }

        private static void Write(StreamWriter fileWriter, ReuterCurrency reutCurrency)
        {
            if (!string.IsNullOrEmpty(reutCurrency.ISOCode) && !string.IsNullOrEmpty(reutCurrency.Name))
            {
                string date1 = reqDate.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
                fileWriter.Write($"{reutCurrency.ISOCode},");
                fileWriter.Write($"{reutCurrency.Price},");
                fileWriter.Write($"{reutCurrency.Price},");
                fileWriter.Write($"{DateTime.Now.TimeOfDay:hh\\:mm\\:ss},", CultureInfo.InvariantCulture);
                fileWriter.Write($"{date1},");
                fileWriter.Write($"{reutCurrency.Name}\r\n");
            }
        }

        private static void Write (StreamWriter writer, Currency currency)
        {
            string date = reqDate.AddDays(-1).ToString("M/d/yyyy", CultureInfo.InvariantCulture);
            string date1 = reqDate.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
            writer.Write($"{currency.VchCode},");
            writer.Write($"{currency.Nominal},");
            writer.Write($"{currency.Curs.Replace(',', '.')},");
            writer.Write($"{date1},");
            writer.Write($"{date},");
            writer.Write($"{DateTime.Now.TimeOfDay:hh\\:mm\\:ss}\r\n", CultureInfo.InvariantCulture);
        }

        private static void Write(StreamWriter writer, MetalCurs metal)
        {
            string date = reqDate.AddDays(-1).ToString("M/d/yyyy", CultureInfo.InvariantCulture);
            string date1 = reqDate.ToString("MM/d/yyyy", CultureInfo.InvariantCulture);
            writer.Write($"{metal.ISOCode},");
            writer.Write($"1,");
            writer.Write($"{metal.Price.Replace(',', '.')},");
            writer.Write($"{date1},");
            writer.Write($"{date},");
            writer.Write($"{DateTime.Now.TimeOfDay:hh\\:mm\\:ss}\r\n", CultureInfo.InvariantCulture);
        }

        private static StringBuilder GenerateReutersSoap()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
            sb.Append("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">");
            sb.Append("<soap:Body>");
            sb.Append("<GetReutersCursOnDateXML xmlns=\"http://web.cbr.ru/\">");
            sb.Append($"<On_date>{reqDate.AddDays(-1):yyyy-MM-dd}</On_date>");
            sb.Append("</GetReutersCursOnDateXML>");
            sb.Append("</soap:Body>");
            sb.Append("</soap:Envelope>");
            return sb;
        }

        private static StringBuilder GenerateMetalSoap()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
            sb.Append("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">");
            sb.Append("<soap:Body>");
            sb.Append("<DragMetDynamicXML xmlns=\"http://web.cbr.ru/\">");
            sb.Append($"<fromDate>{reqDate:yyyy-MM-dd}</fromDate>");
            sb.Append($"<ToDate>{reqDate:yyyy-MM-dd}</ToDate>");
            sb.Append("</DragMetDynamicXML>");
            sb.Append("</soap:Body>");
            sb.Append("</soap:Envelope>");
            return sb;
        }

        private static StringBuilder GenerateCursOnDateSoap()
        {
            StringBuilder cursOnDateSoap = new StringBuilder();
            cursOnDateSoap.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
            cursOnDateSoap.Append("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">");
            cursOnDateSoap.Append("<soap:Body>");
            cursOnDateSoap.Append("<GetCursOnDateXML xmlns=\"http://web.cbr.ru/\">");
            cursOnDateSoap.Append($"<On_date>{reqDate:yyyy-MM-dd}</On_date>");
            cursOnDateSoap.Append("</GetCursOnDateXML>");
            cursOnDateSoap.Append("</soap:Body>");
            cursOnDateSoap.Append("</soap:Envelope>");
            return cursOnDateSoap;
        }

        private static XmlDocument GetDocument(string message)
        {
            try
            {
                #region request
                WebRequest CursRequest = (HttpWebRequest)WebRequest.Create(ApiUrl);
                CursRequest.ContentType = @"text/xml";
                CursRequest.Method = "POST";

                UTF8Encoding encoding = new UTF8Encoding();
                byte[] CursBytes = encoding.GetBytes(message);
                Stream newStream = CursRequest.GetRequestStream();
                newStream.Write(CursBytes, 0, CursBytes.Length);
                newStream.Close();
                #endregion

                HttpWebResponse response;

                #region get response

                response = (HttpWebResponse)CursRequest.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                XmlDocument document = new XmlDocument();
                document.LoadXml(reader.ReadToEnd());
                return document;
                #endregion
            }
            catch (WebException e)
            {
                Console.WriteLine("Ошибка соединения. Описание:");
                Console.WriteLine(e.Message);
                Console.WriteLine("Убедитесь что у Вас включен LocalProxy");
                Console.WriteLine("Press any key to quit");
                Console.ReadKey();
                return null;
            }
            
        }

        private static List<Currency> ExtractCurrencies()
        {
            StringBuilder cursOnDateSoap = GenerateCursOnDateSoap();
            XmlDocument document = GetDocument(cursOnDateSoap.ToString());
            if (document == null) return null;
            List<Currency> currencies = new List<Currency>();
            XmlNode rs = document.ChildNodes[1].FirstChild.FirstChild.FirstChild.FirstChild;
            foreach (XmlNode node in rs.ChildNodes)
            {
                Currency currency = new Currency();
                foreach (XmlNode childNode in node)
                {
                    switch (childNode.Name)
                    {
                        case ("Vnom"):
                            currency.Nominal = childNode.InnerText.Trim();
                            break;
                        case ("Vcurs"):
                            currency.Curs = childNode.InnerText.Trim().Replace('.', ',');
                            break;
                        case ("Vcode"):
                            Int32 code = Convert.ToInt32(childNode.InnerText.Trim());
                            currency.Vcode = $"{code:000}";
                            break;
                        case ("VchCode"):
                            currency.VchCode = $"{childNode.InnerText.Trim()}";
                            break;
                        case ("Vname"):
                            currency.Name = childNode.InnerText.Trim();
                            break;
                    }
                }
                currencies.Add(currency);
            }
            return currencies;
        }

        private static List<MetalCurs> ExtractMetalCurs()
        {
            StringBuilder MetalSoap = GenerateMetalSoap();
            XmlDocument document = GetDocument(MetalSoap.ToString());
            if (document == null) return null;
            List<MetalCurs> metals = new List<MetalCurs>();
            XmlNode rs = document.ChildNodes[1].FirstChild.FirstChild.FirstChild.FirstChild;
            foreach (XmlNode node in rs.ChildNodes)
            {
                MetalCurs metal = new MetalCurs();
                foreach (XmlNode childNode in node)
                {
                    switch (childNode.Name)
                    {
                        case ("DateMet"):
                            metal.DateMet = Convert.ToDateTime(childNode.InnerText.Trim());
                            break;
                        case ("CodMet"):
                            metal.Code = Convert.ToInt32(childNode.InnerText.Trim());
                            break;
                        case ("price"):
                            metal.Price = childNode.InnerText.Trim();
                            break;
                    }

                }
                metals.Add(metal);
            }
            return metals;
        }

        private static List<ReuterCurrency> ExtractReutersCurs()
        {
            StringBuilder ReutersSoap = GenerateReutersSoap();
            XmlDocument document = GetDocument(ReutersSoap.ToString());
            if (document == null) return null;
            List<ReuterCurrency> reuterCurrencies = new List<ReuterCurrency>();
            XmlNode rs = document.ChildNodes[1].FirstChild.FirstChild.FirstChild.FirstChild;
            foreach (XmlNode node in rs.ChildNodes)
            {
                ReuterCurrency reuterCur = new ReuterCurrency();
                foreach (XmlNode childNode in node)
                {
                    switch (childNode.Name)
                    {
                        case ("num_code"):
                            int code = Convert.ToInt32(childNode.InnerText.Trim());
                            reuterCur.Code = code.ToString("000");
                            break;
                        case ("val"):
                            reuterCur.Price = childNode.InnerText.Trim();
                            break;
                        case ("dir"):
                            reuterCur.Dir = childNode.InnerText.Trim();
                            break;
                    }
                }
                reuterCurrencies.Add(reuterCur);
            }
            ReuterCurrency eur = new ReuterCurrency
            {
                Code = "978",
                Dir = "0",
                Price = "1"
            };
            ReuterCurrency usd = new ReuterCurrency
            {
                Code = "840",
                Dir = "0",
                Price = "1"
            };
            reuterCurrencies.Add(eur);
            reuterCurrencies.Add(usd);
            return reuterCurrencies;
        }
    }
}
