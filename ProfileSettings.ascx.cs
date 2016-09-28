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
    public partial class ProfileSettings : ModuleSettings
    {
        #region Defines

        /// <summary>
        /// Templates directory relative path.
        /// </summary>
        private const string TEMPLATES_DIRECTORY = "~\\DesktopModules\\DNNGamification\\Templates\\Profile";

        #endregion

        #region Private Fields

        /// <summary>
        /// Leaderboard settings.
        /// </summary>
        private Infrastructure.ProfileModuleSettings _settings = null;

        #endregion

        #region Protected Methods : Event Handlers

        /// <summary>
        /// OnInit handler.
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            try // try to handle OnInit
            {
                LocalResourceFile = TemplateSourceDirectory + "/App_LocalResources/ProfileSettings.resx";

                ClientResourceManager.RegisterStyleSheet(Page, TemplateSourceDirectory + "/Css/profile.settings.css", FileOrder.Css.DefaultPriority + 1);
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
                    string path = Server.MapPath(TEMPLATES_DIRECTORY);

                    foreach (var directory in Directory.EnumerateDirectories(path))
                    {
                        var info = new DirectoryInfo(directory);
                        {
                            cbTemplateDirectory.Items.Add(new DnnComboBoxItem(info.Name, info.Name));
                        }
                    }

                    if (!String.IsNullOrEmpty(_settings.TemplateDirectory))
                    {
                        cbTemplateDirectory.SelectedValue = _settings.TemplateDirectory;
                    }

                    chbShowChart.Checked = _settings.ShowChart;
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
                _settings = Infrastructure.ProfileModuleSettings.Load(ModuleId);
                SetupPortalsList(cbPortalId, _settings.PortalId);
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

                _settings = Infrastructure.ProfileModuleSettings.Load(ModuleId);
                {
                    if (cbPortalId.SelectedIndex >= 0)
                    {
                        _settings.PortalId = Convert.ToInt32(cbPortalId.SelectedValue);
                    }

                    if (cbTemplateDirectory.SelectedIndex >= 0)
                    {
                        _settings.TemplateDirectory = cbTemplateDirectory.SelectedValue;
                    }

                    _settings.ShowChart = chbShowChart.Checked;
                    
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