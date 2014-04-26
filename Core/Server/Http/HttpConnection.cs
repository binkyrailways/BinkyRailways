using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace BinkyRailways.Core.Server.Http
{
    public class HttpConnection
    {
        private const int BufSize = 4096;

        private readonly TcpClient socket;
        private readonly HttpServer srv;

        private Stream inputStream;
        private StreamWriter outputWriter;

        private string httpMethod;
        private string url;
        private string protocolVersionstring;
        private readonly Dictionary<string, string> httpHeaders = new Dictionary<string, string>();


        private const int MaxPostSize = 10 * 1024 * 1024; // 10MB

        internal HttpConnection(TcpClient s, HttpServer srv)
        {
            socket = s;
            this.srv = srv;
        }

        /// <summary>
        /// Gets the response writer.
        /// </summary>
        public StreamWriter Output { get { return outputWriter; } }

        /// <summary>
        /// Gets the URL of this request.
        /// </summary>
        public string Url { get { return url; } }

        private static string StreamReadLine(Stream inputStream)
        {
            var data = "";
            while (true)
            {
                var nextChar = inputStream.ReadByte();
                if (nextChar == '\n') 
                    break; 
                if (nextChar == '\r') 
                    continue;
                if (nextChar == -1)
                {
                    Thread.Sleep(1); continue;
                }
                data += Convert.ToChar(nextChar);
            }
            return data;
        }

        public void Process()
        {
            try
            {
                // we can't use a StreamReader for input, because it buffers up extra data on us inside it's
                // "processed" view of the world, and we want the data raw after the headers
                inputStream = new BufferedStream(socket.GetStream());

                // we probably shouldn't be using a streamwriter for all output from handlers either
                outputWriter = new StreamWriter(new BufferedStream(socket.GetStream()));
                try
                {
                    ParseRequest();
                    ReadHeaders();
                    if (httpMethod.Equals("GET"))
                    {
                        HandleGetRequest();
                    }
                    else if (httpMethod.Equals("POST"))
                    {
                        HandlePostRequest();
                    }
                }
                catch (Exception e)
                {
                    //Console.WriteLine("Exception: " + e);
                    WriteFailure();
                }
                outputWriter.Flush();
            }
            finally
            {
                // bs.Flush(); // flush any remaining output
                inputStream = null;
                outputWriter = null; // bs = null;            
                socket.Close();
            }
        }

        private void ParseRequest()
        {
            var request = StreamReadLine(inputStream);
            var tokens = request.Split(' ');
            if (tokens.Length != 3)
            {
                throw new Exception("invalid http request line");
            }
            httpMethod = tokens[0].ToUpper();
            url = tokens[1];
            protocolVersionstring = tokens[2];

            //Console.WriteLine("starting: " + request);
        }

        private void ReadHeaders()
        {
            //Console.WriteLine("readHeaders()");
            String line;
            while ((line = StreamReadLine(inputStream)) != null)
            {
                if (line.Length == 0)
                {
                    //Console.WriteLine("got headers");
                    return;
                }

                var separator = line.IndexOf(':');
                if (separator == -1)
                {
                    throw new Exception("invalid http header line: " + line);
                }
                var name = line.Substring(0, separator);
                var pos = separator + 1;
                while ((pos < line.Length) && (line[pos] == ' '))
                {
                    pos++; // strip any spaces
                }

                var value = line.Substring(pos, line.Length - pos);
                //Console.WriteLine("header: {0}:{1}", name, value);
                httpHeaders[name] = value;
            }
        }

        /// <summary>
        /// Process GET requests
        /// </summary>
        private void HandleGetRequest()
        {
            srv.HandleGetRequest(this);
        }

        /// <summary>
        /// Process POST requests
        /// </summary>
        private void HandlePostRequest()
        {
            // this post data processing just reads everything into a memory stream.
            // this is fine for smallish things, but for large stuff we should really
            // hand an input stream to the request processor. However, the input stream 
            // we hand him needs to let him see the "end of the stream" at this content 
            // length, because otherwise he won't know when he's seen it all! 

            //Console.WriteLine("get post data start");
            var ms = new MemoryStream();
            string sValue;
            int contentLen;
            if (httpHeaders.TryGetValue("Content-Length", out sValue) && int.TryParse(sValue, out contentLen))
            {
                if (contentLen > MaxPostSize)
                {
                    throw new Exception(string.Format("POST Content-Length({0}) too big for this simple server", contentLen));
                }
                var buf = new byte[BufSize];
                var toRead = contentLen;
                while (toRead > 0)
                {
                    //Console.WriteLine("starting Read, to_read={0}", toRead);

                    var numread = inputStream.Read(buf, 0, Math.Min(BufSize, toRead));
                    //Console.WriteLine("read finished, numread={0}", numread);
                    if (numread == 0)
                    {
                        if (toRead == 0)
                            break;
                        throw new Exception("client disconnected during post");
                    }
                    toRead -= numread;
                    ms.Write(buf, 0, numread);
                }
                ms.Seek(0, SeekOrigin.Begin);
            }
            //Console.WriteLine("get post data end");
            srv.HandlePostRequest(this, new StreamReader(ms));

        }

        /// <summary>
        /// Write a "success" response header.
        /// </summary>
        public void WriteSuccess(string contentType, DateTime lastModified, CacheControl cacheControl)
        {
            outputWriter.WriteLine("HTTP/1.0 200 OK");
            if (!string.IsNullOrEmpty(contentType))
                outputWriter.WriteLine("Content-Type: " + contentType);
            if (lastModified != DateTime.MinValue)
                outputWriter.WriteLine("Last-Modified: " + lastModified.ToUniversalTime().ToString("r"));
            switch (cacheControl)
            {
                case CacheControl.Private:
                    outputWriter.WriteLine("cache-control: private");
                    break;
                case CacheControl.Public:
                    outputWriter.WriteLine("cache-control: public");
                    break;
                case CacheControl.NoCache:
                    outputWriter.WriteLine("cache-control: no-cache");
                    break;
            }
            outputWriter.WriteLine("Connection: close");
            outputWriter.WriteLine("");
        }

        /// <summary>
        /// Write a "404" response header.
        /// </summary>
        public void WriteFailure()
        {
            outputWriter.WriteLine("HTTP/1.0 404 File not found");
            outputWriter.WriteLine("Connection: close");
            outputWriter.WriteLine("");
        }

        /// <summary>
        /// Flush the output writer.
        /// </summary>
        public void Flush()
        {
            outputWriter.Flush();
        }
    }
}