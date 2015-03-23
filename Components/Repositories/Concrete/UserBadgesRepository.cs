namespace DNNGamification.Components.Repositories
{
    using DotNetNuke.Common;
    using DotNetNuke.Common.Utilities;

    using DNNGamification.Components.Entities;

    using DotNetNuke.Data;

    using System.Collections;
    using System.Collections.Generic;

    using System;
    using System.Configuration;
    using System.Data;

    /// <summary>
    /// User badges repository.
    /// </summary>
    public class UserBadgesRepository : RepositoryBase
    {
        #region Public Properties : Insert

        /// <summary>
        /// Adds user badge.
        /// </summary>
        public int Add(int badgeId, int userId, int portalId)
        {
            return DataProvider.ExecuteScalar<int>("DNNGamification_AddUserBadge", badgeId, userId, portalId);
        }

        #endregion

        #region Public Properties : Select

        /// <summary>
        /// Gets user badge by ID.
        /// </summary>
        public UserBadge GetBy(int id)
        {
            return CBO.FillObject<UserBadge>(DataProvider.ExecuteReader("DNNGamification_GetUserBadgeById", id));
        }

        /// <summary>
        /// Get top, actual or all user badges by user ID.
        /// </summary>
        public List<UserBadge> GetManyBy(int userId, int portalId, bool actual = true, int? top = null)
        {
            return CBO.FillCollection<UserBadge>(DataProvider.ExecuteReader("DNNGamification_GetUserBadgesByUser", userId, portalId, actual, top));
        }

        #endregion

        #region Public Properties : Update

        /// <summary>
        /// Updates user badge.
        /// </summary>
        public void Update(int id, int? badgeId, int? userId, int? portalId, DateTime? expire)
        {
            DataProvider.ExecuteNonQuery("DNNGamification_UpdateUserBadge", id, badgeId, portalId, userId, expire);
        }

        #endregion

        #region Public Properties : Delete

        /// <summary>
        /// Delete user badge.
        /// </summary>
        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("DNNGamification_DeleteUserBadgeById", id);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        public UserBadgesRepository(DataProvider provider)
            : base(provider)
        { }

        #endregion
    }
}
