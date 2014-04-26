using System.Collections.Generic;
using System.ComponentModel;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Device that signals some event on the railway with a state of "on" or "off".
    /// </summary>
    public sealed class BinarySensor : Sensor, IBinarySensor, IActionTriggerSourceInternals
    {
        private readonly ActionTrigger activateTrigger;
        private readonly ActionTrigger deActivateTrigger;

        /// <summary>
        /// Default ctor
        /// </summary>
        public BinarySensor()
        {
            activateTrigger = new ActionTrigger(this, Strings.TriggerNameActivate);
            deActivateTrigger = new ActionTrigger(this, Strings.TriggerNameDeActivate);
        }

        /// <summary>
        /// Trigger fired when the sensor becomes active.
        /// </summary>
        public ActionTrigger ActivateTrigger { get { return activateTrigger; } }

        /// <summary>
        /// Should <see cref="ActivateTrigger"/> be serialized?
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeActivateTrigger()
        {
            return !activateTrigger.IsEmpty;
        }

        /// <summary>
        /// Trigger fired when the sensor becomes in-active.
        /// </summary>
        public ActionTrigger DeActivateTrigger { get { return deActivateTrigger; } }

        /// <summary>
        /// Should <see cref="DeActivateTrigger"/> be serialized?
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeDeActivateTrigger()
        {
            return !deActivateTrigger.IsEmpty;
        }

        /// <summary>
        /// Trigger fired when the sensor becomes active.
        /// </summary>
        IActionTrigger IBinarySensor.ActivateTrigger { get { return activateTrigger; } }

        /// <summary>
        /// Trigger fired when the sensor becomes in-active.
        /// </summary>
        IActionTrigger IBinarySensor.DeActivateTrigger { get { return deActivateTrigger; } }

        /// <summary>
        /// Gets all triggers of this entity.
        /// </summary>
        IEnumerable<IActionTrigger> IActionTriggerSource.Triggers
        {
            get
            {
                yield return activateTrigger;
                yield return deActivateTrigger;
            }
        }

        /// <summary>
        /// Gets the containing railway.
        /// </summary>
        IRailway IActionTriggerSourceInternals.Railway { get { return Root; } }

        /// <summary>
        /// Gets the persistent entity that contains this action trigger.
        /// </summary>
        IPersistentEntity IActionTriggerSourceInternals.Container { get { return Module; } }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.TypeNameBinarySensor; }
        }

        /// <summary>
        /// Validate the integrity of this entity.
        /// </summary>
        public override void Validate(IEntity validationRoot, ValidationResults results)
        {
            base.Validate(validationRoot, results);
            activateTrigger.Validate(validationRoot, results);
            deActivateTrigger.Validate(validationRoot, results);
        }

        /// <summary>
        /// If this entity uses the given subject, add a <see cref="UsedByInfo"/> entry to 
        /// the given result.
        /// </summary>
        public override void CollectUsageInfo(IEntity subject, UsedByInfos results)
        {
            base.CollectUsageInfo(subject, results);
            activateTrigger.CollectUsageInfo(subject, results);
            deActivateTrigger.CollectUsageInfo(subject, results);
        }

        /// <summary>
        /// The given entity is removed from the package.
        /// </summary>
        protected override void RemovedFromPackage(IPersistentEntity entity)
        {
            base.RemovedFromPackage(entity);
            activateTrigger.RemovedFromPackage(entity);
            deActivateTrigger.RemovedFromPackage(entity);
        }
    }
}
