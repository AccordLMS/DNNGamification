namespace DNNGamification.Components.Controllers
{
    using System;

    using DotNetNuke.Services.Social.Notifications;

    using DotNetNuke.Entities;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Host;

    using DotNetNuke.Framework;

    /// <summary>
    /// 
    /// </summary>
    public class BusinessController : IUpgradeable
    {
        #region Private Methods

        /// <summary>
        /// Registers notification types.
        /// </summary>
        private static void RegisterNotificationTypes()
        {
            var badgeAwarded = new NotificationType
            {
                Name = "DNNGamification_BadgeAwarded", Description = "Badge awarded"
            };

            var controller = ServiceLocator<INotificationsController, NotificationsController>.Instance;
            {
                controller.CreateNotificationType(badgeAwarded);
            }
        }

        #endregion

        #region Public Methods : IUpgradeable

        /// <summary>
        /// UpgradeModule handler.
        /// </summary>
        public string UpgradeModule(string version)
        {
            try
            {
                BusinessController.RegisterNotificationTypes(); return "Success";
            }
            catch (Exception ex)
            {
                return "Failed: " + ex.Message;
            }
        }

        #endregion
    }
}
