using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Model.Impl;
using BinkyRailways.Core.Util;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.Import.Rocrail
{
    internal sealed class ModuleImporter : LocsImporter
    {
        internal const int GridSize = 24;
        private const string EdgePrefix = "point-";

        private readonly Dictionary<string, IJunction> junctions = new Dictionary<string, IJunction>();
        private readonly Dictionary<string, ISensor> feedbacks = new Dictionary<string, ISensor>();
        private readonly Dictionary<string, IBlock> blocks = new Dictionary<string, IBlock>();
        private readonly Dictionary<string, IRoute> routes = new Dictionary<string, IRoute>();
        private readonly Dictionary<string, IEdge> edges = new Dictionary<string, IEdge>();

        /// <summary>
        /// Importer for modules.
        /// </summary>
        public ModuleImporter(XElement root, string path)
            : base(root, path)
        {
        }

        /// <summary>
        /// Import the module.
        /// </summary>
        public new IModule Import(IPackage package)
        {
            // Create module
            var module = package.AddNewModule();
            module.Description = System.IO.Path.GetFileNameWithoutExtension(Path);

            // Import individual objects
            var railway = package.Railway;
            ImportSwitches(module);
            ImportFeedbacks(module);
            ImportBlocks(module);
            ImportRoutes(module, railway);
            ImportLocs(package, railway);

            // Import the track image
            ImportTrack(module);

            // Success
            railway.Modules.Add(module);

            return module;
        }

        /// <summary>
        /// Import all junctions
        /// </summary>
        private void ImportSwitches(IModule module)
        {
            var swlist = Root.Element("swlist");
            if (swlist == null) 
                return;

            foreach (var sw in swlist.Elements("sw"))
            {
                var id = sw.GetAttributeValue("id", string.Empty);
                if (string.IsNullOrEmpty(id))
                {
                    Message("Switch found without an id");
                    continue;
                }
                if (junctions.ContainsKey(id.ToLower()))
                {
                    Message("Duplicate switch found ({0})", id);
                    continue;
                }

                var type = sw.GetAttributeValue("type");
                if (!((type == "left") || (type == "right") || (type == "crossing") || (type == "dcrossing") || (type == "threeway")))
                {
                    Message("Switch ({0}) with unsupported type \"{1}\" found", id, type);
                    continue;
                }

                var x = sw.GetIntAttributeValue("x");
                var y = sw.GetIntAttributeValue("y");
                var port1 = sw.GetIntAttributeValue("port1");

                var ewidth = ((type == "dcrossing") || (type == "crossing")) ? GridSize * 2 : GridSize;
                var eheight = GridSize;

                //var accessoryctrl = sw.Element("accessoryctrl");
                var isPassive = (port1 <= 0);

                var entity = isPassive ? 
                    (IJunction)module.Junctions.AddPassiveJunction() : 
                    module.Junctions.AddSwitch();
                entity.Description = id;
                entity.X = x * GridSize + (ewidth - entity.Width) / 2;
                entity.Y = y * GridSize + (eheight  - entity.Height) / 2;
                SetRotation(entity, sw.GetAttributeValue("ori"), true, ewidth, eheight);

                var @switch = entity as ISwitch;
                if (@switch != null)
                {
                    @switch.HasFeedback = false;

                    if (port1 > 0)
                    {
                        @switch.Address = new Address(AddressType.LocoNet, null, port1);
                    }
                }

                // Record
                junctions[id.ToLower()] = entity;
            }
        }

        /// <summary>
        /// Import all feedbacks
        /// </summary>
        private void ImportFeedbacks(IModule module)
        {
            var fblist = Root.Element("fblist");
            if (fblist == null)
                return;

            foreach (var fb in fblist.Elements("fb"))
            {
                var id = fb.GetAttributeValue("id", string.Empty);
                if (string.IsNullOrEmpty(id))
                {
                    Message("Feedback found without an id");
                    continue;
                }
                if (feedbacks.ContainsKey(id.ToLower()))
                {
                    Message(string.Format("Duplicate feedback found ({0})", id));
                    continue;
                }

                var x = fb.GetIntAttributeValue("x");
                var y = fb.GetIntAttributeValue("y");
                var addr = fb.GetIntAttributeValue("addr");

                var entity = module.Sensors.AddNewBinarySensor();
                entity.Description = id;
                entity.X = x * GridSize + (GridSize - entity.Width) / 2;
                entity.Y = y * GridSize + (GridSize - entity.Height) / 2;
                SetRotation(entity, fb.GetAttributeValue("ori"), true, GridSize, GridSize);

                if (addr > 0)
                {
                    entity.Address = new Address(AddressType.LocoNet, null, addr);
                }

                // Record
                feedbacks[id.ToLower()] = entity;
            }
        }

        /// <summary>
        /// Import all blocks
        /// </summary>
        private void ImportBlocks(IModule module)
        {
            var bklist = Root.Element("bklist");
            if (bklist == null)
                return;

            foreach (var bk in bklist.Elements("bk"))
            {
                var id = bk.GetAttributeValue("id", string.Empty);
                if (string.IsNullOrEmpty(id))
                {
                    Message("Block found without an id");
                    continue;
                }
                if (blocks.ContainsKey(id.ToLower()))
                {
                    Message(string.Format("Duplicate block found ({0})", id));
                    continue;
                }

                var x = bk.GetIntAttributeValue("x");
                var y = bk.GetIntAttributeValue("y");

                var entity = module.Blocks.AddNew();
                entity.Description = id;
                entity.Width = GridSize * 4;
                entity.X = x * GridSize;
                entity.Y = y * GridSize + (GridSize - entity.Height) / 2;
                SetRotation(entity, bk.GetAttributeValue("ori"), true, GridSize * 4, GridSize);

                // Setup waiting
                entity.WaitProbability = (bk.GetAttributeValue("wait") == "true") ? 100 : 0;
                entity.MinimumWaitTime = bk.GetIntAttributeValue("minwaittime");
                entity.MaximumWaitTime = bk.GetIntAttributeValue("maxwaittime");
                entity.ChangeDirection = (bk.GetAttributeValue("terminalstation") == "true") ? ChangeDirection.Allow : ChangeDirection.Avoid;

                // Record
                blocks[id.ToLower()] = entity;
            }
        }

        /// <summary>
        /// Import all routes
        /// </summary>
        private void ImportRoutes(IModule module, IRailway railway)
        {
            var stlist = Root.Element("stlist");
            if (stlist == null)
                return;

            foreach (var st in stlist.Elements("st"))
            {
                var id = st.GetAttributeValue("id", string.Empty);
                if (string.IsNullOrEmpty(id))
                {
                    Message("Route found without an id");
                    continue;
                }
                if (routes.ContainsKey(id.ToLower()))
                {
                    Message(string.Format("Route block found ({0})", id));
                    continue;
                }

                var entity = module.Routes.AddNew();
                var bka = st.GetAttributeValue("bka");
                if (string.IsNullOrEmpty(bka))
                {
                    Message(string.Format("Route \"{0}\" has empty 'From' block ", id));
                }
                else
                {
                    entity.From = GetEndPointForRoute(module, bka);
                }
                var bkb = st.GetAttributeValue("bkb");
                if (string.IsNullOrEmpty(bka))
                {
                    Message(string.Format("Route \"{0}\" has empty 'To' block ", id));
                }
                else
                {
                    entity.To = GetEndPointForRoute(module, bkb);
                }

                // Import switch states
                foreach (var swcmd in st.Elements("swcmd"))
                {
                    var swid = swcmd.GetAttributeValue("id");
                    if (string.IsNullOrEmpty(swid))
                    {
                        Message(string.Format("Switch command found without an id in route \"{0}\"", id));
                        continue;
                    }

                    IJunction junction;
                    if (!junctions.TryGetValue(swid.ToLower(), out junction))
                    {
                        Message(string.Format("No switch found with id \"{0}\" in route \"{1}\"", swid, id));
                        continue;                        
                    }

                    var @switch = junction as ISwitch;
                    var passiveJunction = junction as IPassiveJunction;
                    if (@switch != null)
                    {
                        var cmd = swcmd.GetAttributeValue("cmd");
                        switch (cmd)
                        {
                            case "straight":
                                entity.CrossingJunctions.Add(@switch, SwitchDirection.Straight);
                                break;
                            case "turnout":
                                entity.CrossingJunctions.Add(@switch, SwitchDirection.Off);
                                break;
                            default:
                                Message(
                                    string.Format("Switch command found with unknown cmd \"{0}\" in route \"{1}\"", cmd,
                                                  id));
                                break;
                        }
                    }
                    else if (passiveJunction != null)
                    {
                        entity.CrossingJunctions.Add(passiveJunction);
                    }
                }

                // Get target block
                var bk = GetBlockElement(bkb);
                if (bk != null)
                {
                    // Import terminalstation -> permission
                    if (bk.GetAttributeValue("terminalstation") == "true")
                    {
                        // Avoid trains that cannot reverse
                        var predicate = railway.PredicateBuilder.CreateCanChangeDirection();
                        entity.Permissions.Includes.Predicates.Add(predicate);
                    }

                    // Import events
                    foreach (var fbevent in bk.Elements("fbevent"))
                    {
                        var from = fbevent.GetAttributeValue("from");
                        if (!((from == "all") || (from == bka)))
                            continue;

                        var fbid = fbevent.GetAttributeValue("id");
                        var action = fbevent.GetAttributeValue("action");

                        ISensor fb;
                        if (!feedbacks.TryGetValue(fbid, out fb))
                        {
                            Message(string.Format("Unknown feedback '{0}' in fbevent of block '{1}'", fbid, bkb));
                            continue;
                        }

                        switch (action)
                        {
                            case "enter":
                                {
                                    var @event = entity.Events.Add(fb);
                                    var b = @event.Behaviors.Add();
                                    b.StateBehavior = RouteStateBehavior.Enter;
                                }
                                break;
                            case "in":
                                {
                                    var @event = entity.Events.Add(fb);
                                    var b = @event.Behaviors.Add();
                                    b.StateBehavior = RouteStateBehavior.Reached;
                                }
                                break;
                            default:
                                Message(string.Format("Unsupport action '{0}' in fbevent of block '{1}'", action, bkb));
                                break;
                        }
                    }
                }

                // Record
                routes[id.ToLower()] = entity;
            }
        }

        /// <summary>
        /// Import the track image
        /// </summary>
        private void ImportTrack(IModule module)
        {
            var elements = PositionElements.ToList();

            var width = Math.Min(200 * GridSize, Math.Max(module.Width, elements.Max(x => (x.GetIntAttributeValue("x") + 1) * GridSize)));
            var height = Math.Min(200 * GridSize, Math.Max(module.Height, elements.Max(x => (x.GetIntAttributeValue("y") + 1) * GridSize)));

            using (var bitmap = new Bitmap(1, 1))
            {
                using (var outerGraphics = Graphics.FromImage(bitmap))
                {
                    var refHdc = outerGraphics.GetHdc();
                    var metaFile = new Metafile(refHdc, new Rectangle(0, 0, width, height), MetafileFrameUnit.Pixel, EmfType.EmfPlusDual);
                    try
                    {
                        const int h = GridSize/2;
                        using (var g = Graphics.FromImage(metaFile))
                        {
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            using (var pen = new Pen(Color.Black, 2))
                            {
                                foreach (var tk in elements)
                                {
                                    var type = tk.GetAttributeValue("type", string.Empty);
                                    var x = tk.GetIntAttributeValue("x");
                                    var y = tk.GetIntAttributeValue("y");
                                    var ori = tk.GetAttributeValue("ori");

                                    if ((x < 0) || (y < 0))
                                        continue;

                                    var ewidth = GridSize;
                                    var eheight = GridSize;
                                    if ((tk.Name.LocalName == "sw") && ((type == "crossing") || (type == "dcrossing")))
                                        ewidth = GridSize*2;

                                    // Transform so we can always draw on 0,0 in 'west' direction
                                    var state = g.Save();
                                    switch (ori)
                                    {
                                        case "south":
                                            g.RotateTransform(90);
                                            g.TranslateTransform(eheight, 0, MatrixOrder.Append);
                                            break;
                                        case "north":
                                            g.RotateTransform(270);
                                            g.TranslateTransform(0, ewidth, MatrixOrder.Append);
                                            break;
                                        case "east":
                                            g.RotateTransform(180);
                                            g.TranslateTransform(ewidth, eheight, MatrixOrder.Append);
                                            break;
                                    }
                                    g.TranslateTransform(x*GridSize, y*GridSize, MatrixOrder.Append);

                                    if ((tk.Name.LocalName == "fb") && (tk.GetAttributeValue("curve") == "true"))
                                        type = "curve";

                                    switch (tk.Name.LocalName + "-" + type)
                                    {
                                        default:
                                        case "tk-straight":
                                        case "tk-dir":
                                        case "tk-dirall":
                                            g.DrawLine(pen, 0, h, GridSize, h);
                                            break;
                                        case "sw-dcrossing":
                                            g.DrawLine(pen, 0, h, GridSize*2, h);
                                            break;
                                        case "sw-left":
                                            g.DrawLine(pen, 0, h, GridSize, h);
                                            g.DrawLine(pen, h, GridSize, GridSize, h);
                                            break;
                                        case "sw-right":
                                            g.DrawLine(pen, 0, h, GridSize, h);
                                            g.DrawLine(pen, 0, h, h, GridSize);
                                            break;
                                        case "sw-crossing":
                                            g.DrawLine(pen, 0, h, GridSize*2, h);
                                            if (tk.GetAttributeValue("dir") == "true")
                                            {
                                                g.DrawLine(pen, h, 0, GridSize + h, GridSize);
                                            }
                                            else
                                            {
                                                g.DrawLine(pen, h, GridSize, GridSize + h, 0);
                                            }
                                            break;
                                        case "fb-curve":
                                        case "tk-curve":
                                            g.DrawLine(pen, 0, h, h, GridSize);
                                            break;
                                        case "tk-connector":
                                            g.DrawLine(pen, 0, h, h, h);
                                            var r = h/3;
                                            g.DrawArc(pen, h - r/2, h - r/2, r, r, 0, 360);
                                            break;
                                        case "tk-buffer":
                                            g.DrawLine(pen, 0, h, h, h);
                                            g.DrawLine(pen, h, h/2, h, h + h/2);
                                            break;
                                    }

                                    g.Restore(state);
                                }
                            }
                        }
                    }
                    finally
                    {
                        outerGraphics.ReleaseHdc(refHdc);
                    }

                    // Set image
                    var stream = new MemoryStream();
                    metaFile.SaveAsEmf(stream);
                    stream.Position = 0;
                    module.BackgroundImage = stream;
                }
            }
        }

        /// <summary>
        /// Gets an endpoint with the given id.
        /// </summary>
        private IEndPoint GetEndPointForRoute(IModule module, string id)
        {
            if (id == null)
                return null;
            var key = id.ToLower();
            IBlock block;
            if (blocks.TryGetValue(key, out block))
                return block;
            if (key.StartsWith(EdgePrefix))
            {
                // Assume it's an edge
                IEdge edge;
                if (edges.TryGetValue(key, out edge))
                    return edge;

                // Not found, create edge
                return CreateEdge(module, key);
            }
            Message(string.Format("No block or edge found with id \"{0}\"", id));
            return null;
        }

        
        /// <summary>
        /// Gets the XML element representing a block with the given id.
        /// </summary>
        private XElement GetBlockElement(string id)
        {
            if (id == null)
                return null;
            id = id.ToLower();
            return Root.Descendants("bk").FirstOrDefault(x => x.GetAttributeValue("id", string.Empty).ToLower() == id);
        }

        /// <summary>
        /// Create a new edge.
        /// </summary>
        private IEdge CreateEdge(IModule module, string key)
        {
            var edge = module.Edges.AddNew();
            edge.Description = key;

            // Determine position
            var modWidth = PositionedEntities.Max(x => (x.X + x.Width));
            var modHeight = PositionedEntities.Max(x => (x.Y + x.Height));
            var elements = PositionElements.ToList();
            modWidth = Math.Max(modWidth, elements.Max(x => (x.GetIntAttributeValue("x") + 1) * GridSize));
            modHeight = Math.Max(modHeight, elements.Max(x => (x.GetIntAttributeValue("y") + 1) * GridSize));            

            // Find connectors
            var connectors = Root.Descendants("tk").Where(x => x.GetAttributeValue("type") == "connector");
            XElement conn = null;

            switch (key.Substring(EdgePrefix.Length))
            {
                case "ne":
                    conn = FindBestConnector(connectors, "north", x => int.MaxValue - x.GetIntAttributeValue("y"), x => x.GetIntAttributeValue("x"), false);
                    edge.X = (modWidth / 3) * 1;
                    edge.Y = 0;
                    break;
                case "nw":
                    conn = FindBestConnector(connectors, "north", x => int.MaxValue - x.GetIntAttributeValue("y"), x => x.GetIntAttributeValue("x"), true);
                    edge.X = (modWidth / 3) * 2;
                    edge.Y = 0;
                    break;
                case "se":
                    conn = FindBestConnector(connectors, "south", x => x.GetIntAttributeValue("y"), x => x.GetIntAttributeValue("x"), false);
                    edge.X = (modWidth / 3) * 1;
                    edge.Y = modHeight;
                    break;
                case "sw":
                    conn = FindBestConnector(connectors, "south", x => x.GetIntAttributeValue("y"), x => x.GetIntAttributeValue("x"), true);
                    edge.X = (modWidth / 3) * 2;
                    edge.Y = modHeight;
                    break;
                case "wn":
                    conn = FindBestConnector(connectors, "east", x => int.MaxValue - x.GetIntAttributeValue("x"), x => x.GetIntAttributeValue("y"), true);
                    edge.X = 0;
                    edge.Y = (modHeight / 3) * 1;
                    break;
                case "ws":
                    conn = FindBestConnector(connectors, "east", x => int.MaxValue - x.GetIntAttributeValue("x"), x => x.GetIntAttributeValue("y"), false);
                    edge.X = 0;
                    edge.Y = (modHeight / 3) * 2;
                    break;
                case "en":
                    conn = FindBestConnector(connectors, "west", x => x.GetIntAttributeValue("x"), x => x.GetIntAttributeValue("y"), true);
                    edge.X = modWidth;
                    edge.Y = (modHeight / 3) * 1;
                    break;
                case "es":
                    conn = FindBestConnector(connectors, "west", x => x.GetIntAttributeValue("x"), x => x.GetIntAttributeValue("y"), false);
                    edge.X = modWidth;
                    edge.Y = (modHeight / 3) * 2;
                    break;
                default:
                    Message(string.Format("Unknown edge position ({0})", key));
                    break;
            }

            if (conn != null)
            {
                var x = conn.GetIntAttributeValue("x");
                var y = conn.GetIntAttributeValue("y");
                edge.X = x * GridSize + (GridSize - edge.Width) / 2;
                edge.Y = y * GridSize + (GridSize - edge.Height) / 2;
            }

            edges[key] = edge;
            return edge;
        }

        private XElement FindBestConnector(IEnumerable<XElement> all, string orientation, Func<XElement, int> priorityFilter, Func<XElement, int> orderBy, bool first)
        {
            var filtered = all.Where(x => (x.GetAttributeValue("ori", "west") == orientation)).ToList();

            if (filtered.Count == 0)
                return null;
            var max = filtered.Max(priorityFilter);
            var maxSet = filtered.Where(x => priorityFilter(x) == max).OrderBy(orderBy).ToList();
            if (maxSet.Count == 0)
                return null;
            if ((first) || (maxSet.Count == 1))
                return maxSet[0];
            return maxSet[1];
        }

        /// <summary>
        /// Gets all rocrail elements that can have a position.
        /// </summary>
        private IEnumerable<XElement> PositionElements
        {
            get
            {
                var root = Root;
                return root.Descendants("tk").Concat(root.Descendants("fb")).Concat(root.Descendants("sw")).Concat(root.Descendants("sg"));
            }
        }

        /// <summary>
        /// Gets all positioned entities that were imported.
        /// </summary>
        private IEnumerable<IPositionedEntity> PositionedEntities
        {
            get { return junctions.Values.Cast<IPositionedEntity>().Concat(feedbacks.Values).Concat(blocks.Values); }
        }

        /// <summary>
        /// Convert orientation to rotation
        /// </summary>
        private static void SetRotation(IPositionedEntity entity, string ori, bool northWestOnly, int ewidth, int eheight)
        {
            if (northWestOnly)
            {
                if (ori == "east") ori = "west";
                if (ori == "south") ori = "north";
            }
            switch (ori)
            {
                case "east":
                    entity.Rotation = 180;
                    break;
                case "north":
                    entity.Rotation = 270;
                    // Adjust for rotation around center
                    entity.X -= (ewidth - eheight) / 2;
                    entity.Y += (ewidth - eheight) / 2;
                    break;
                case "south":
                    entity.Rotation = 90;
                    // Adjust for rotation around center
                    entity.X -= (ewidth - eheight) / 2;
                    entity.Y += (ewidth - eheight) / 2;
                    break;
            }
        }
    }
}
