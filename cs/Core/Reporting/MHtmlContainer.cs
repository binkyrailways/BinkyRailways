using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace BinkyRailways.Core.Reporting
{
    /// <summary>
    /// Helper class used to construct mhtml files.
    /// </summary>
    internal sealed class MHtmlContainer
    {
        private readonly List<Part>  parts = new List<Part>();

        /// <summary>
        /// Add a content part.
        /// </summary>
        public void AddPart(string contentType, string contentId, byte[] data)
        {
            var part = new Part(contentType, contentId, data);
            parts.Add(part);
        }

        /// <summary>
        /// Write the entire container to a file with the given path.
        /// </summary>
        public void WriteTo(string path, XElement html)
        {
            var boundary = "boundary-1";
            using (var writer = new StreamWriter(path, false, Encoding.ASCII))
            {
                writer.WriteLine("From: \"Saved by {0}\"", Assembly.GetEntryAssembly().GetName().Name);
                writer.WriteLine("Subject: ");
                writer.WriteLine("MIME-Version: 1.0");
                writer.WriteLine("Content-Type: multipart/related;\n\ttype=text/html;\n\tboundary=\"{0}\"", boundary);
                writer.WriteLine();
                writer.WriteLine("This is a multi-part message in MIME format.");
                writer.WriteLine();
                writer.WriteLine("--" + boundary);

                // Create HTML part
                var settings = new XmlWriterSettings { Encoding = Encoding.Unicode , OmitXmlDeclaration = true };
                var memStream = new MemoryStream();
                using (var xmlWriter = XmlWriter.Create(memStream, settings))
                {
                    xmlWriter.WriteDocType("html", "-//W3C//DTD HTML 4.0 Transitional//EN", null, null);
                    html.WriteTo(xmlWriter);
                }

                // Write HTML part
                var htmlPart = new Part("text/html;\n\tcharset=\"unicode\"", null, memStream.ToArray());
                htmlPart.WriteTo(writer, boundary, (parts.Count == 0));

                // Writer other parts
                foreach (var part in parts)
                {
                    part.WriteTo(writer, boundary, (parts.Last() == part));
                }
            }
        }

        private sealed class Part
        {
            private const int LineLength = 76;

            private readonly string contentType;
            private readonly string contentId;
            private readonly byte[] data;

            /// <summary>
            /// Default ctor
            /// </summary>
            public Part(string contentType, string contentId, byte[] data)
            {
                this.contentType = contentType;
                this.contentId = contentId;
                this.data = data;
            }

            /// <summary>
            /// Write this part to the given writer
            /// </summary>
            public void WriteTo(TextWriter writer, string boundary, bool isLastPart)
            {
                writer.WriteLine("Content-Type: {0}", contentType);
                writer.WriteLine("Content-Transfer-Encoding: base64");
                if (!string.IsNullOrEmpty(contentId)) 
                    writer.WriteLine("Content-ID: <{0}>", contentId);
                writer.WriteLine();

                var base64 = Convert.ToBase64String(data);
                while (base64.Length > 0)
                {
                    if (base64.Length > LineLength)
                    {
                        writer.WriteLine(base64.Substring(0, LineLength));
                        base64 = base64.Substring(LineLength);
                    }
                    else
                    {
                        writer.WriteLine(base64);
                        base64 = string.Empty;
                    }
                }
                writer.WriteLine();
                writer.WriteLine("--" + boundary + (isLastPart ? "--" : string.Empty));
            }
        }
    }
}
