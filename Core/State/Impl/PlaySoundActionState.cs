using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State for a play sound action.
    /// </summary>
    public sealed class PlaySoundActionState : ActionState<IPlaySoundAction>
    {
        private ISound sound;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal PlaySoundActionState(IPlaySoundAction entity, RailwayState railwayState)
            : base(entity, railwayState)
        {
        }

        /// <summary>
        /// Execute this action in the given context.
        /// </summary>
        public override void Execute(IActionContext context)
        {
            if (sound != null)
            {
                sound.Play();
            }
        }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected override bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            var stream = Entity.Sound;
            if (stream != null)
            {
                sound = ui.SoundPlayer.Create(stream);
            }
            return true;
        }
    }
}
