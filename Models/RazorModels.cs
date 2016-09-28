namespace DNNGamification.Models
{
    using DNNGamification.Components;
    using DNNGamification.Components.Repositories;
    using DNNGamification.Components.Entities;

    using DotNetNuke.Entities;
    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Entities.Users;

    using System;

    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Leaderboard model.
    /// </summary>
    public class LeaderboardModel
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets row.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Gets user badges list.
        /// </summary>
        public List<UserBadge> Badges
        {
            get // gets leaderboard user' badges list (all)
            {
                using (var uow = new UnitOfWork())
                {
                    //PortalSettings portal = PortalSettings.Current;

                    //if (portal != null) // chek settings
                    //{
                        return uow.UserBadges.GetManyBy(UserId, PortalId, false);
                    //}

                    //return new List<UserBadge>();
                }
            }
        }

        /// <summary>
        /// Gets or sets user ID.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets portal ID.
        /// </summary>
        public int PortalId { get; set; }

        /// <summary>
        /// Gets user profile absolute URL.
        /// </summary>
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
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets activity points.
        /// </summary>
        public int ActivityPoints { get; set; }

        /// <summary>
        /// Gets or sets rank.
        /// </summary>
        public int Rank { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LeaderboardModel() { }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LeaderboardModel(ScoringLeaderboard source)
        {
            Rank           = source.Rank;
            Row            = source.Row;
            UserId         = source.UserId;
            PortalId       = source.PortalId;
            ActivityPoints = source.ActivityPoints;
            FirstName      = source.FirstName;
            DisplayName    = source.DisplayName;
            LastName       = source.LastName;
        }

        #endregion
    }
}