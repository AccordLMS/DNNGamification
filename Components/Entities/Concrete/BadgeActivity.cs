namespace DNNGamification.Components.Entities
{
    using System;
    using System.Data;

    using System.Runtime;
    using System.Runtime.Serialization;

    /// <summary>
    /// Badge activity entity.
    /// </summary>
    [DataContract, Serializable]
    public class BadgeActivity : EntityBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets badge activity ID.
        /// </summary>
        [DataMember]
        public int BadgeActivityId { get; set; }
        /// <summary>
        /// Gets or sets key ID.
        /// </summary>
        [IgnoreDataMember]
        public override int KeyID
        {
            get { return BadgeActivityId; } set { BadgeActivityId = value; }
        }

        /// <summary>
        /// Gets or sets portal ID.
        /// </summary>
        [DataMember]
        public int PortalId { get; set; }

        /// <summary>
        /// Gets or sets module ID.
        /// </summary>
        [DataMember]
        public int DesktopModuleId { get; set; }

        /// <summary>
        /// Gets or sets activity ID.
        /// </summary>
        [DataMember]
        public int ActivityId { get; set; }

        /// <summary>
        /// Gets or sets badge ID.
        /// </summary>
        [DataMember]
        public int BadgeId { get; set; }

        /// <summary>
        /// Gets or sets activity points.
        /// </summary>
        [DataMember]
        public int ActivityPoints { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        [IgnoreDataMember]
        public string DisplayName
        {
            get { return String.Format("{0} > {1}", DesktopModuleName, Name); }
        }

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

        #endregion

        #region Public Methods : IHydratable

        /// <summary>
        /// Provides hydration.
        /// </summary>
        public override void Fill(IDataReader r)
        {
            BadgeActivityId   = Utils.ConvertTo<int>(r["BadgeActivityId"]);

            PortalId          = Utils.ConvertTo<int>(r["PortalId"]);
            DesktopModuleId   = Utils.ConvertTo<int>(r["DesktopModuleId"]);
            ActivityId        = Utils.ConvertTo<int>(r["ActivityId"]);
            BadgeId           = Utils.ConvertTo<int>(r["BadgeId"]);

            ActivityPoints    = Utils.ConvertTo<int>(r["ActivityPoints"]);

            DesktopModuleName = Utils.ConvertTo<string>(r["DesktopModuleName"]);
            Name              = Utils.ConvertTo<string>(r["Name"]);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BadgeActivity() { }

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        public BadgeActivity(int id, int points)
        {
            BadgeActivityId = id; ActivityPoints = points;
        }

        #endregion
    }
}
