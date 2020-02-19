namespace DNNGamification.Components.Repositories
{
    using DotNetNuke.Common;
    using DotNetNuke.Common.Utilities;

    using DNNGamification.Components.Entities;

    using DotNetNuke.Data;

    using System.Collections;
    using System.Collections.Generic;

    using System.Data;

    /// <summary>
    /// User activities repository.
    /// </summary>
    public class UserActivitiesRepository : RepositoryBase
    {
        #region Public Properties : Insert

        /// <summary>
        /// Adds user activity.
        /// </summary>
        public int Add(int userId, int portalId, decimal activityPoints)
        {
            return DataProvider.ExecuteScalar<int>("DNNGamification_AddUserActivity", userId, portalId, activityPoints);
        }

        #endregion

        #region Public Properties : Select

        /// <summary>
        /// Gets user activity by user ID.
        /// </summary>
        public UserActivity GetBy(int userId, int portalId)
        {
            return CBO.FillObject<UserActivity>(DataProvider.ExecuteReader("DNNGamification_GetUserActivityByUser", userId, portalId));
        }

        #endregion

        #region Public Properties : Update

        /// <summary>
        /// Updates user activity.
        /// </summary>
        public void Update(int id, int? userId, int? portalId, decimal? activityPoints)
        {
            DataProvider.ExecuteNonQuery("DNNGamification_UpdateUserActivity", id, userId, portalId, activityPoints);
        }

        #endregion

        #region Public Properties : Delete

        /// <summary>
        /// Deletes user activity.
        /// </summary>
        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("DNNGamification_DeleteUserActivityById", id);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        public UserActivitiesRepository(DataProvider provider)
            : base(provider)
        { }

        #endregion
    }
}
