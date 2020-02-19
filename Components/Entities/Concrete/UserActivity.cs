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
    /// User activity entity.
    /// </summary>
    [DataContract, Serializable]
    public class UserActivity : EntityBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets user activity ID.
        /// </summary>
        [DataMember]
        public int UserActivityId { get; set; }

        /// <summary>
        /// Gets or sets key ID.
        /// </summary>
        [IgnoreDataMember]
        public override int KeyID
        {
            get { return UserActivityId; } set { UserActivityId = value; }
        }

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
        /// Gets or sets activity points.
        /// </summary>
        [DataMember]
        public decimal ActivityPoints { get; set; }

        /// <summary>
        /// Gets or sets last modified date.
        /// </summary>
        [DataMember]
        public DateTime? LastModifiedDate { get; set; }

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
            UserActivityId   = Utils.ConvertTo<int>(r["UserActivityId"]);
            UserId           = Utils.ConvertTo<int>(r["UserId"]);
            PortalId         = Utils.ConvertTo<int>(r["PortalId"]);
            ActivityPoints   = Utils.ConvertTo<decimal>(r["ActivityPoints"]);
            LastModifiedDate = Utils.ConvertTo<DateTime?>(r["LastModifiedDate"]);
            CreateDate       = Utils.ConvertTo<DateTime>(r["CreateDate"]);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UserActivity() { }

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        public UserActivity(int id, decimal points)
        {
            UserActivityId = id; ActivityPoints = points;
        }

        #endregion
    }
}
