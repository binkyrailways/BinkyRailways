using System.IO;
using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Action that plays a specific sound.
    /// </summary>
    public sealed class PlaySoundAction : Action, IPlaySoundAction
    {
        private const string SoundIdPrefix = "Sound";
        private readonly StreamProperty sound;

        /// <summary>
        /// Default ctor
        /// </summary>
        public PlaySoundAction()
        {
            sound = new StreamProperty();
        }

        /// <summary>
        /// Gets/sets the sound played in this action.
        /// </summary>
        /// <value>Null if there is no sound.</value>
        /// <remarks>Sound must be wav</remarks>
        [XmlIgnore]
        public Stream Sound
        {
            get
            {
                UpdateSoundContext();
                return sound.GetStream(SoundId);
            }
            set
            {
                UpdateSoundContext();
                sound.SetStream(value, SoundId);
            }
        }

        /// <summary>
        /// Gets the ID used to store the sound data under.
        /// </summary>
        private string SoundId
        {
            get
            {
                this.EnsureId();
                return SoundIdPrefix + "-" + Id;
            }
        }

        /// <summary>
        /// Called when the Owner property has changed.
        /// </summary>
        protected override void OwnerChanged()
        {
            base.OwnerChanged();
            UpdateSoundContext();
        }

        /// <summary>
        /// Connect the sound property holder to it's context
        /// </summary>
        private void UpdateSoundContext()
        {
            if (Owner != null)
            {
                sound.SetContext(Package, Container);
            }
            else
            {
                sound.SetContext(null, null);
            }            
        }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Create a clone of this action.
        /// </summary>
        protected override Action Clone()
        {
            return new PlaySoundAction();
        }
    }
}
