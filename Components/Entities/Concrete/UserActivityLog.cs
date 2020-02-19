namespace DNNGamification.Components.Entities
{
    using DotNetNuke.Entities;
    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Entities.Users;

    using System;
    using System.Data;

    using System.Runtime;
    using System.Runtime.Serialization;

    /// <summary>
    /// User activity log entity.
    /// </summary>
    [DataContract, Serializable]
    public class UserActivityLog : EntityBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets user activity ID.
        /// </summary>
        [DataMember]
        public int UserActivityLogId { get; set; }

        /// <summary>
        /// Gets or sets key ID.
        /// </summary>
        [IgnoreDataMember]
        public override int KeyID
        {
            get { return UserActivityLogId; } set { UserActivityLogId = value; }
        }

        /// <summary>
        /// Gets or sets activity ID.
        /// </summary>
        [DataMember]
        public int ActivityId { get; set; }

        /// <summary>
        /// Gets or sets user ID.
        /// </summary>
        [DataMember]
        public int UserId { get; set; }

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
        /// Gets or sets module name.
        /// </summary>
        [DataMember]
        public string DesktopModuleName { get; set; }

        /// <summary>
        /// Gets or sets activity points.
        /// </summary>
        [DataMember]
        public decimal ActivityPoints { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        [IgnoreDataMember]
        public string DisplayName
        {
            get { return String.Format("{0} > {1}", DesktopModuleName, Name); }
        }

        /// <summary>
        /// Gets or sets create date.
        /// </summary>
        [DataMember]
        public DateTime CreateDate { get; set; }

        #endregion

        #region Public Methods : IHydratable

        /// <summary>
        /// Provides hydration.
        /// </summary>
        public override void Fill(IDataReader r)
        {
            UserActivityLogId = Utils.ConvertTo<int>(r["UserActivityLogId"]);
            ActivityId        = Utils.ConvertTo<int>(r["ActivityId"]);
            UserId            = Utils.ConvertTo<int>(r["UserId"]);
            PortalId          = Utils.ConvertTo<int>(r["PortalId"]);
            DesktopModuleId   = Utils.ConvertTo<int>(r["DesktopModuleId"]);
            DesktopModuleName = Utils.ConvertTo<string>(r["DesktopModuleName"]);
            Name              = Utils.ConvertTo<string>(r["Name"]);
            ActivityPoints    = Utils.ConvertTo<decimal>(r["ActivityPoints"]);
            CreateDate        = Utils.ConvertTo<DateTime>(r["CreateDate"]);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UserActivityLog() { }

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        public UserActivityLog(int id, decimal points)
        {
            UserActivityLogId = id; ActivityPoints = points;
        }

        #endregion
    }
}
