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
    /// User activities log repository.
    /// </summary>
    public class UserActivitiesLogRepository : RepositoryBase
    {
        #region Public Properties : Insert

        /// <summary>
        /// Adds user activity log.
        /// </summary>
        public int Add(int activityId, int userId, int portalId, int portalActivityId, decimal activityPoints, int attemptId)
        {
            try
            {

            
            return DataProvider.ExecuteScalar<int>("DNNGamification_AddUserActivityLog", activityId, userId, portalId, portalActivityId, activityPoints, attemptId);
            }catch(Exception ex)
            {
                return 0;
            }
        }

        #endregion

        #region Public Properties : Select

        /// <summary>
        /// Gets user activity log by ID.
        /// </summary>
        public UserActivityLog GetBy(int id)
        {
            return CBO.FillObject<UserActivityLog>(DataProvider.ExecuteReader("DNNGamification_GetUserActivityLogById", id));
        }

        /// <summary>
        /// Gets user activity log by user ID and activity ID.
        /// </summary>
        public List<UserActivityLog> GetManyBy(int userId, int portalId, int activityId)
        {
            return CBO.FillCollection<UserActivityLog>(DataProvider.ExecuteReader("DNNGamification_GetUserActivityLogByUserAndDefinition", userId, portalId, activityId));
        }

        /// <summary>
        /// Gets user activity log by user ID.
        /// </summary>
        public List<UserActivityLog> GetManyBy(int userId, int portalId)
        {
            return CBO.FillCollection<UserActivityLog>(DataProvider.ExecuteReader("DNNGamification_GetUserActivityLogByUser", userId, portalId));
        }

        #endregion

        #region Public Properties : Get : Reports and Charts

        /// <summary>
        /// Gets scoring users.
        /// </summary>
        public List<ScoringUser> GetUsers(int portalId, string term, int startIndex, int length, string orderBy, string orderByDirection, out int total)
        {
            IDataReader reader = DataProvider.ExecuteReader("DNNGamification_GetScoringUsers", portalId, term, startIndex, length, orderBy, orderByDirection);

            reader.Read(); total = Utils.ConvertTo<int>(reader["TotalCount"]); reader.NextResult();
            {
                return CBO.FillCollection<ScoringUser>(reader);
            }
        }

        /// <summary>
        /// Gets scoring leaderboard.
        /// </summary>
        public List<ScoringLeaderboard> GetLeaderboard(int portalId, int? userId, int? groupId, int? friendsOfId, int startIndex, int length, out int total)
        {
            IDataReader reader = DataProvider.ExecuteReader("DNNGamification_GetScoringLeaderboard", portalId, userId, groupId, friendsOfId, startIndex, length);

            reader.Read(); total = Utils.ConvertTo<int>(reader["TotalCount"]); reader.NextResult();
            {
                return CBO.FillCollection<ScoringLeaderboard>(reader);
            }
        }

        /// <summary>
        /// Gets scoring chart.
        /// </summary>
        public List<UserActivityLog> GetChart(int userId, int portalId, DateTime start, DateTime end)
        {
            IDataReader reader = DataProvider.ExecuteReader("DNNGamification_GetScoringChart", userId, portalId, start, end);
            {
                return CBO.FillCollection<UserActivityLog>(reader);
            }
        }

        /// <summary>
        /// Gets scoring leaderboard.
        /// </summary>
        public List<UserActivitySummary> GetUserActivitySummary(int? userId, int portalId, DateTime start, DateTime end, int startIndex, int length, out int total)
        {
            IDataReader reader;

            if (start == Null.NullDate || end == Null.NullDate)
            {
                reader = DataProvider.ExecuteReader("DNNGamification_GetActivitySummary", portalId, userId, DBNull.Value, DBNull.Value, startIndex, length);
            }
            else
            {
                reader = DataProvider.ExecuteReader("DNNGamification_GetActivitySummary", portalId, userId, start, end, startIndex, length);
            }
            reader.Read(); total = Utils.ConvertTo<int>(reader["TotalCount"]); reader.NextResult();
            return CBO.FillCollection<UserActivitySummary>(reader);
        }

        #endregion

        #region Public Properties : Update

        /// <summary>
        /// Updates user activity log.
        /// </summary>
        public void Update(int id, int? activityId, int? userId, int? portalId, int? activityPoints)
        {
            DataProvider.ExecuteNonQuery("DNNGamification_UpdateUserActivityLog", id, activityId, userId, portalId, activityPoints);
        }

        #endregion

        #region Public Properties : Delete

        /// <summary>
        /// Deletes user activity log by ID.
        /// </summary>
        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("DNNGamification_DeleteUserActivityLogById", id);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        public UserActivitiesLogRepository(DataProvider provider): base(provider)
        { }

        #endregion
    }
}
