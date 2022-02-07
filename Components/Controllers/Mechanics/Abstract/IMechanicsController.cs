namespace DNNGamification.Components.Controllers
{
    using DNNGamification.Components.Entities;

    using DotNetNuke.Entities;
    using DotNetNuke.Entities.Users;

    using System;

    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Mechanics controller interface.
    /// </summary>
    public interface IMechanicsController : IDisposable
    {
        #region Public Methods : Insert

        /// <summary>
        /// Adds badge activity.
        /// </summary>
        int AddBadgeActivity(int portalId, int activityId, int badgeId, int activityPoints);

        /// <summary>
        /// Adds activity.
        /// </summary>
        int AddActivity(int desktopModuleId, string name, string description, string synonym, int activityPoints, bool once);

        /// <summary>
        /// Adds activity.
        /// </summary>
        int AddActivity(int desktopModuleId, string name, string description, string synonym, decimal activityPoints, bool once);

        /// <summary>
        /// Adds user badge.
        /// </summary>
        int AddUserBadge(int badgeId, int userId, int portalId);

        #endregion

        #region Public Methods : Select

        /// <summary>
        /// Gets badges list.
        /// </summary>
        List<Badge> GetBadges(int portalId);

        /// <summary>
        /// Gets user badges list.
        /// </summary>
        List<UserBadge> GetUserBadges(int userId, int portalId, bool actual = true, int? top = null);

        /// <summary>
        /// Gets badge activities list.
        /// </summary>
        List<BadgeActivity> GetBadgeActivities(int badgeId);

        /// <summary>
        /// Gets activity by name and desktop module ID.
        /// </summary>
        Activity GetActivity(string synonym, int desktopModuleId);

        /// <summary>
        /// Gets activity by ID.
        /// </summary>
        Activity GetActivity(int id);

        #endregion

        #region Public Methods : Update

        // <summary>
        /// Updates activity.
        /// </summary>
        void UpdateActivity(int id, int desktopModuleId, string name, string description, string synonym, int activityPoints, bool once);

        /// <summary>
        /// Updates activity.
        /// </summary>
        void UpdateActivity(int id, int desktopModuleId, string name, string description, string synonym, decimal activityPoints, bool once);

        #endregion

        #region Public Methods : Delete

        /// <summary>
        /// Deletes action definition by ID.
        /// </summary>
        void DeleteActivity(int id);

        /// <summary>
        /// Deletes action definition by synonym and module ID.
        /// </summary>
        void DeleteActivity(string synonym, int desktopModuleId);

        /// <summary>
        /// Deletes user badge.
        /// </summary>
        void DeleteUserBadge(int id);

        #endregion

        #region Public Methods

        /// <summary>
        /// Logs user activity (uses transaction).
        /// </summary>
        void LogUserActivity(string synonym, int desktopModuleId, int userId, int portalId, int portalActivityId);

        /// <summary>
        /// Logs user activity (uses transaction).
        /// </summary>
        void LogUserActivity(string synonym, int desktopModuleId, int userId, int portalId);

        /// <summary>
        /// Logs user activity (uses transaction).
        /// </summary>
        void LogUserActivity(string synonym, int desktopModuleId, int userId, int portalId, int portalActivityId, decimal points);


        /// <summary>
        /// Logs user activity (uses transaction).
        /// </summary>
        void LogUserActivity(string synonym, int desktopModuleId, int userId, int portalId, int portalActivityId, decimal points, int attemptId);

        /// <summary>
        /// Update user activity, adding the addValue to the existing one (addValue could be negavitve, to substract)
        /// </summary>
        void UpdateUserActivity(string EAName, int desktopModuleId, int userId, int portalId, decimal addValue);

        #endregion
    }
}
