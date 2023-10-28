using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.Reporting
{
    public class LocReportBuilder : IReportBuilder
    {
        private readonly IRailway railway;
        private const string CSS = @"
table { border: 1px solid black; width: 95%; }
.hdr { font-weight: bold; }
tr.hdr td { border-bottom: 1px solid black; }
.odd { background-color: lightgray; }
table tr td img { max-width: 150px; }
";

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocReportBuilder(IRailway railway)
        {
            this.railway = railway;
        }

        /// <summary>
        /// Gets the filename extension for this report.
        /// </summary>
        public string ReportExtension { get { return ".mht"; } }

        /// <summary>
        /// Generate a report now in a file with the given path.
        /// </summary>
        public void Generate(string targetPath)
        {
            var container = new MHtmlContainer();

            var doc = new XElement("html");
            var head = doc.AddElement("head");
            var meta = head.AddElement("meta");
            meta.SetAttributeValue("content", "text/html; charset=unicode");
            meta.SetAttributeValue("http-equiv", "Content-Type");
            head.Add(new XElement("style", CSS));
            var body = doc.AddElement("body");
            var heading = body.AddElement("h1", string.Format("Locs in {0}", railway.Description));
            var table = body.AddElement("table");
            table.SetAttributeValue("cellspacing", "0");
            table.SetAttributeValue("cellpadding", "5");

            var hrow = table.AddElement("tr");
            hrow.SetAttributeValue("class", "hdr");
            hrow.Add(new XElement("td", "Image"));
            hrow.Add(new XElement("td", "Name"));
            hrow.Add(new XElement("td", "Address"));
            hrow.Add(new XElement("td", "Change direction"));
            hrow.Add(new XElement("td", "Min. speed"));
            hrow.Add(new XElement("td", "Med. speed"));
            hrow.Add(new XElement("td", "Max. speed"));
            hrow.Add(new XElement("td", "Groups"));

            var odd = false;
            foreach (var loc in railway.GetLocs().OrderBy(x => x.Description))
            {
                var row = table.AddElement("tr");
                row.SetAttributeValue("class", odd ? "odd" : "");

                var image = loc.Image;
                if (image != null)
                {
                    var src = "loc-" + loc.Id + ".png";
                    container.AddPart("image/png", src, image.ToArray());
                    var img = row.AddElement("td").AddElement("img");
                    img.SetAttributeValue("src", "cid:" + src);
                } else
                {
                    // No image
                    row.AddElement("td", "No image");
                }

                row.Add(new XElement("td", loc.Description));
                row.Add(new XElement("td", loc.Address));
                row.Add(new XElement("td", (loc.ChangeDirection == ChangeDirection.Allow) ? "Allow" : "Avoid"));
                row.Add(new XElement("td", string.Format("{0}%", loc.SlowSpeed)));
                row.Add(new XElement("td", string.Format("{0}%", loc.MediumSpeed)));
                row.Add(new XElement("td", string.Format("{0}%", loc.MaximumSpeed)));
                row.Add(new XElement("td", GetGroups(loc)));
                odd = !odd;
            }

            container.WriteTo(targetPath, doc);
        }

        /// <summary>
        /// Gets the groups containing this loc.
        /// </summary>
        private string GetGroups(ILoc loc)
        {
            var groups = railway.LocGroups.Where(x => x.Locs.ContainsId(loc.Id)).ToList();
            if (groups.Count == 0)
                return "-";
            return string.Join(", ", groups.Select(x => x.Description).ToArray());
        }
    }
}
