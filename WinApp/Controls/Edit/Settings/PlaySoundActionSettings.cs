using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Reflection;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.TypeConverters;
using BinkyRailways.WinApp.UIEditors;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    /// <summary>
    /// Settings wrapper for an action.
    /// </summary>
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class PlaySoundActionSettings : ActionSettings<IPlaySoundAction>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal PlaySoundActionSettings(IPlaySoundAction entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            base.GatherProperties(properties);
            properties.Add(() => Sound, Strings.TabGeneral, "Sound", "The sound file that will be played");
        }

        [TypeConverter(typeof(SoundTypeConverter))]
        [Editor(typeof(SoundEditor), typeof(UITypeEditor))]
        public Stream Sound
        {
            get { return Entity.Sound; }
            set { Entity.Sound = value; }
        }
    }
}
