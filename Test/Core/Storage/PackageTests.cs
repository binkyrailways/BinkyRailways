using System.IO;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Storage;
using NUnit.Framework;

namespace BinkyRailways.Test.Core.Storage
{
    [TestFixture]
    public class PackageTests
    {
        /// <summary>
        /// Create a blank new package
        /// </summary>
        [Test]
        public void TestCreateNewPackage()
        {
            IPackage pkg = Package.Create();
            Assert.IsNotEmpty(pkg.Railway.Id, "Railway ID should not be empty");
            Assert.AreEqual(0, pkg.GetLocs().Count(), "There should be 0 locs");
            Assert.AreEqual(0, pkg.GetModules().Count(), "There should be 0 modules");
            Assert.AreEqual(0, pkg.GetCommandStations().Count(), "There should be 0 command stations");
        }

        /// <summary>
        /// Create a blank new package and save it
        /// </summary>
        [Test]
        public void TestCreateAndSaveNewPackage()
        {
            var pkg = Package.Create();
            pkg.Save(GetPath("TestCreateAndSaveNewPackage.mrwp"));
        }

        /// <summary>
        /// Create a blank new package and save it
        /// </summary>
        [Test]
        public void TestCreateAndSaveAndOpenNewPackage()
        {
            var pkg = Package.Create();
            var path = GetPath("TestCreateAndSaveAndOpenNewPackage.mrwp");
            pkg.Save(path);
            var pkg2 = Package.Load(path);
            Assert.AreEqual(pkg.Railway.Id, pkg2.Railway.Id, "Railway id should be the same");
        }

        /// <summary>
        /// Create a blank new package and add a loc to it.
        /// </summary>
        [Test]
        public void TestAddLoc()
        {
            var pkg = Package.Create();
            var path = GetPath("TestAddLoc.mrwp");
            pkg.AddNewLoc();
            pkg.Save(path);
            var pkg2 = Package.Load(path);
            Assert.AreEqual(pkg2.GetLocs().Count(), 1, "1 loc expected");
        }

        /// <summary>
        /// Create a blank new package and add a module to it.
        /// </summary>
        [Test]
        public void TestAddModule()
        {
            var pkg = Package.Create();
            var path = GetPath("TestAddModule.mrwp");
            pkg.AddNewModule();
            pkg.Save(path);
            var pkg2 = Package.Load(path);
            Assert.AreEqual(pkg2.GetModules().Count(), 1, "1 module expected");
        }

        /// <summary>
        /// Create a blank new package and add a module to it.
        /// </summary>
        [Test]
        public void TestModule()
        {
            var pkg = Package.Create();
            var path = GetPath("TestModule.mrwp");
            var module = pkg.AddNewModule();
            var blockA = module.Blocks.AddNew();
            var blockB = module.Blocks.AddNew();
            var blockC = module.Blocks.AddNew();
            var route = module.Routes.AddNew();
            route.From = blockA;
            route.To = blockB;
            var @switch = module.Junctions.AddSwitch();
            module.Edges.AddNew();
            module.Sensors.AddNewBinarySensor();
            route.CrossingJunctions.Add(@switch, SwitchDirection.Straight);

            pkg.Save(path);

            var pkg2 = Package.Load(path);
            Assert.AreEqual(pkg2.GetModules().Count(), 1, "1 module expected");
        }

        /// <summary>
        /// Create a blank new package and add a module to it.
        /// </summary>
        [Test]
        public void TestSwitchWithState()
        {
            var pkg = Package.Create();
            var path = GetPath("TestSwitchWithState.mrwp");
            var module = pkg.AddNewModule();
            var route = module.Routes.AddNew();
            var @switch = module.Junctions.AddSwitch();
            route.CrossingJunctions.Add(@switch, SwitchDirection.Off);
            pkg.Save(path);

            var pkg2 = Package.Load(path);
            Assert.AreEqual(pkg2.GetModules().Count(), 1, "1 module expected");

            var rmodule = pkg2.GetModule(module.Id);
            var rroute = rmodule.Routes.First(x => x.Id == route.Id);
            var rjunc = (ISwitchWithState)rroute.CrossingJunctions.First();
            Assert.AreEqual(rjunc.Direction, SwitchDirection.Off);
        }

        /// <summary>
        /// Gets the full path of a temp file with given name.
        /// </summary>
        private string GetPath(string fileName)
        {
            var folder = Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), "TestOutput");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return Path.Combine(folder, fileName);
        }
    }
}
