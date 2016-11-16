using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;



namespace TikiCrawler.Controllers
{
    public class Product
    {
        private string ProductID { get; set; }
        private string LinkProduct { get; set; }
        private string ProductName { get; set; }
        private string Brand { get; set; }
        private string Price { get; set; }
        private string TotalReviewPoint { get; set; }


        public Product() { }
        public Product(string productid,string linkproduct, string name, string brand, string price ,string totalreviewpoint)
        {
            ProductID = productid;
            LinkProduct = linkproduct;
            ProductName = name;
            Brand = brand;
            Price = price;
            TotalReviewPoint = totalreviewpoint;
        }
    }

    public class GetTikiController : Controller
    {
        // get product information 
        private Product GetProductInformations(HtmlDocument htmlSnippet)
        {
            Product product;


            HtmlNode ProductID_Node =  htmlSnippet.DocumentNode.SelectNodes("//input[@id=\"product_id\"]")[0];
            HtmlAttribute ProductID = ProductID_Node.Attributes["value"];
            //HtmlNode ProductLink = htmlSnippet.DocumentNode.SelectNodes("//h1[@class=\"item-name\"]")[0];


            HtmlNode Name =  htmlSnippet.DocumentNode.SelectNodes("//h1[@class=\"item-name\"]")[0];
            HtmlNode Brand =  htmlSnippet.DocumentNode.SelectNodes("//div[@class=\"item-brand\"]")[0];
            HtmlNode Price_tmp =  htmlSnippet.DocumentNode.SelectNodes("//span[@id=\"span-price\"]")[0];
            string Price = Price_tmp.InnerText.Trim();
            Price = Price.Remove(Price.Length - 2).Trim();

            //get star point
            HtmlNode TotalReviewPoint =  htmlSnippet.DocumentNode.SelectNodes("//p[@class=\"total-review-point\"]")[0];

            

            product = new Product(ProductID.Value ,"", Name.InnerText.Trim(), Brand.InnerText.Trim().Substring(15).Trim(), Price,TotalReviewPoint.InnerText);
            
            return product;
        }

        // get all link
        private List<string> ExtractAllAHrefTags(HtmlDocument htmlSnippet)
        {
            List<string> hrefTags = new List<string>();
            foreach (HtmlNode link in htmlSnippet.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute att = link.Attributes["href"];
                hrefTags.Add(att.Value);
            }
            return hrefTags;
        }


        List<string> ValueFilter = new List<string>();// list values had block
        


        private List<string> FilterAll(List<string> allLink)
        {
            List<string> link = new List<string>();
            foreach(string item in allLink)
            {
                if (!item.StartsWith("http://tiki.vn/"))
                {
                    if (item.StartsWith("#") || item.StartsWith("javascript"))
                    {
                        // bỏ qua
                    }
                    else
                    {
                        string value = "http://tiki.vn" + item;
                        link.Add(value);
                    }
                }
                else
                {
                    link.Add(item);
                }
            }

            return link;
        }

        

        // GET: GetTiki
        public void Index()
        {
            ViewBag.Message = "Geting data from server...!";
            List<string> linkTags;

            string link = "http://tiki.vn/philips-e330-2-sim-p218950.html?ref=c1789.c1793.c1796.c2051.c3672.c3737.c3843.c4763.c4921.c5055.c5093.c4709.c4861.c4214.c4946.c4947.";
            //string link = "http://www.tiki.vn/";
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(link); ;
            linkTags = FilterAll(ExtractAllAHrefTags(doc));

            Product pro = GetProductInformations(doc);

            ViewBag.Html = "";
        }
    }
}