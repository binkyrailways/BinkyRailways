using System;
using System.Collections.Generic;
using System.Linq;
using BinkyRailways.Core.Model.Impl;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Module extensions methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Gets the description of the given entity with it's module description as prefix.
        /// </summary>
        public static string GlobalDescription(this IModuleEntity entity)
        {
            var prefix = (entity.Module != null) ? entity.Module.ToString() : "?";
            return prefix + "." + entity;
        }

        /// <summary>
        /// Make sure this entity has a unique ID.
        /// </summary>
        internal static void EnsureId(this IEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = Entity.UniqueId();
            }
        }

        /// <summary>
        /// Is the given route internal to its module?
        /// That is, it goes from a block to another block.
        /// </summary>
        public static bool IsInternal(this IRoute route)
        {
            return (route.From is IBlock) && (route.To is IBlock);
        }

        /// <summary>
        /// Is the starting endpoint of given route internal to its module?
        /// That is, it goes from a block to an edge.
        /// </summary>
        public static bool IsFromBlockToEdge(this IRoute route)
        {
            return (route.From is IBlock) && (route.To is IEdge);
        }

        /// <summary>
        /// Is the destination endpoint of given route internal to its module?
        /// That is, it goes from an edge to another block.
        /// </summary>
        public static bool IsFromEdgeToBlock(this IRoute route)
        {
            return (route.From is IEdge) && (route.To is IBlock);
        }

        /// <summary>
        /// Find the route that is the reverse of the given route.
        /// </summary>
        /// <returns>Null if not found</returns>
        public static IRoute GetReverse(this IRoute route)
        {
            var from = route.To;
            var to = route.From;
            var module = route.Module;

            if ((from == null) || (to == null) || (module == null))
                return null;

            var reverseRoutes = module.Routes.Where(x => (x.From == from) && (x.To == to));
            foreach (var iterator in reverseRoutes)
            {
                // Look for correct block sides (in case of blocks)
                if (from is IBlock)
                {
                    // From side should match
                    if (route.FromBlockSide != iterator.ToBlockSide)
                    {
                        // No match
                        continue;
                    }
                }
                if (to is IBlock)
                {
                    // To side should match
                    if (route.ToBlockSide != iterator.FromBlockSide)
                    {
                        // No match
                        continue;
                    }
                }
                // We found it
                return iterator;
            }
            // Not found
            return null;
        }

        /// <summary>
        /// Add a route that is the reverse of the given route.
        /// </summary>
        /// <returns>Null if no reverse can be created.</returns>
        public static IRoute AddReverse(this IRoute route)
        {
            var result = GetReverse(route);
            if (result != null)
                return result;

            var from = route.To;
            var to = route.From;
            var module = route.Module;

            if ((from == null) || (to == null) || (module == null))
                return null;

            result = module.Routes.AddNew();
            result.From = from;
            result.FromBlockSide = route.ToBlockSide;
            result.To = to;
            result.ToBlockSide = route.FromBlockSide;
            result.Speed = route.Speed;
            route.CrossingJunctions.CopyTo(result.CrossingJunctions);
            return result;
        }

        /// <summary>
        /// Is the destination endpoint of given route internal to its module?
        /// That is, it goes from a block/edge to another block.
        /// </summary>
        public static bool IsToInternal(this IRoute route)
        {
            return (route.To is IBlock);
        }

        /// <summary>
        /// Gets all positioned entities in the given module.
        /// </summary>
        public static IEnumerable<IPositionedEntity> GetPositionedEntities(this IModule module)
        {
            return module.Blocks.Cast<IPositionedEntity>()
                .Concat(module.Edges.Cast<IPositionedEntity>())
                .Concat(module.Junctions.Cast<IPositionedEntity>())
                .Concat(module.Sensors.Cast<IPositionedEntity>())
                .Concat(module.Signals.Cast<IPositionedEntity>())
                .Concat(module.Outputs.Cast<IPositionedEntity>());
        }

        /// <summary>
        /// Gets the inverted side.
        /// </summary>
        public static  BlockSide Invert(this BlockSide value)
        {
            return (value == BlockSide.Front) ? BlockSide.Back : BlockSide.Front;
        }

        /// <summary>
        /// Gets the inverted direction.
        /// </summary>
        public static SwitchDirection Invert(this SwitchDirection value)
        {
            return (value == SwitchDirection.Straight) ? SwitchDirection.Off : SwitchDirection.Straight;
        }

        /// <summary>
        /// Does the given type require a numeric value.
        /// </summary>
        public static bool RequiresNumericValue(this AddressType addressType)
        {
            switch (addressType)
            {
                case AddressType.Dcc:
                case AddressType.LocoNet:
                case AddressType.Motorola:
                case AddressType.Mfx:
                    return true;
                case AddressType.BinkyNet:
                    return false;
                default:
                    throw new ArgumentException("Unknown address type " + addressType);
            }
        }

        /// <summary>
        /// Gets the maximum possible value for an address of the given type.
        /// </summary>
        public static int MaxValue(this AddressType addressType)
        {
            switch (addressType)
            {
                case AddressType.Dcc:
                    return 9999; // 0x27FF;
                case AddressType.LocoNet:
                    return 4096;
                case AddressType.Motorola:
                    return 80;
                case AddressType.Mfx:
                    return 16000;
                case AddressType.BinkyNet:
                    return 0;
                default:
                    throw new ArgumentException("Unknown address type " + addressType);
            }
        }

        /// <summary>
        /// Gets the minimum possible value for an address of the given type.
        /// </summary>
        public static int MinValue(this AddressType addressType)
        {
            switch (addressType)
            {
                case AddressType.Dcc:
                    return 0;
                case AddressType.LocoNet:
                    return 1;
                case AddressType.Motorola:
                    return 1;
                case AddressType.Mfx:
                    return 1;
                case AddressType.BinkyNet:
                    return 0;
                default:
                    throw new ArgumentException("Unknown address type " + addressType);
            }
        }

        /// <summary>
        /// Gets all locs (instead of loc references) contained in the given railway.
        /// </summary>
        public static IEnumerable<ILoc> GetLocs(this IRailway railway)
        {
            foreach (var locRef in railway.Locs)
            {
                ILoc loc;
                if (locRef.TryResolve(out loc))
                {
                    yield return loc;
                }
            }
        }

        /// <summary>
        /// Gets all modules (instead of module references) contained in the given railway.
        /// </summary>
        public static IEnumerable<IModule> GetModules(this IRailway railway)
        {
            foreach (var moduleRef in railway.Modules)
            {
                IModule module;
                if (moduleRef.TryResolve(out module))
                {
                    yield return module;
                }
            }
        }

        /// <summary>
        /// Gets all address entities contained in the given railway.
        /// </summary>
        public static IEnumerable<IAddressEntity> GetAddressEntities(this IRailway railway)
        {
            return railway.GetModules().SelectMany(x => x.GetAddressEntities()).Concat(railway.GetLocs());
        }

        /// <summary>
        /// Gets all address entities contained in the given railway.
        /// </summary>
        public static IEnumerable<IAddressEntity> GetAddressEntities(this IModule module)
        {
            return module.GetPositionedEntities().OfType<IAddressEntity>();
        }

        /// <summary>
        /// Is the given sensor used in the given route and entering/reached sensor?
        /// </summary>
        public static bool IsUsedBy(this ISensor sensor, IRoute route)
        {
            return route.Events.Any(x => x.Sensor == sensor);
        }

        /// <summary>
        /// Add warnings if addresses used in the given entities are used in more then 1 entity.
        /// </summary>
        internal static void WarnForDuplicateAddresses(this List<IAddressEntity> addressEntities, ValidationResults results)
        {
            // Check for duplicate addresses
            var addressUsages = addressEntities.SelectMany(x => x.AddressUsages).Distinct().OrderBy(x => x).ToList();
            foreach (var iterator in addressUsages)
            {
                var addressUsage = iterator;
                var usedIn = addressEntities.Where(entity => entity.AddressUsages.Any(x => x.Equals(addressUsage))).Distinct().ToList();
                if (usedIn.Count > 1)
                {
                    results.Warn(usedIn[0], Strings.AddressXIsAlsoUsedInY, addressUsage.Address, string.Join(", ", usedIn.Skip(1).Select(x => x.Description)));
                }
            }
        }
    }
}
