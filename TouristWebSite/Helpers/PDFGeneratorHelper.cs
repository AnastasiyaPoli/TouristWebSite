using DAL.DBHelpers;
using DAL.Models;
using HtmlAgilityPack;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Web.Hosting;
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
                            node.InnerHtml += model.Price + "грн.";
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

        public static string GeneratePDFRecommend(List<Tour> tours, string userId)
        {
            var pathTemplate = HostingEnvironment.ApplicationPhysicalPath + "\\Content\\Documents\\Recommendations\\TemplateRecommendations.html";
            HtmlDocument HTMLdoc = new HtmlDocument();
            HTMLdoc.Load(pathTemplate);

            var myNodes = HTMLdoc.DocumentNode.SelectNodes("//div");

            bool end = false;
            foreach (HtmlNode node in myNodes)
            {
                if (end) break;
                switch (node.Id)
                {
                    case "0":
                        {
                            if (tours.Count < 1)
                            {
                                node.SetAttributeValue("style", "border: 1px solid black; border-radius: 10px; padding: 10px; display: none");
                                end = true;
                            }
                        }
                        break;

                    case "name0":
                        {
                            node.InnerHtml = tours[0].Name;
                        }
                        break;

                    case "place0":
                        {
                            node.InnerHtml = "<strong> Місце: </strong>" + tours[0].Place;
                        }
                        break;

                    case "description0":
                        {
                            node.InnerHtml = "<strong> Опис: </strong>" + tours[0].Description;
                        }
                        break;

                    case "dates0":
                        {
                            node.InnerHtml = "<strong> Дати: </strong>" + tours[0].DateStart.ToShortDateString() + "-" + tours[0].DateEnd.ToShortDateString();
                        }
                        break;

                    case "price0":
                        {
                            node.InnerHtml = "<strong> Ціна: </strong>" + tours[0].Price;
                        }
                        break;
                }
            }

            end = false;
            foreach (HtmlNode node in myNodes)
            {
                if (end) break;
                switch (node.Id)
                {
                    case "1":
                        {
                            if (tours.Count < 2)
                            {
                                node.SetAttributeValue("style", "border: 1px solid black; border-radius: 10px; padding: 10px; display: none");
                                end = true;
                            }
                        }
                        break;

                    case "name1":
                        {
                            node.InnerHtml = tours[1].Name;
                        }
                        break;

                    case "place1":
                        {
                            node.InnerHtml = "<strong> Місце: </strong>" + tours[1].Place;
                        }
                        break;

                    case "description1":
                        {
                            node.InnerHtml = "<strong> Опис: </strong>" + tours[1].Description;
                        }
                        break;

                    case "dates1":
                        {
                            node.InnerHtml = "<strong> Дати: </strong>" + tours[1].DateStart.ToShortDateString() + "-" + tours[1].DateEnd.ToShortDateString();
                        }
                        break;

                    case "price1":
                        {
                            node.InnerHtml = "<strong> Ціна: </strong>" + tours[1].Price;
                        }
                        break;
                }
            }

            end = false;
            foreach (HtmlNode node in myNodes)
            {
                if (end) break;
                switch (node.Id)
                {
                    case "2":
                        {
                            if (tours.Count < 3)
                            {
                                node.SetAttributeValue("style", "border: 1px solid black; border-radius: 10px; padding: 10px; display: none");
                                end = true;
                            }
                        }
                        break;

                    case "name2":
                        {
                            node.InnerHtml = tours[2].Name;
                        }
                        break;

                    case "place2":
                        {
                            node.InnerHtml = "<strong> Місце: </strong>" + tours[2].Place;
                        }
                        break;

                    case "description2":
                        {
                            node.InnerHtml = "<strong> Опис: </strong>" + tours[2].Description;
                        }
                        break;

                    case "dates2":
                        {
                            node.InnerHtml = "<strong> Дати: </strong>" + tours[2].DateStart.ToShortDateString() + "-" + tours[2].DateEnd.ToShortDateString();
                        }
                        break;

                    case "price2":
                        {
                            node.InnerHtml = "<strong> Ціна: </strong>" + tours[2].Price;
                        }
                        break;
                }
            }

            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument doc = converter.ConvertHtmlString(HTMLdoc.DocumentNode.OuterHtml);
            var pathSave = HostingEnvironment.ApplicationPhysicalPath + "\\Content\\Documents\\Recommendations\\" + userId + "-" + DateTime.Now.Millisecond + ".pdf";
            doc.Save(pathSave);

            return pathSave;
        }
    }
}