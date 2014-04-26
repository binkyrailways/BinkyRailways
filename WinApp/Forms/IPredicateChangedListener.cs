using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Forms
{
    public interface IPredicateChangedListener
    {
        /// <summary>
        /// Called when a predicate has changed.
        /// </summary>
        void PredicateChanged(ILocPredicate predicate);
    }
}
