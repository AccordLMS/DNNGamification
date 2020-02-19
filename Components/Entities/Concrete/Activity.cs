namespace DNNGamification.Components.Entities
{
    using System;
    using System.Data;

    using System.Runtime.Serialization;

    /// <summary>
    /// Activity entity.
    /// </summary>
    [DataContract, Serializable]
    public class Activity : EntityBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets ID.
        /// </summary>
        [DataMember]
        public int ActivityId { get; set; }

        /// <summary>
        /// Gets or sets key ID.
        /// </summary>
        [IgnoreDataMember]
        public override int KeyID
        {
            get { return ActivityId; } set { ActivityId = value; }
        }

        /// <summary>
        /// Gets or sets module ID.
        /// </summary>
        [DataMember]
        public int DesktopModuleId { get; set; }

        /// <summary>
        /// Gets or sets module name.
        /// </summary>
        [DataMember]
        public string DesktopModuleName { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets display name.
        /// </summary>
        [IgnoreDataMember]
        public string DisplayName
        {
            get { return String.Format("{0} > {1}", DesktopModuleName, Name); }
        }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets synonym.
        /// </summary>
        [DataMember]
        public string Synonym { get; set; }

        /// <summary>
        /// Gets or sets activity points.
        /// </summary>
        [DataMember]
        public decimal ActivityPoints { get; set; }

        /// <summary>
        /// Gets or sets once.
        /// </summary>
        [DataMember]
        public bool Once { get; set; }

        #endregion

        #region Public Methods : IHydratable

        /// <summary>
        /// Provides hydration.
        /// </summary>
        public override void Fill(IDataReader r)
        {
            ActivityId                = Utils.ConvertTo<int>(r["ActivityId"]);
            Name                      = Utils.ConvertTo<string>(r["Name"]);
            DesktopModuleName         = Utils.ConvertTo<string>(r["DesktopModuleName"]);
            Description               = Utils.ConvertTo<string>(r["Description"]);
            Synonym                   = Utils.ConvertTo<string>(r["Synonym"]);
            DesktopModuleId           = Utils.ConvertTo<int>(r["DesktopModuleId"]);
            ActivityPoints            = Utils.ConvertTo<decimal>(r["ActivityPoints"]);
            Once                      = Utils.ConvertTo<bool>(r["Once"]);
        }

        #endregion
    }
}
