using System;
using System.Collections.Generic;
using System.Linq;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Automatic
{
    internal static class Extensions
    {
        /// <summary>
        /// Is is needed for a loc facing in the given direction (in the From block of the given route)
        /// to chance direction when taking this route?
        /// </summary>
        /// <param name="route">The route being investigated</param>
        /// <param name="locDirection">The direction the loc is facing in the From block of the given <see cref="route"/>.</param>
        internal static bool IsDirectionChangeNeeded(this IRouteState route, BlockSide locDirection)
        {
            var fromSide = route.FromBlockSide;
            return (fromSide != locDirection);
        }

        /// <summary>
        /// Gets a selection of all possible routes from the given block.
        /// </summary>
        internal static IEnumerable<IRouteState> GetAllPossibleRoutesFromBlock(this IRailwayState railwayState, IBlockState fromBlock)
        {
            return railwayState.RouteStates.Where(x => (x.From == fromBlock));
        }

        /// <summary>
        /// Gets a selection of all possible non-closed routes from the given block.
        /// </summary>
        internal static IEnumerable<IRouteState> GetAllPossibleNonClosedRoutesFromBlock(this IRailwayState railwayState, IBlockState fromBlock)
        {
            return railwayState.GetAllPossibleRoutesFromBlock(fromBlock).Where(x => !x.Closed);
        }

        /// <summary>
        /// Gets a selection of all possible routes from the given block + side.
        /// </summary>
        internal static IEnumerable<IRouteState> GetAllPossibleRoutesFromBlock(this IRailwayState railwayState, IBlockState fromBlock, BlockSide fromBlockSide)
        {
            return railwayState.RouteStates.Where(x => (x.From == fromBlock) && (x.FromBlockSide == fromBlockSide));
        }

        /// <summary>
        /// Gets a selection of all possible non-closed routes from the given block + side.
        /// </summary>
        internal static IEnumerable<IRouteState> GetAllPossibleNonClosedRoutesFromBlock(this IRailwayState railwayState, IBlockState fromBlock, BlockSide fromBlockSide)
        {
            return railwayState.GetAllPossibleRoutesFromBlock(fromBlock, fromBlockSide).Where(x => !x.Closed);
        }

        /// <summary>
        /// Are there more than 1 non-closed routes from the given block.
        /// </summary>
        internal static bool HasMultipleNonClosedRoutesFromBlock(this IRailwayState railwayState, IBlockState fromBlock)
        {
            return (railwayState.GetAllPossibleNonClosedRoutesFromBlock(fromBlock).Count() > 1);
        }

        /// <summary>
        /// Can the given loc change direction in the given block?
        /// </summary>
        internal static bool CanChangeDirectionIn(this ILocState loc, IBlockState block)
        {
            return (loc.ChangeDirection == ChangeDirection.Allow) && (block.ChangeDirection == ChangeDirection.Allow);
        }

        /// <summary>
        /// Is the given block consider a station for the given loc?
        /// </summary>
        /// <remarks>It is considered a station if the loc is allowed to stop in the block and the wait possibility is greater then 50%</remarks>
        internal static bool IsStationFor(this IBlockState block, ILocState loc)
        {
            return (block.IsStation) && block.WaitPermissions.Evaluate(loc);
        }

        /// <summary>
        /// Take a gamble.
        /// The result is true or false, where the given chance is for "true".
        /// </summary>
        internal static bool Gamble(this Random rnd, int chance)
        {
            if ((chance < 0) || (chance > 100))
                throw new ArgumentOutOfRangeException("chance", chance, "Must be between 0..100");
            if (chance == 0)
                return false;
            if (chance == 100)
                return true;
            var value = rnd.Next(100);
            return (value <= chance);
        }

        /// <summary>
        /// Take a random choice between one of the options.
        /// Each option has a probability.
        /// </summary>
        internal static T Gamble<T>(this Random rnd, params Tuple<T, int>[] options)
        {
            var boundaries = new int[options.Length];
            var max = 0;
            for (var i = 0; i < options.Length; i++)
            {
                max += options[i].Item2 + 1;
                boundaries[i] = max;
            }

            var value = rnd.Next(max + 1);
            for (var i = 0; i < options.Length; i++)
            {
                if (value <= boundaries[i])
                    return options[i].Item1;
            }
            // We should never get here
            return options[options.Length - 1].Item1;
        }

        /// <summary>
        /// Is the given loc allowed to leave its current block?
        /// </summary>
        internal static bool CanLeaveCurrentBlock(this ILocState loc)
        {
            var currentBlock = loc.CurrentBlock.Actual;
            var currentBlockGroup = (currentBlock != null) ? currentBlock.BlockGroup : null;
            return ((currentBlockGroup == null) || (currentBlockGroup.FirstLocCanLeave));
        }
    }
}
