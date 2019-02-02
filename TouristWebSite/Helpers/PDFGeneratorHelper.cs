using HtmlAgilityPack;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using DAL.Models;

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
    }
}