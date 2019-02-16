using DAL.Models;
using HtmlAgilityPack;
using SelectPdf;
using System.Web.Hosting;
using DAL.DBHelpers;
using TouristWebSite.Models;

namespace TouristWebSite.Helpers
{
    public static class PDFGeneratorHelper
    {
        public static string GeneratePDF(Tour tour, int peopleCount, string comment, long addId)
        {
            var pathTemplate = HostingEnvironment.ApplicationPhysicalPath + "\\Content\\Documents\\Template.html";
            HtmlDocument HTMLdoc = new HtmlDocument();
            HTMLdoc.Load(pathTemplate);

            var myNodes = HTMLdoc.DocumentNode.SelectNodes("//div");
            foreach (HtmlNode node in myNodes)
            {
                switch (node.Id)
                {
                    case "name":
                        {
                            node.InnerHtml = tour.Name + " (" + tour.Place + ")";
                        }
                        break;

                    case "dates":
                        {
                            node.InnerHtml = "<strong> Дати: </strong>" + tour.DateStart.ToShortDateString() + " - " + tour.DateEnd.ToShortDateString();
                        }
                        break;

                    case "peopleCount":
                        {
                            node.InnerHtml = "<strong>Кількість людей: </strong>" + peopleCount;
                        }
                        break;

                    case "price":
                        {
                            node.InnerHtml = "<strong>Ціна: </strong>" + peopleCount * tour.Price;
                        }
                        break;

                    case "comment":
                        {
                            node.InnerHtml = "<strong>Ваш коментар: </strong>" + comment;
                        }
                        break;

                }
            }

            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument doc = converter.ConvertHtmlString(HTMLdoc.DocumentNode.OuterHtml);
            var pathSave = HostingEnvironment.ApplicationPhysicalPath + "\\Content\\Documents\\" + addId + ".pdf";
            doc.Save(pathSave);

            return pathSave;
        }

        public static string GenerateConstructPDF(ConstructViewModel model, long addId)
        {
            var pathTemplate = HostingEnvironment.ApplicationPhysicalPath + "\\Content\\Documents\\Constructed\\TemplateConstructed.html";
            HtmlDocument HTMLdoc = new HtmlDocument();
            HTMLdoc.Load(pathTemplate);

            var myNodes = HTMLdoc.DocumentNode.SelectNodes("//div");
            foreach (HtmlNode node in myNodes)
            {
                switch (node.Id)
                {
                    case "dates":
                        {
                            node.InnerHtml += RoutesDBHelper.GetRouteById(model.Route).Start.ToShortDateString() + " - " + RoutesDBHelper.GetRouteById(model.Route).End.ToShortDateString();
                        }
                        break;

                    case "peopleCount":
                        {
                            node.InnerHtml += model.PeopleCount.ToString();
                        }
                        break;


                    case "price":
                        {
                            long? ex1 = 0;
                            long? ex2 = 0;
                            long? ex3 = 0;
                            long? ex4 = 0;
                            long? ex5 = 0;

                            if (model.ExcursionsCount > 0)
                            {
                                ex1 = model.Excursion1;
                            }

                            if (model.ExcursionsCount > 1)
                            {
                                ex2 = model.Excursion2;
                            }

                            if (model.ExcursionsCount > 2)
                            {
                                ex3 = model.Excursion3;
                            }

                            if (model.ExcursionsCount > 3)
                            {
                                ex4 = model.Excursion4;
                            }

                            if (model.ExcursionsCount > 4)
                            {
                                ex5 = model.Excursion5;
                            }

                            node.InnerHtml += PriceCounterHelper.CountPrice(model.Route, model.Class == "Бізнес", model.BackRoute, model.BackClass == "Бізнес", model.Hotel, model.HotelClass == "Люкс", (long)ex1, (long)ex2, (long)ex3, (long)ex4, (long)ex5, model.PeopleCount) + "грн.";
                        }
                        break;

                    case "country":
                        {
                            node.InnerHtml += CountriesDBHelper.GetCountryById(model.Country).Name;
                        }
                        break;

                    case "city":
                        {
                            node.InnerHtml += CitiesDBHelper.GetCityById(model.City).Name;
                        }
                        break;

                    case "transport":
                        {
                            node.InnerHtml += TransportsDBHelper.GetTransportById(model.Transport).Name;
                        }
                        break;

                    case "leavepoint":
                        {
                            node.InnerHtml += LeavePointDBHelper.GetLeavePointById(model.LeavePoint).Name;
                        }
                        break;

                    case "destinationcountry":
                        {
                            node.InnerHtml += DestinationCountriesDBHelper.GetCountryById(model.DestinationCountry).Name;
                        }
                        break;

                    case "destinationcity":
                        {
                            node.InnerHtml += DestinationCitiesDBHelper.GetCityById(model.DestinationCity).Name;
                        }
                        break;

                    case "destinationpoint":
                        {
                            node.InnerHtml += DestinationPointDBHelper.GetDestinationPointById(model.DestinationPoint).Name;
                        }
                        break;

                    case "route":
                        {
                            var route = RoutesDBHelper.GetRouteById(model.Route);
                            node.InnerHtml += route.Name + "(" + route.Start.ToShortDateString() + ", " + route.Start.ToShortTimeString() + " - " + route.End.ToShortDateString() + ", " + route.End.ToShortTimeString() + ")";
                        }
                        break;

                    case "class":
                        {
                            node.InnerHtml += model.Class;
                        }
                        break;

                    case "hotel":
                        {
                            node.InnerHtml += HotelsDBHelper.GetHotelById(model.Hotel).Name;
                        }
                        break;

                    case "hotelclass":
                        {
                            node.InnerHtml += model.HotelClass;
                        }
                        break;

                    case "excursions":
                        {
                            string content = string.Empty;

                            if (model.ExcursionsCount > 0)
                            {
                                content += ExcursionsDBHelper.GetExcursionById(model.Excursion1).Name + ", ";
                            }

                            if (model.ExcursionsCount > 1)
                            {
                                content += ExcursionsDBHelper.GetExcursionById(model.Excursion2).Name + ", ";
                            }

                            if (model.ExcursionsCount > 2)
                            {
                                content += ExcursionsDBHelper.GetExcursionById(model.Excursion3).Name + ", ";
                            }

                            if (model.ExcursionsCount > 3)
                            {
                                content += ExcursionsDBHelper.GetExcursionById(model.Excursion4).Name + ", ";
                            }

                            if (model.ExcursionsCount > 4)
                            {
                                content += ExcursionsDBHelper.GetExcursionById(model.Excursion5).Name + ", ";
                            }

                            if (content != string.Empty)
                            {
                                content = content.Substring(0, content.Length - 2);
                            }

                            node.InnerHtml += content;
                        }
                        break;

                    case "backroute":
                        {
                            var route = BackRoutesDBHelper.GetBackRouteById(model.BackRoute);
                            node.InnerHtml += route.Name + "(" + route.Start.ToShortDateString() + ", " + route.Start.ToShortTimeString() + " - " + route.End.ToShortDateString() + ", " + route.End.ToShortTimeString() + ")";
                        }
                        break;

                    case "backclass":
                        {
                            node.InnerHtml += model.BackClass;
                        }
                        break;
                }
            }

            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument doc = converter.ConvertHtmlString(HTMLdoc.DocumentNode.OuterHtml);
            var pathSave = HostingEnvironment.ApplicationPhysicalPath + "\\Content\\Documents\\Constructed\\" + addId + ".pdf";
            doc.Save(pathSave);

            return pathSave;
        }
    }
}