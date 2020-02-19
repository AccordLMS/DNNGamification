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
    /// Activities repository.
    /// </summary>
    public class ActivitiesRepository : RepositoryBase
    {
        #region Public Properties : Insert

        /// <summary>
        /// Adds activity.
        /// </summary>
        public int Add(int desktopModuleId, string name, string description, string synonym, decimal activityPoints, bool once)
        {
            return DataProvider.ExecuteScalar<int>("DNNGamification_AddActivity", desktopModuleId, name, description, synonym, activityPoints, once);
        }

        #endregion

        #region Public Properties : Select

        /// <summary>
        /// Gets activity by ID.
        /// </summary>
        public Activity GetBy(int id)
        {
            return CBO.FillObject<Activity>(DataProvider.ExecuteReader("DNNGamification_GetActivityById", id));
        }

        /// <summary>
        /// Gets activity by synonym and module ID.
        /// </summary>
        public Activity GetBy(string synonym, int desktopModuleId)
        {
            return CBO.FillObject<Activity>(DataProvider.ExecuteReader("DNNGamification_GetActivityBySynonymAndModule", synonym, desktopModuleId));
        }

        /// <summary>
        /// Gets activity paged view.
        /// </summary>
        public List<Activity> GetView(int startIndex, int length, string orderBy, string orderByDirection, out int total)
        {
            IDataReader reader = DataProvider.ExecuteReader("DNNGamification_GetActivitiesView", startIndex, length, orderBy, orderByDirection);

            reader.Read(); total = Utils.ConvertTo<int>(reader["TotalCount"]); reader.NextResult();
            {
                return CBO.FillCollection<Activity>(reader);
            }
        }

        /// <summary>
        /// Gets all activities.
        /// </summary>
        public List<Activity> GetAll()
        {
            return CBO.FillCollection<Activity>(DataProvider.ExecuteReader("DNNGamification_GetActivities"));
        }

        #endregion

        #region Public Properties : Update

        /// <summary>
        /// Updates activity.
        /// </summary>
        public void Update(int id, int? desktopModuleId, string name, string description, string synonym, decimal? activityPoints, bool? once)
        {
            DataProvider.ExecuteNonQuery("DNNGamification_UpdateActivity", id, desktopModuleId, name, description, synonym, activityPoints, once);
        }

        #endregion

        #region Public Properties : Delete

        /// <summary>
        /// Deletes activity.
        /// </summary>
        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("DNNGamification_DeleteActivityById", id);
        }

        /// <summary>
        /// Deletes activity.
        /// </summary>
        public void Delete(string synonym, int desktopModuleId)
        {
            DataProvider.ExecuteNonQuery("DNNGamification_DeleteActivityBySynonymAndModule", synonym, desktopModuleId);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        public ActivitiesRepository(DataProvider provider) : base(provider) { }

        #endregion
    }
}
