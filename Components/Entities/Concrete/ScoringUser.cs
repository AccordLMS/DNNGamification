namespace DNNGamification.Components.Entities
{
    using DotNetNuke.Entities;
    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Entities.Users;

    using System.Runtime;
    using System.Runtime.Serialization;

    using System;
    using System.Data;

    using System.Collections.Generic;

    /// <summary>
    /// Scoring user entity.
    /// </summary>
    [DataContract, Serializable]
    public class ScoringUser : EntityBase
    {
        #region Private Fields

        /// <summary>
        /// 
        /// </summary>
        private UserInfo _userInfo = null;

        /// <summary>
        /// 
        /// </summary>
        private List<UserBadge> _userBadges = null;

        /// <summary>
        /// 
        /// </summary>
        private List<Badge> _badges = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets user ID.
        /// </summary>
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets key ID.
        /// </summary>
        [IgnoreDataMember]
        public override int KeyID
        {
            get { return UserId; } set { UserId = value; }
        }

        /// <summary>
        /// Gets user profile absolute URL.
        /// </summary>
        [IgnoreDataMember]
        public string ProfilePhotoUrl
        {
            get // gets leaderboard user' profile photo absolute URL
            {
                PortalSettings portal = null;

                if ((portal = PortalController.Instance.GetCurrentPortalSettings()) != null)
                {
                    if (_userInfo == null) _userInfo = UserController.Instance.GetUserById(portal.PortalId, UserId);
                }

                if (_userInfo != null && _userInfo.Profile != null)
                {
                    return _userInfo.Profile.PhotoURL; // resolve absolute URL
                }

                return null;
            }
        }

        /// <summary>
        /// Gets or sets user name.
        /// </summary>
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets activity points.
        /// </summary>
        [DataMember]
        public int ActivityPoints { get; set; }

        /// <summary>
        /// Gets or sets row.
        /// </summary>
        [DataMember]
        public int Row { get; set; }

        #endregion

        #region Public Methods : IHydratable

        /// <summary>
        /// Provides hydration.
        /// </summary>
        public override void Fill(IDataReader r)
        {
            UserId         = Utils.ConvertTo<int>(r["UserId"]);
            ActivityPoints = Utils.ConvertTo<int>(r["ActivityPoints"]);
            UserName       = Utils.ConvertTo<string>(r["Username"]);
            Row            = Utils.ConvertTo<int>(r["Row"]);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ScoringUser() { }

        #endregion
    }
}
