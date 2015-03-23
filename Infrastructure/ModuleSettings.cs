namespace DNNGamification.Infrastructure
{
    using DotNetNuke.Entities.Modules;

    using System;
    using System.Collections;

    /// <summary>
    /// Profile settings.
    /// </summary>
    public class ProfileModuleSettings
    {
        #region Private Fields

        /// <summary>
        /// 
        /// </summary>
        private bool _showChart = true;

        /// <summary>
        /// 
        /// </summary>
        private string _templateDirectory = "Wide";
        
        /// <summary>
        /// 
        /// </summary>
        private int _moduleId = -1;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets show activity chart.
        /// </summary>
        public bool ShowChart
        {
            get { return _showChart; } set { _showChart = value; }
        }

        /// <summary>
        /// Gets or sets achievements template directory.
        /// </summary>
        public string TemplateDirectory
        {
            get { return _templateDirectory; } set { _templateDirectory = value; }
        }

        /// <summary>
        /// Gets or sets module ID.
        /// </summary>
        public int ModuleId
        {
            get { return _moduleId; } set { _moduleId = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads module settings.
        /// </summary>
        public static ProfileModuleSettings Load(int moduleId)
        {
            var controller = new ModuleController();
            {
                return new ProfileModuleSettings(moduleId, controller.GetTabModule(moduleId).TabModuleSettings);
            }
        }

        /// <summary>
        /// Updates module settings.
        /// </summary>
        public void Update()
        {
            var controller = new ModuleController();
            {
                if (!String.IsNullOrEmpty(TemplateDirectory))
                {
                    controller.UpdateTabModuleSetting(_moduleId, "TemplateDirectory", TemplateDirectory.ToString());
                }

                string showChart = ShowChart.ToString();
                {
                    controller.UpdateTabModuleSetting(_moduleId, "ShowChart", showChart);
                }
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        protected ProfileModuleSettings(int moduleId, Hashtable settings)
        {
            _moduleId = moduleId; bool showChart = false;
            
            if (settings.ContainsKey("ShowChart")) // check show activity chart
            {
                if (Boolean.TryParse(settings["ShowChart"].ToString(), out showChart)) ShowChart = showChart;
            }

            if (settings.ContainsKey("TemplateDirectory"))
            {
                TemplateDirectory = settings["TemplateDirectory"].ToString();
            }
        }

        #endregion
    }

    /// <summary>
    /// Leaderboard settings.
    /// </summary>
    public class LeaderboardModuleSettings
    {
        #region Private Fields

        /// <summary>
        /// 
        /// </summary>
        private int _moduleId = -1;

        /// <summary>
        /// 
        /// </summary>
        private bool _showPaging = false;

        /// <summary>
        /// 
        /// </summary>
        private LeaderboardMode _leaderboardMode = LeaderboardMode.All;

        /// <summary>
        /// 
        /// </summary>
        private string _Directory = "Wide";

        /// <summary>
        /// 
        /// </summary>
        private int _pageSize = 10;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets leaderboard mode.
        /// </summary>
        public LeaderboardMode LeaderboardMode
        {
            get { return _leaderboardMode; } set { _leaderboardMode = value; }
        }

        /// <summary>
        /// Gets or sets show pagination controls.
        /// </summary>
        public bool ShowPaging
        {
            get { return _showPaging; } set { _showPaging = value; }
        }

        /// <summary>
        /// Gets or sets template directory.
        /// </summary>
        public string TemplateDirectory
        {
            get { return _Directory; } set { _Directory = value; }
        }

        /// <summary>
        /// Gets or sets page size.
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; } set { _pageSize = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads module settings.
        /// </summary>
        public static LeaderboardModuleSettings Load(int moduleId)
        {
            var controller = new ModuleController();
            {
                return new LeaderboardModuleSettings(moduleId, controller.GetTabModule(moduleId).TabModuleSettings);
            }
        }

        /// <summary>
        /// Updates module settings.
        /// </summary>
        public void Update()
        {
            var controller = new ModuleController();
            {
                int mode = (int)LeaderboardMode;
                {
                    controller.UpdateTabModuleSetting(_moduleId, "LeaderboardMode", mode.ToString());
                }

                string showPaging = ShowPaging.ToString();
                {
                    controller.UpdateTabModuleSetting(_moduleId, "ShowPaging", showPaging);
                }

                if (!String.IsNullOrEmpty(TemplateDirectory))
                {
                    controller.UpdateTabModuleSetting(_moduleId, "TemplateDirectory", TemplateDirectory.ToString());
                }

                string pageSize = PageSize.ToString();
                {
                    controller.UpdateTabModuleSetting(_moduleId, "PageSize", pageSize);
                }
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        protected LeaderboardModuleSettings(int moduleId, Hashtable settings)
        {
            _moduleId = moduleId;

            int pageSize = 0; int leaderboardMode = 0;

            bool showPaging = false;

            if (settings.ContainsKey("TemplateDirectory"))
            {
                TemplateDirectory = settings["TemplateDirectory"].ToString();
            }

            if (settings.ContainsKey("ShowPaging") && Boolean.TryParse(settings["ShowPaging"].ToString(), out showPaging))
            {
                ShowPaging = showPaging;
            }

            if (settings.ContainsKey("LeaderboardMode") && Int32.TryParse(settings["LeaderboardMode"].ToString(), out leaderboardMode))
            {
                LeaderboardMode = (LeaderboardMode)leaderboardMode;
            }

            if (settings.ContainsKey("PageSize") && Int32.TryParse(settings["PageSize"].ToString(), out pageSize))
            {
                PageSize = pageSize;
            }
        }

        #endregion
    }
}