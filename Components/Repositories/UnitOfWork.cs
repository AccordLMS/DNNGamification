namespace DNNGamification.Components.Repositories
{
    using DotNetNuke.Data;

    using System;
    using System.Data.Common;

    /// <summary>
    /// Unit of work interface.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        #region Properties

        /// <summary>
        /// Gets activities repository.
        /// </summary>
        ActivitiesRepository Activities { get; }

        /// <summary>
        /// Gets badge activity repository.
        /// </summary>
        BadgeActivitiesRepository BadgeActivities { get; }

        /// <summary>
        /// Gets badges repository.
        /// </summary>
        BadgesRepository Badges { get; }

        /// <summary>
        /// Gets user activities repository.
        /// </summary>
        UserActivitiesRepository UserActivities { get; }

        /// <summary>
        /// Gets user activities log repository.
        /// </summary>
        UserActivitiesLogRepository UserActivitiesLog { get; }

        /// <summary>
        /// Gets user badges repository.
        /// </summary>
        UserBadgesRepository UserBadges { get; }

        #endregion
    }

    /// <summary>
    /// Unit of work implementation.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Fields

        /// <summary>
        /// Gets scoring action definitions repository.
        /// </summary>
        private ActivitiesRepository _activities = null;

        /// <summary>
        /// Gets badge activities repository.
        /// </summary>
        private BadgeActivitiesRepository _badgeActivities = null;

        /// <summary>
        /// Gets badges repository.
        /// </summary>
        private BadgesRepository _badges = null;

        /// <summary>
        /// Gets user activities repository.
        /// </summary>
        private UserActivitiesRepository _userActivities = null;

        /// <summary>
        /// Gets user activities log repository.
        /// </summary>
        private UserActivitiesLogRepository _userActivitiesLog = null;

        /// <summary>
        /// Gets user badges repository.
        /// </summary>
        private UserBadgesRepository _userBadges = null;

        #endregion

        #region Private Properties : Provider

        /// <summary>
        /// Gets or sets data provider.
        /// </summary>
        private DataProvider DataProvider { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets activities repository.
        /// </summary>
        public ActivitiesRepository Activities
        {
            get { return _activities ?? (_activities = new ActivitiesRepository(DataProvider)); }
        }

        /// <summary>
        /// Gets badge activities repository.
        /// </summary>
        public BadgeActivitiesRepository BadgeActivities
        {
            get { return _badgeActivities ?? (_badgeActivities = new BadgeActivitiesRepository(DataProvider)); }
        }

        /// <summary>
        /// Gets badges repository.
        /// </summary>
        public BadgesRepository Badges
        {
            get { return _badges ?? (_badges = new BadgesRepository(DataProvider)); }
        }

        /// <summary>
        /// Gets user activities repository.
        /// </summary>
        public UserActivitiesRepository UserActivities
        {
            get { return _userActivities ?? (_userActivities = new UserActivitiesRepository(DataProvider)); }
        }

        /// <summary>
        /// Gets user activities log repository.
        /// </summary>
        public UserActivitiesLogRepository UserActivitiesLog
        {
            get { return _userActivitiesLog ?? (_userActivitiesLog = new UserActivitiesLogRepository(DataProvider)); }
        }

        /// <summary>
        /// Gets user badges repository.
        /// </summary>
        public UserBadgesRepository UserBadges
        {
            get { return _userBadges ?? (_userBadges = new UserBadgesRepository(DataProvider)); }
        }

        #endregion

        #region Public Methods : IDisposable

        /// <summary>
        /// Disposes current instance.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UnitOfWork()
        {
            DataProvider = DataProvider.Instance();
        }

        #endregion
    }
}