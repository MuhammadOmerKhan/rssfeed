using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
namespace RSSFeed.Controllers
{
    public class RSSActionResult : ActionResult
    {
        public SyndicationFeed Feed { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/rss+xml";

            Rss20FeedFormatter rssFormatter = new Rss20FeedFormatter(Feed);
            //This line enables Atom attribute in the header of RSS.
            rssFormatter.SerializeExtensionsAsAtom = false;
            using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output))
            {
                rssFormatter.WriteTo(writer);
            }
        }
    }
    public class HomeController : Controller
    {
        public RSSActionResult RSSFeed()
        {
            //This line creates the title, description and link of your website.
            SyndicationFeed feed = new SyndicationFeed("Muhammad Omer Khan", "This project allows you to create RSS Feed in ASP.NET MVC", new Uri("http://www.muhammadomerkhan.com"));

            //This line helps you to create rss feed of your website.
            List<SyndicationItem> items = new List<SyndicationItem>();
            SyndicationItem item = new SyndicationItem("Test", "ABCD", new Uri("http://www.google.com"));
            items.Add(item);
            feed.Items = items;

            return new RSSActionResult() { Feed = feed };
        }
    }
}