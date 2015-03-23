namespace DNNGamification.Components.Entities
{
    using DNNGamification.Components.Repositories;

    using DotNetNuke.Common;
    using DotNetNuke.Common.Utilities;

    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Entities.Users;

    using DotNetNuke.Services.Tokens;

    using System;

    using System.Runtime;
    using System.Runtime.Serialization;

    using System.Collections;
    using System.Collections.Generic;

    using System.Data;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Scoring leaderboard entity.
    /// </summary>
    [DataContract, Serializable]
    public class ScoringLeaderboard : EntityBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets row.
        /// </summary>
        [DataMember]
        public int Row { get; set; }

        /// <summary>
        /// Gets user badges list.
        /// </summary>
        [IgnoreDataMember]
        public List<UserBadge> Badges
        {
            get // gets leaderboard user' badges list (all)
            {
                using (var uow = new UnitOfWork())
                {
                    PortalSettings portal = PortalSettings.Current;

                    if (portal != null) // chek settings
                    {
                        return uow.UserBadges.GetManyBy(UserId, portal.PortalId, false);
                    }

                    return new List<UserBadge>();
                }
            }
        }

        /// <summary>
        /// Gets or sets key ID.
        /// </summary>
        [IgnoreDataMember]
        public override int KeyID
        {
            get { return UserId; } set { UserId = value; }
        }

        /// <summary>
        /// Gets or sets user ID.
        /// </summary>
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// Gets user profile absolute URL.
        /// </summary>
        [IgnoreDataMember]
        public string ProfilePhotoUrl
        {
            get // gets leaderboard user' profile photo absolute URL
            {
                PortalSettings portal = PortalSettings.Current;

                if (portal == null) return null;

                UserInfo userInfo = UserController.Instance.GetUserById(portal.PortalId, UserId);

                if (userInfo != null && userInfo.Profile != null)
                {
                    return userInfo.Profile.PhotoURL; // resolve absolute URL
                }

                return String.Empty;
            }
        }

        /// <summary>
        /// Gets or sets first name.
        /// </summary>
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets display name.
        /// </summary>
        [DataMember]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets last name.
        /// </summary>
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets activity points.
        /// </summary>
        [DataMember]
        public int ActivityPoints { get; set; }

        /// <summary>
        /// Gets or sets rank.
        /// </summary>
        [DataMember]
        public int Rank { get; set; }

        #endregion

        #region Public Methods : IHydratable

        /// <summary>
        /// Provides hydration.
        /// </summary>
        public override void Fill(IDataReader r)
        {
            Rank           = Utils.ConvertTo<int>(r["Rank"]);
            Row            = Utils.ConvertTo<int>(r["Row"]);
            UserId         = Utils.ConvertTo<int>(r["UserId"]);
            ActivityPoints = Utils.ConvertTo<int>(r["ActivityPoints"]);
            FirstName      = Utils.ConvertTo<string>(r["FirstName"]);
            DisplayName    = Utils.ConvertTo<string>(r["DisplayName"]);
            LastName       = Utils.ConvertTo<string>(r["LastName"]);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ScoringLeaderboard() { }

        #endregion
    }
}
