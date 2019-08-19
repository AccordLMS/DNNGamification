using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules.Communications;


namespace DNNGamification
{
    using DotNetNuke.Common;

    using DotNetNuke.Services.Exceptions;

    using DotNetNuke.Web.Client;
    using DotNetNuke.Web.Client.ClientResourceManagement;

    using DotNetNuke.Web.UI.WebControls;

    using System.Web.UI;
    using System.Web.UI.WebControls;

    using System;
    using System.Threading;
    using System.IO;
    using System.Collections;
    using Telerik.Web.UI;

    using DotNetNuke.Services.Localization;

    /// <summary>
    /// 
    /// </summary>
    public partial class ActivitySummarySettings : ModuleSettings
    {
        #region Defines

        /// <summary>
        /// Templates directory relative path.
        /// </summary>
        private const string TEMPLATES_DIRECTORY = "~\\DesktopModules\\DNNGamification\\Templates\\ActivitySummary";

        #endregion      

        #region Private Fields

        /// <summary>
        /// ActivitySummary settings.
        /// </summary>
        private Infrastructure.ActivitySummaryModuleSettings _settings = null;

        #endregion

        #region Protected Methods : Event Handlers

        /// <summary>
        /// OnInit handler.
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            try // try to handle OnInit
            {
                LocalResourceFile = TemplateSourceDirectory + "/App_LocalResources/ActivitySummarySettings.resx";

                ClientResourceManager.RegisterStyleSheet(Page, TemplateSourceDirectory + "/Css/leaderboard.settings.css", FileOrder.Css.DefaultPriority + 1);
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
        /// Page_Load handler.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try // try to handle Page_Load
            {             
                if (!IsPostBack)
                {
                    BindLearnerModules();
                    txtPageSize.Text = _settings.PageSize.ToString();

                    foreach (string directory in Directory.EnumerateDirectories(Server.MapPath(TEMPLATES_DIRECTORY)))
                    {
                        var info = new DirectoryInfo(directory);
                        {
                            cbTemplateDirectory.Items.Add(new ListItem(info.Name, info.Name));
                        }
                    }

                    if (!String.IsNullOrEmpty(_settings.TemplateDirectory)) // check directory name is defined
                    {
                        cbTemplateDirectory.SelectedValue = _settings.TemplateDirectory.ToString();
                    }
                    else
                    {
                        cbTemplateDirectory.SelectedValue = "Default";                
                    }

                    chkShowDateFilters.Checked = _settings.ShowDateFilter;
                    chbShowPaging.Checked = _settings.ShowPaging;

                    if (_settings.BeginDate != null && _settings.EndDate != null)
                    {
                        ctlCompletionDate.DateRange = _settings.DateRange;
                        ctlCompletionDate.StartDate = _settings.BeginDate;
                        ctlCompletionDate.EndDate = _settings.EndDate;                     
                    }

                    if(_settings.LearnerModuleId == -1)
                    {
                        cmbLearnerModule.SelectedValue = "default";
                    }
                    else
                    {
                        cmbLearnerModule.SelectedValue = _settings.LearnerModuleId.ToString();
                    }
                    
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads settings.
        /// </summary>
        public override void LoadSettings()
        {
            try // try to handle LoadSettings
            {
                _settings = Infrastructure.ActivitySummaryModuleSettings.Load(ModuleId);
                SetupPortalsList(ddrPortalId, _settings.PortalId);     
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// Updates settings.
        /// </summary>
        public override void UpdateSettings()
        {
            try // try to handle UpdateSettings
            {
                if (!Page.IsValid) return;

                _settings = Infrastructure.ActivitySummaryModuleSettings.Load(ModuleId);
                {

                    if (ddrPortalId.SelectedIndex >= 0)
                    {
                        _settings.PortalId = Convert.ToInt32(ddrPortalId.SelectedValue);
                    }

                    if (cbTemplateDirectory.SelectedIndex >= 0)
                    {
                        _settings.TemplateDirectory = cbTemplateDirectory.SelectedValue;
                    }

                    if(cmbLearnerModule.SelectedIndex >= 0)
                    {
                        if (cmbLearnerModule.SelectedValue != "default")
                        {
                            _settings.LearnerModuleId = Convert.ToInt32(cmbLearnerModule.SelectedValue);
                        }
                        else
                        {
                            _settings.LearnerModuleId = -1;
                        }

                    }
                    else
                    {
                        _settings.LearnerModuleId = -1;
                    }

                    _settings.ShowDateFilter = chkShowDateFilters.Checked;
                    _settings.ShowPaging = chbShowPaging.Checked;

                    _settings.BeginDate = ctlCompletionDate.StartDate;
                    _settings.EndDate = ctlCompletionDate.EndDate;
                    _settings.DateRange = ctlCompletionDate.DateRange;            

                    int pageSize = 0; if (!String.IsNullOrEmpty(txtPageSize.Text) && Int32.TryParse(txtPageSize.Text, out pageSize))
                    {
                        _settings.PageSize = pageSize;
                    }

                    _settings.Update();
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        #endregion

        #region Private Methods

        private void BindLearnerModules()
        {
            try
            {
                TabController objTabController = new TabController();
                ModuleController objModules = new ModuleController();
                PortalController objPortalController = new PortalController();
                UserController usrController = new UserController();
                ArrayList objPortals = objPortalController.GetPortals();
                int managerId = Null.NullInteger;

                cmbLearnerModule.Items.Clear();

                RadComboBoxItem objListItem2;
                objListItem2 = new RadComboBoxItem();
                objListItem2.Value = "default";
                objListItem2.Text = "None";
                cmbLearnerModule.Items.Add(objListItem2);

                foreach (PortalInfo portal in objPortals)
                {
                    if (portal.PortalID == PortalId)
                    {
                        ArrayList arrPortalModules = objModules.GetModules(portal.PortalID);
                        foreach (ModuleInfo objModule in arrPortalModules)
                        {
                            if (objModule.IsDeleted == false & objModule.DesktopModule.FriendlyName == "LMS Learner")
                            {                            
                                RadComboBoxItem objListItem;
                                objListItem = cmbLearnerModule.FindItemByValue(objModule.ModuleID.ToString());
                                if (objListItem == null)
                                {
                                    objListItem = new RadComboBoxItem();
                                    objListItem.Value = objModule.ModuleID.ToString();
                                    objListItem.Text = objModule.ModuleTitle + " (" + objModule.ModuleID.ToString() + ")";

                                    TabInfo objParentTab = objTabController.GetTab(objModule.TabID, PortalId, true);
                                    objListItem.Text = objParentTab.TabName + " -> " + objListItem.Text;

                                    bool parentTabIsDeleted = objParentTab.IsDeleted;

                                    while (objParentTab.ParentId != Null.NullInteger)
                                    {
                                        objParentTab = objTabController.GetTab(objParentTab.ParentId, PortalId, true);
                                        if (objListItem.Text.Length + objParentTab.TabName.Length > 100)
                                        {
                                            objListItem.Text = "..." + objListItem.Text;
                                            break;
                                        }
                                        objListItem.Text = objParentTab.TabName + " -> " + objListItem.Text;
                                        if (objParentTab.TabID == PortalSettings.HomeTabId)
                                            break;
                                    }

                                    if (UserController.Instance.GetCurrentUserInfo().IsSuperUser)
                                        objListItem.Text = "[" + portal.PortalID + " - " + portal.PortalName + "] " + objListItem.Text;

                                    if (!parentTabIsDeleted)
                                        cmbLearnerModule.Items.Add(objListItem);
                                    
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion
    }
}