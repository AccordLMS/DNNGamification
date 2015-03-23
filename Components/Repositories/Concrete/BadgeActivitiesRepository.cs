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
    /// Badge activities repository.
    /// </summary>
    public class BadgeActivitiesRepository : RepositoryBase
    {
        #region Public Properties : Insert

        /// <summary>
        /// Adds badge activity.
        /// </summary>
        public int Add(int portalId, int activityId, int badgeId, int activityPoints)
        {
            return DataProvider.ExecuteScalar<int>("DNNGamification_AddBadgeActivity", portalId, activityId, badgeId, activityPoints);
        }

        #endregion

        #region Public Properties : Select

        /// <summary>
        /// Gets badge activity by ID.
        /// </summary>
        public BadgeActivity GetBy(int id)
        {
            return CBO.FillObject<BadgeActivity>(DataProvider.ExecuteReader("DNNGamification_GetBadgeActivityById", id));
        }

        /// <summary>
        /// Gets badge activities by badge ID.
        /// </summary>
        public List<BadgeActivity> GetManyBy(int badgeId)
        {
            return CBO.FillCollection<BadgeActivity>(DataProvider.ExecuteReader("DNNGamification_GetBadgeActivitiesByBadge", badgeId));
        }

        /// <summary>
        /// Gets all badge activities.
        /// </summary>
        public List<BadgeActivity> Get(int portalId)
        {
            return CBO.FillCollection<BadgeActivity>(DataProvider.ExecuteReader("DNNGamification_GetBadgeActivities", portalId));
        }

        #endregion

        #region Public Properties : Update

        /// <summary>
        /// Updates badge activity.
        /// </summary>
        public void Update(int id, int? portalId, int? activityId, int? badgeId, int? activityPoints)
        {
            DataProvider.ExecuteNonQuery("DNNGamification_UpdateBadgeActivity", id, portalId, activityId, badgeId, activityPoints);
        }

        #endregion

        #region Public Properties : Delete

        /// <summary>
        /// Deletes badge activity by ID.
        /// </summary>
        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("DNNGamification_DeleteBadgeActivityById", id);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        public BadgeActivitiesRepository(DataProvider provider) : base(provider) { }

        #endregion
    }
}
