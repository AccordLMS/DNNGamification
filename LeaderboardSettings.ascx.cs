using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;


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

    /// <summary>
    /// 
    /// </summary>
    public partial class LeaderboardSettings : ModuleSettings
    {
        #region Defines

        /// <summary>
        /// Templates directory relative path.
        /// </summary>
        private const string TEMPLATES_DIRECTORY = "~\\DesktopModules\\DNNGamification\\Templates\\Leaderboard";

        #endregion

        #region Private Fields

        /// <summary>
        /// Leaderboard settings.
        /// </summary>
        private Infrastructure.LeaderboardModuleSettings _settings = null;

        #endregion

        #region Protected Methods : Event Handlers

        /// <summary>
        /// OnInit handler.
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            try // try to handle OnInit
            {
                LocalResourceFile = TemplateSourceDirectory + "/App_LocalResources/LeaderboardSettings.resx";

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
                    txtPageSize.Text = _settings.PageSize.ToString();

                    foreach (string directory in Directory.EnumerateDirectories(Server.MapPath(TEMPLATES_DIRECTORY)))
                    {
                        var info = new DirectoryInfo(directory);
                        {
                            cbTemplateDirectory.Items.Add(new ListItem(info.Name, info.Name));
                        }
                    }

                    chbShowPaging.Checked = _settings.ShowPaging;
                    {
                        cbLeaderboardMode.SelectedIndex = (int)_settings.LeaderboardMode;
                    }

                    if (!String.IsNullOrEmpty(_settings.TemplateDirectory)) // check directory name is defined
                    {
                        cbTemplateDirectory.SelectedValue = _settings.TemplateDirectory.ToString();
                    }
                    else
                    {
                        cbTemplateDirectory.SelectedValue = "Default";
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
                _settings = Infrastructure.LeaderboardModuleSettings.Load(ModuleId);
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

                _settings = Infrastructure.LeaderboardModuleSettings.Load(ModuleId);
                {

                    if (ddrPortalId.SelectedIndex >= 0)
                    {
                        _settings.PortalId = Convert.ToInt32(ddrPortalId.SelectedValue);
                    }

                    if (cbTemplateDirectory.SelectedIndex >= 0)
                    {
                        _settings.TemplateDirectory = cbTemplateDirectory.SelectedValue;
                    }

                    _settings.ShowPaging = chbShowPaging.Checked;

                    int pageSize = 0; if (!String.IsNullOrEmpty(txtPageSize.Text) && Int32.TryParse(txtPageSize.Text, out pageSize))
                    {
                        _settings.PageSize = pageSize;
                    }

                    if (cbLeaderboardMode.SelectedIndex >= 0)
                    {
                        _settings.LeaderboardMode = (LeaderboardMode)cbLeaderboardMode.SelectedIndex;
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
    }
}