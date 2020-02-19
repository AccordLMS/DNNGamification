namespace DNNGamification
{
    using DotNetNuke.Entities.Users;

    using DNNGamification.Components;
    using DNNGamification.Components.Entities;

    using DotNetNuke.Services;
    using DotNetNuke.Services.Exceptions;

    using DotNetNuke.Web.Client;
    using DotNetNuke.Web.Client.ClientResourceManagement;

    using System.Collections.Generic;

    using System;
    using System.Linq;
    using System.IO;
    using DotNetNuke.Services.Localization;

    /// <summary>
    /// Module view control.
    /// </summary>
    public partial class Profile : ModuleBase
    {
        #region Defines

        public const string TEMPLATES_PATH = "~\\DesktopModules\\DNNGamification\\Templates";

        #endregion

        #region Private Field

        /// <summary>
        /// 
        /// </summary>
        private decimal _activityPoints = 0;

        #endregion

        #region Private Fields : Templating

        /// <summary>
        /// HTML templates.
        /// </summary>
        private Dictionary<string, string> _templates = null;

        /// <summary>
        /// Profile module settings.
        /// </summary>
        private Infrastructure.ProfileModuleSettings _settings = null;

        /// <summary>
        /// Template evaluator.
        /// </summary>
        private ProfileEvaluator _razor = null;

        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        public string ToRelativeUrl(FileInfo source)
        {
            string result = source.FullName.Replace(Server.MapPath("~\\"), null);
            {
                return result.Replace(@"\", "/").Insert(0, "~/");
            }
        }

        #endregion

        #region Protected Methods : Events

        /// <summary>
        /// OnInit handler.
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            try // try to handle OnInit
            {
                string[] patterns = { "*.js", "*.css", "*.cshtml" };

                LocalResourceFile = TemplateSourceDirectory + "/App_LocalResources/Profile.resx";
                {
                    _settings = Infrastructure.ProfileModuleSettings.Load(ModuleId);
                }

                ClientResourceManager.RegisterScript(Page, TemplateSourceDirectory + "/Scripts/profile.js", FileOrder.Js.DefaultPriority + 1);
                {
                    ClientResourceManager.RegisterStyleSheet(Page, TemplateSourceDirectory + "/Css/profile.css", FileOrder.Css.DefaultPriority + 1);
                }

                _razor = new ProfileEvaluator(ModuleContext, LocalResourceFile);
                {
                    _templates = new Dictionary<string, string>();
                }

                DirectoryInfo rootDirectory = null;

                string path = String.Format("{0}\\Profile\\{1}", TEMPLATES_PATH, _settings.TemplateDirectory);
                {
                    rootDirectory = new DirectoryInfo(Server.MapPath(path));
                }

                foreach (var file in rootDirectory.EnumerateFiles(patterns[0], SearchOption.AllDirectories))
                {
                    ClientResourceManager.RegisterScript(Page, ToRelativeUrl(file), FileOrder.Js.DefaultPriority + 2);
                }
                foreach (var file in rootDirectory.EnumerateFiles(patterns[1], SearchOption.AllDirectories))
                {
                    ClientResourceManager.RegisterStyleSheet(Page, ToRelativeUrl(file), FileOrder.Css.DefaultPriority + 2);
                }

                foreach (var file in rootDirectory.EnumerateFiles(patterns[2], SearchOption.AllDirectories))
                {
                    _templates.Add(Path.GetFileNameWithoutExtension(file.Name), ToRelativeUrl(file));
                }

                base.OnInit(e);
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
                    int userId = -1;
                    int portalId = (_settings.PortalId < 0 ? PortalId : _settings.PortalId);

                    if (!Int32.TryParse(Request.QueryString["UserId"], out userId))
                    {
                        userId = UserInfo.UserID; // define user ID
                    }

                    UserActivity userActivity = UnitOfWork.UserActivities.GetBy(userId, portalId);
                    {
                        decimal points = (_activityPoints = userActivity != null ? userActivity.ActivityPoints : 0);
                    }

                    DateTime now = DateTime.Now.Date;

                    pnlChart.Visible = _settings.ShowChart;
                    if (_settings.ShowChart)
                    {
                        if (hcActivities.PlotArea.Series.Count > 0)
                        { 
                            hcActivities.PlotArea.Series[0].Name = Localization.GetString("ChartActivitiesSerie.Text", LocalResourceFile);
                        }
                        hcActivities.PlotArea.XAxis.TitleAppearance.Text = Localization.GetString("ChartActivitiesXAxis.Text", LocalResourceFile);
                        hcActivities.PlotArea.YAxis.TitleAppearance.Text = Localization.GetString("ChartActivitiesYAxis.Text", LocalResourceFile);

                        pnlAchievements.CssClass += " gmfInline";

                        DateTime start = now.AddDays(-13), end = now.AddDays(1).AddTicks(-1);

                        List<UserActivityLog> log = UnitOfWork.UserActivitiesLog.GetChart(userId, portalId, start, end);
                        {
                            var dataSource = new List<dynamic>(); // define dynamic data source

                            //dataSource.Add(new { XAxis = i.Day, YAxis = log.Count(a => (a.CreateDate.Date == i)) });

                            for (DateTime i = start; i <= end; i = i.AddDays(1))
                            {
                                dataSource.Add(new { XAxis = i.Day, YAxis = log.Where(a => (a.CreateDate.Date == i)).Sum(a => a.ActivityPoints) });
                            }

                            hcActivities.DataSource = dataSource;
                            {
                                hcActivities.DataBind(); // bind chart
                            }
                        }
                    }

                    rptBadges.DataSource = UnitOfWork.UserBadges.GetManyBy(userId, portalId);
                    {
                        rptBadges.DataBind(); // bind repeater
                    }
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Evaluates header template.
        /// </summary>
        protected string EvaluateHeader(object data)
        {
            return _razor.Evaluate(null, _activityPoints, _templates["Header"]);
        }

        /// <summary>
        /// Evaluates footer template.
        /// </summary>
        protected string EvaluateFooter(object data)
        {
            string template = (rptBadges.Items.Count > 0) ? _templates["Footer"] : _templates["FooterEmpty"];
            {
                return _razor.Evaluate(null, _activityPoints, template);
            }
        }

        /// <summary>
        /// Evaluates alternating template.
        /// </summary>
        protected string EvaluateAlternating(object data)
        {
            var item = (data as UserBadge);
            {
                return _razor.Evaluate(item, _activityPoints, _templates["Alternating"]);
            }
        }

        /// <summary>
        /// Evaluates item template.
        /// </summary>
        protected string EvaluateItem(object data)
        {
            var item = (data as UserBadge);
            {
                return _razor.Evaluate(item, _activityPoints, _templates["Item"]);
            }
        }

        #endregion
    }
}
