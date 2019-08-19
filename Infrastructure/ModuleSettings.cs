using DotNetNuke.Common.Utilities;

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

        /// <summary>
        /// 
        /// </summary>
        private int _portalId = -1;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the mapped Portal ID.
        /// </summary>
        public int PortalId
        {
            get { return _portalId; }
            set { _portalId = value; }
        }

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
                return new ProfileModuleSettings(moduleId, controller.GetModule(moduleId, Null.NullInteger, true).ModuleSettings); // GetTabModule(moduleId).TabModuleSettings);
            }
        }

        /// <summary>
        /// Updates module settings.
        /// </summary>
        public void Update()
        {
            var controller = new ModuleController();
            {
                controller.UpdateModuleSetting(_moduleId, "PortalId", PortalId.ToString());

                if (!String.IsNullOrEmpty(TemplateDirectory))
                {
                    controller.UpdateModuleSetting(_moduleId, "TemplateDirectory", TemplateDirectory.ToString());
                }

                string showChart = ShowChart.ToString();
                {
                    controller.UpdateModuleSetting(_moduleId, "ShowChart", showChart);
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

            if (settings.ContainsKey("PortalId"))
            {
                PortalId = Convert.ToInt32(settings["PortalId"]);
            }

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
        private int _portalId = -1;

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
        /// Gets or sets the mapped Portal ID.
        /// </summary>
        public int PortalId
        {
            get { return _portalId; }
            set { _portalId = value; }
        }

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
                return new LeaderboardModuleSettings(moduleId, controller.GetModule(moduleId, Null.NullInteger, true).ModuleSettings); //controller.GetTabModule(moduleId).TabModuleSettings);
            }
        }

        /// <summary>
        /// Updates module settings.
        /// </summary>
        public void Update()
        {
            var controller = new ModuleController();
            {

                controller.UpdateModuleSetting(_moduleId, "PortalId", PortalId.ToString());

                int mode = (int)LeaderboardMode;
                {
                    controller.UpdateModuleSetting(_moduleId, "LeaderboardMode", mode.ToString());
                }

                string showPaging = ShowPaging.ToString();
                {
                    controller.UpdateModuleSetting(_moduleId, "ShowPaging", showPaging);
                }

                if (!String.IsNullOrEmpty(TemplateDirectory))
                {
                    controller.UpdateModuleSetting(_moduleId, "TemplateDirectory", TemplateDirectory.ToString());
                }

                string pageSize = PageSize.ToString();
                {
                    controller.UpdateModuleSetting(_moduleId, "PageSize", pageSize);
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

            if (settings.ContainsKey("PortalId"))
            {
                PortalId = Convert.ToInt32(settings["PortalId"]);
            }

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

    /// <summary>
    /// ActivitySummary settings.
    /// </summary>
    public class ActivitySummaryModuleSettings
    {
        #region Private Fields

        /// <summary>
        /// 
        /// </summary>
        private int _moduleId = -1;

        /// <summary>
        /// 
        /// </summary>
        private int _portalId = -1;

        /// <summary>
        /// 
        /// </summary>
        private bool _showPaging = false;   

        /// <summary>
        /// 
        /// </summary>
        private string _Directory = "Wide";

        /// <summary>
        /// 
        /// </summary>
        private int _pageSize = 10;

        /// <summary>
        /// 
        /// </summary>
        private int _learnerModuleId = -1;

        /// <summary>
        /// 
        /// </summary>
        private bool _showDateFilter = false;

        /// <summary>
        /// 
        /// </summary>
        private DateTime _beginDate = Null.NullDate;

        /// <summary>
        /// 
        /// </summary>
        private DateTime _endDate = Null.NullDate;

        /// <summary>
        /// 
        /// </summary>
        private String _dateRange = "AllTime";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the mapped Portal ID.
        /// </summary>
        public int PortalId
        {
            get { return _portalId; }
            set { _portalId = value; }
        }

        /// <summary>
        /// Gets or sets the mapped Portal ID.
        /// </summary>
        public int LearnerModuleId
        {
            get { return _learnerModuleId; }
            set { _learnerModuleId = value; }
        }

        /// <summary>
        /// Gets or sets show pagination controls.
        /// </summary>
        public bool ShowPaging
        {
            get { return _showPaging; }
            set { _showPaging = value; }
        }

        /// <summary>
        /// Gets or sets show pagination controls.
        /// </summary>
        public bool ShowDateFilter
        {
            get { return _showDateFilter; }
            set { _showDateFilter = value; }
        }

        /// <summary>
        /// Gets or sets template directory.
        /// </summary>
        public string TemplateDirectory
        {
            get { return _Directory; }
            set { _Directory = value; }
        }

        /// <summary>
        /// Gets or sets page size.
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        /// <summary>
        /// Gets or sets the begin date
        /// </summary>
        public DateTime BeginDate
        {
            get { return _beginDate; }
            set { _beginDate = value; }
        }

        /// <summary>
        /// Gets or sets the begin date
        /// </summary>
        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        /// <summary>
        /// Gets or sets the date range
        /// </summary>
        public String DateRange
        {
            get { return _dateRange; }
            set { _dateRange = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads module settings.
        /// </summary>
        public static ActivitySummaryModuleSettings Load(int moduleId)
        {
            var controller = new ModuleController();
            {
                return new ActivitySummaryModuleSettings(moduleId, controller.GetModule(moduleId, Null.NullInteger, true).ModuleSettings); //controller.GetTabModule(moduleId).TabModuleSettings);
            }
        }

        /// <summary>
        /// Updates module settings.
        /// </summary>
        public void Update()
        {
            var controller = new ModuleController();
            {

                controller.UpdateModuleSetting(_moduleId, "PortalId", PortalId.ToString());

                string showPaging = ShowPaging.ToString();
                {
                    controller.UpdateModuleSetting(_moduleId, "ShowPaging", showPaging);
                }

                if (!String.IsNullOrEmpty(TemplateDirectory))
                {
                    controller.UpdateModuleSetting(_moduleId, "TemplateDirectory", TemplateDirectory.ToString());
                }

                string pageSize = PageSize.ToString();
                {
                    controller.UpdateModuleSetting(_moduleId, "PageSize", pageSize);
                }

                string showDateFilter = ShowDateFilter.ToString();
                {
                    controller.UpdateModuleSetting(_moduleId, "ShowDateFilter", showDateFilter);
                }

                string learnerModuleId = LearnerModuleId.ToString();
                {
                    controller.UpdateModuleSetting(_moduleId, "LearnerModuleId", learnerModuleId);
                }

                string beginDate = BeginDate.ToString();
                {
                    controller.UpdateModuleSetting(_moduleId, "BeginDate", beginDate);
                }

                string endDate = EndDate.ToString();
                {
                    controller.UpdateModuleSetting(_moduleId, "EndDate", endDate);
                }

                string dateRange = DateRange;
                {
                    controller.UpdateModuleSetting(_moduleId, "DateRange", dateRange);
                }
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        protected ActivitySummaryModuleSettings(int moduleId, Hashtable settings)
        {
            _moduleId = moduleId;

            int pageSize = 0; 

            bool showPaging = false;
            bool showDateFilter = false;
            int learnerModuleId = 0;
            DateTime endDate = Null.NullDate;
            DateTime beginDate = Null.NullDate;
            String dateRange = "";

            if (settings.ContainsKey("PortalId"))
            {
                PortalId = Convert.ToInt32(settings["PortalId"]);
            }

            if (settings.ContainsKey("TemplateDirectory"))
            {
                TemplateDirectory = settings["TemplateDirectory"].ToString();
            }

            if (settings.ContainsKey("LearnerModuleId") && Int32.TryParse(settings["LearnerModuleId"].ToString(), out learnerModuleId))
            {
                LearnerModuleId = learnerModuleId;
            }

            if (settings.ContainsKey("ShowPaging") && Boolean.TryParse(settings["ShowPaging"].ToString(), out showPaging))
            {
                ShowPaging = showPaging;
            }

            if (settings.ContainsKey("PageSize") && Int32.TryParse(settings["PageSize"].ToString(), out pageSize))
            {
                PageSize = pageSize;
            }

            if (settings.ContainsKey("ShowDateFilter") && Boolean.TryParse(settings["ShowDateFilter"].ToString(), out showDateFilter))
            {
                ShowDateFilter = showDateFilter;
            }

            if (settings.ContainsKey("BeginDate") && DateTime.TryParse(settings["BeginDate"].ToString(), out beginDate))
            {
                BeginDate = beginDate;
            }
            else
            {
                BeginDate = DateTime.Now;
            }

            if (settings.ContainsKey("EndDate") && DateTime.TryParse(settings["EndDate"].ToString(), out endDate))
            {
                EndDate = endDate;
            }
            else
            {
                EndDate = DateTime.Now;
            }

            if (settings.ContainsKey("DateRange"))
            {
                DateRange = settings["DateRange"].ToString();
            }
            else
            {
                DateRange = "AllTime";
            }
        }

        #endregion
    }
}