using System.Collections.Generic;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;

namespace DNNGamification
{
    using DotNetNuke.Entities;
    using DotNetNuke.Entities.Modules;

    using DotNetNuke.Services.Exceptions;

    using DotNetNuke.Framework;
    using DotNetNuke.Framework.JavaScriptLibraries;

    using DotNetNuke.Web.Client;
    using DotNetNuke.Web.Client.ClientResourceManagement;

    using System.Web.UI;
    using System.Web.UI.WebControls;

    using System;

    /// <summary>
    /// 
    /// </summary>
    public class ModuleSettings : ModuleSettingsBase
    {
        #region Protected Properties

        /// <summary>
        /// Gets js files priority (order).
        /// </summary>
        protected int JsPriority
        {
            get { return (int)FileOrder.Js.DefaultPriority + 1; }
        }

        /// <summary>
        /// Gets css files priority (order).
        /// </summary>
        protected int CssPriority
        {
            get { return (int)FileOrder.Css.DefaultPriority + 1; }
        }

        #endregion

        #region Protected Methods

        protected void SetupPortalsList(DropDownList cbPortalId, int savedPortalId)
        {
            var isHostUser = UserInfo.IsSuperUser;
            const string cacheKey = "DNNGamificationPortalsCache";
            List<PortalInfo> portals;
            var p = DataCache.GetCache(cacheKey);
            if (p != null)
                portals = (List<PortalInfo>)p;
            else
            {
                portals = new List<PortalInfo>();
                var portalController = new PortalController();
                var moduleController = new ModuleController();
                var allPortals = portalController.GetPortals();
                foreach (PortalInfo portal in allPortals)
                {
                    // Does the portal have a Mechanics module?
                    var allMechanicsModules = moduleController.GetModulesByDefinition(portal.PortalID, "DNNGamification Mechanics");
                    if (allMechanicsModules.Count <= 0) continue;
                    portals.Add(portal);
                }
                DataCache.SetCache(cacheKey, portals, TimeSpan.FromMinutes(20));
            }

            foreach (var portal in portals)
            {
                cbPortalId.Items.Add(new ListItem(string.Format("{0} - {1}", portal.PortalID, portal.PortalName), portal.PortalID.ToString()));
            }

            // Defaults to the current portal
            var currentPortal = cbPortalId.Items.FindByValue(PortalId.ToString());
            if (currentPortal != null) currentPortal.Selected = true;

            // Select the saved portal
            if (savedPortalId >= 0)
            {
                var savedPortal = cbPortalId.Items.FindByValue(savedPortalId.ToString());
                if (savedPortal != null) savedPortal.Selected = true;
            }

            // Disables the portal selection 
            if (!isHostUser) cbPortalId.Enabled = false;
        }


        /// <summary>
        /// OnInit handler.
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            try // try to handle OnInit
            {
                WebControl.DisabledCssClass = "aspNetDisabled disabled";

                ClientResourceManager.RegisterScript(Page, TemplateSourceDirectory + "/Scripts/auxiliary.js");
                {
                    base.OnInit(e);
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// OnLoad handler.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            try // try to handle OnLoad
            {
                JavaScript.RequestRegistration("jQuery");
                JavaScript.RequestRegistration("jQuery-UI");

                ServicesFramework.Instance.RequestAjaxAntiForgerySupport();
                {
                    base.OnLoad(e);
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        #endregion
    }
}