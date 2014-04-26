using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.TypeConverters;
using BinkyRailways.WinApp.UIEditors;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class LocGroupSettings : EntitySettings<ILocGroup>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal LocGroupSettings(ILocGroup entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            base.GatherProperties(properties);
            //properties.Add(() => Locs, Strings.TabGeneral, Strings.LocGroupLocsName, Strings.LocGroupLocsHelp);

            var i = 0;
            var locAttr = new Attribute[] {
                new TypeConverterAttribute(typeof(LocIsElementOfGroupTypeConverter)), 
                new MergablePropertyAttribute(false), 
                new EditableInRunningStateAttribute()
            };
            foreach (var iterator in Context.AppState.Package.Railway.GetLocs())
            {
                var loc = iterator;
                properties.Add<LocGroupSettings, bool>(s => s.IsElement(loc), (s, x) => s.SetIsElement(loc, x), false, "loc" + i, 
                    Strings.TabLocs, loc.Description, string.Format(Strings.IsXElementOfGroupHelp, loc.Description), locAttr);
                i++;
            }
        }

        /// <summary>
        /// Loc getter
        /// </summary>
        private bool IsElement(ILoc loc)
        {
            return Entity.Locs.Contains(loc);
        }

        /// <summary>
        /// Loc setter
        /// </summary>
        private void SetIsElement(ILoc loc, bool isElement)
        {
            if (isElement)
            {
                Entity.Locs.Add(loc);
            }
            else
            {
                Entity.Locs.Remove(loc);
            }
        }

        /// <summary>
        /// Set of locs in the group.
        /// </summary>
        [TypeConverter(typeof(LocSetTypeConverter))]
        [Editor(typeof(LocSetEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        [EditableInRunningState]
        public IEntitySet3<ILoc> Locs
        {
            get { return Entity.Locs; }
        }
    }
}
