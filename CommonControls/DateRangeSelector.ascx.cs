using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Services.Localization;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Portals;

namespace DNNGamification.CommonControls
{
    public partial class DateRangeSelector : DotNetNuke.Framework.UserControlBase
    {
        #region "Private Members"

        private int _moduleId = Null.NullInteger;
        private string _DateRange = Null.NullString;
        private Interval _Interval = Interval.None;
        private DateTime _StartDate = Null.NullDate;
        private DateTime _EndDate = Null.NullDate;

        private bool _ShowInterval = false;
        private bool _ShowCustom = true;

        #endregion


        #region "Public consts"

        public const string DateRangeAllTime = "AllTime";
        public const string DateRangeToday = "Today";
        public const string DateRangeYesterday = "Yesterday";
        public const string DateRangeThisWeek = "ThisWeek";
        public const string DateRangeLast7Days = "Last7Days";
        public const string DateRangeLastWeek = "LastWeek";
        public const string DateRangeThisMonth = "ThisMonth";
        public const string DateRangeLast30Days = "Last30Days";
        public const string DateRangeLastMonth = "LastMonth";
        public const string DateRangeCustom = "Custom";


        public enum Interval
        {
            Hour = 0,
            Year = 1,
            Month = 2,
            Day = 3,
            None = 4
        }

        #endregion


        #region "Public Properties"

        /// <summary>
        ///         ''' present or not the text labels
        ///         ''' </summary>
        ///         ''' <value></value>
        ///         ''' <remarks></remarks>
        public bool ShowLabels
        {
            set
            {
                tdStartDate.Visible = value;
                tdEndDate.Visible = value;
                tdRBLInterval.Visible = value;
            }
        }

        /// <summary>
        ///         ''' presents or not the option to choose a custom start and end date by
        ///         ''' entering each date
        ///         ''' </summary>
        ///         ''' <value></value>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public bool ShowCustom
        {
            get
            {
                return _ShowCustom;
            }
            set
            {
                _ShowCustom = value;
                ViewState["ShowCustom"] = value;
            }
        }

        /// <summary>
        ///         ''' Valid values:
        ///         ''' Consts.DateRangeAllTime = "AllTime"
        ///         ''' Consts.DateRangeToday = "Today"
        ///         ''' Consts.DateRangeYesterday = "Yesterday"
        ///         ''' Consts.DateRangeThisWeek = "ThisWeek"
        ///         ''' Consts.DateRangeLast7Days = "Last7Days"
        ///         ''' Consts.DateRangeLastWeek = "LastWeek"
        ///         ''' Consts.DateRangeThisMonth = "ThisMonth"
        ///         ''' Consts.DateRangeLast30Days = "Last30Days"
        ///         ''' Consts.DateRangeLastMonth = "LastMonth"
        ///         ''' Consts.DateRangeCustom = "Custom"
        ///         ''' </summary>
        ///         ''' <value></value>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public string DateRange
        {
            get
            {
                if (cmbDateRange.SelectedItem != null)
                    return cmbDateRange.SelectedValue.ToString();
                else
                    return Null.NullString;
            }
            set
            {
                _DateRange = value;

                if (cmbDateRange.Items.Count == 0)
                    LoadDateRange();
                ListItem lItem;
                lItem = cmbDateRange.Items.FindByValue(value);
                if (lItem != null)
                {
                    cmbDateRange.ClearSelection();
                    lItem.Selected = true;
                }
                ViewState["DateRange"] = value;
            }
        }

        /// <summary>
        ///         ''' Get returns the selected start date.
        ///         ''' Set only valid if you seted custom date in daterange
        ///         ''' </summary>
        ///         ''' <value></value>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public DateTime StartDate
        {
            get
            {
                this.LoadSettings();
                if (DateRange != DateRangeAllTime)
                    return _StartDate;
                else
                    return _StartDate;
            }
            set
            {
                _StartDate = value;
                ViewState["StartDate"] = value;
                if (_DateRange == DateRangeCustom)
                {
                    TrStartDate.Style["display"] = string.Empty;
                    rdpStartDate.SelectedDate = _StartDate;
                }
            }
        }

        /// <summary>
        ///         ''' Get returns the selected start date.
        ///         ''' Set only valid if you seted custom date in daterange
        ///         ''' </summary>
        ///         ''' <value></value>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public DateTime EndDate
        {
            get
            {
                this.LoadSettings();
                if (DateRange != DateRangeAllTime)
                    return _EndDate;
                else
                    return _EndDate;
            }
            set
            {
                _EndDate = value;
                ViewState["EndDate"] = value;
                if (_DateRange == DateRangeCustom)
                {
                    TrEndDate.Style["display"] = string.Empty;
                    rdpEndDate.SelectedDate = _EndDate;
                }
            }
        }


        /// <summary>
        ///         ''' Get and sets the actual selected interval. See ShowInterval
        ///         ''' </summary>
        ///         ''' <value></value>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public Interval SelectedInterval
        {
            get
            {
                if (TrInterval.Visible)
                {
                    if (this.RBLInterval.SelectedItem != null)
                        return (Interval)Enum.Parse(typeof(Interval), this.RBLInterval.SelectedValue);
                    else
                        return Interval.None;
                }
                else
                    return Interval.None;
            }
            set
            {
                _Interval = value;
                if (RBLInterval.Items.Count > 0)
                    RBLInterval.SelectedValue = value.ToString();
            }
        }

        /// <summary>
        ///         ''' if true shows the controls to choose an interval related with the 
        ///         ''' start and end date. This is used in the traffic report.
        ///         ''' (do the report from this startdate to this enddate with this interval)
        ///         ''' </summary>
        ///         ''' <value></value>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public bool ShowInterval
        {
            get
            {
                return _ShowInterval;
            }
            set
            {
                _ShowInterval = value;
                TrInterval.Visible = value;
                ViewState["ShowInterval"] = value;
            }
        }

        public string LocalResourceFile
        {
            get
            {
                return this.ResolveUrl(Localization.LocalResourceDirectory + "/DateRangeSelector.ascx");
            }
        }

        #endregion


        #region "Private Methods"

        private void LocalizeLabels()
        {
            lblStartDate.Text = Localization.GetString("plStartDate.Text", LocalResourceFile);
            lblEndDate.Text = Localization.GetString("plEndDate.Text", LocalResourceFile);
            lblInterval.Text = Localization.GetString("plInterval", LocalResourceFile);
            valDateRange.Text = Localization.GetString("valDateRange.Text", LocalResourceFile);
        }

        private DateTime GetLastSunday(DateTime _date)
        {
            while (_date.DayOfWeek == DayOfWeek.Sunday)
                _date.AddDays(-1);
            return _date;
        }

        private DateTime Get2LastSunday(DateTime _date)
        {
            bool firstSunday = false;
            while (_date.DayOfWeek == DayOfWeek.Sunday & firstSunday == true)
            {
                if (_date.DayOfWeek == DayOfWeek.Sunday)
                    firstSunday = true;
                _date.AddDays(-1);
            }
            return _date;
        }

        private void LoadSettings()
        {
            TimeSpan ltimeSpan;

            if (DateRange == DateRangeCustom)
            {
                if (rdpStartDate.SelectedDate != null)
                {
                    _StartDate = rdpStartDate.SelectedDate.Value;
                    _StartDate = new DateTime(_StartDate.Year, _StartDate.Month, _StartDate.Day, 0, 0, 0);
                }
                else
                    _StartDate = Null.NullDate;

                if (rdpEndDate.SelectedDate != null)
                {
                    _EndDate = rdpEndDate.SelectedDate.Value;
                    _EndDate = new DateTime(_EndDate.Year, _EndDate.Month, _EndDate.Day, 23, 59, 59);
                }
                else
                    _EndDate = Null.NullDate;
            }
            else
            {
                _DateRange = cmbDateRange.SelectedValue;
                switch (_DateRange)
                {
                    case DateRangeToday:
                        {
                            _StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                            _EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                            break;
                        }

                    case DateRangeYesterday:
                        {
                            ltimeSpan = new TimeSpan(1, 0, 0, 0);
                            _StartDate = DateTime.Now.Subtract(ltimeSpan);
                            _StartDate = new DateTime(_StartDate.Year, _StartDate.Month, _StartDate.Day, 0, 0, 0);
                            _EndDate = new DateTime(_StartDate.Year, _StartDate.Month, _StartDate.Day, 23, 59, 59);
                            break;
                        }

                    case DateRangeThisWeek:
                        {
                            ltimeSpan = new TimeSpan((int)DateTime.Now.DayOfWeek, 0, 0, 0);
                            _StartDate = DateTime.Now.Subtract(ltimeSpan);
                            _StartDate = new DateTime(_StartDate.Year, _StartDate.Month, _StartDate.Day, 0, 0, 0);
                            ltimeSpan = new TimeSpan(7 - (int)DateTime.Now.DayOfWeek - 1, 0, 0, 0);
                            _EndDate = DateTime.Now.Add(ltimeSpan);
                            _EndDate = new DateTime(_EndDate.Year, _EndDate.Month, _EndDate.Day, 23, 59, 59);
                            break;
                        }

                    case DateRangeLast7Days:
                        {
                            ltimeSpan = new TimeSpan(7, 0, 0, 0);
                            _StartDate = DateTime.Now.Subtract(ltimeSpan);
                            _StartDate = new DateTime(_StartDate.Year, _StartDate.Month, _StartDate.Day, 0, 0, 0);
                            _EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                            break;
                        }

                    case DateRangeLastWeek:
                        {
                            ltimeSpan = new TimeSpan((int)DateTime.Now.DayOfWeek + 7, 0, 0, 0);
                            _StartDate = DateTime.Now.Subtract(ltimeSpan);
                            _StartDate = new DateTime(_StartDate.Year, _StartDate.Month, _StartDate.Day, 0, 0, 0);
                            ltimeSpan = new TimeSpan((int)DateTime.Now.DayOfWeek + 1, 0, 0, 0);
                            _EndDate = DateTime.Now.Subtract(ltimeSpan);
                            _EndDate = new DateTime(_EndDate.Year, _EndDate.Month, _EndDate.Day, 23, 59, 59);
                            break;
                        }

                    case DateRangeThisMonth:
                        {
                            _StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                            _StartDate = new DateTime(_StartDate.Year, _StartDate.Month, _StartDate.Day, 0, 0, 0);
                            _EndDate = _StartDate.AddDays(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - 1);
                            _EndDate = new DateTime(_EndDate.Year, _EndDate.Month, _EndDate.Day, 23, 59, 59);
                            break;
                        }

                    case DateRangeLast30Days:
                        {
                            ltimeSpan = new TimeSpan(30, 0, 0, 0);
                            _StartDate = DateTime.Now.Subtract(ltimeSpan);
                            _StartDate = new DateTime(_StartDate.Year, _StartDate.Month, _StartDate.Day, 0, 0, 0);
                            _EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                            break;
                        }

                    case DateRangeLastMonth:
                        {
                            _StartDate = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, 1);
                            _StartDate = new DateTime(_StartDate.Year, _StartDate.Month, _StartDate.Day, 0, 0, 0);
                            _EndDate = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, DateTime.DaysInMonth(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month));
                            _EndDate = new DateTime(_EndDate.Year, _EndDate.Month, _EndDate.Day, 23, 59, 59);
                            break;
                        }
                }
            }
        }


        private void LoadDateRange()
        {
            this.cmbDateRange.Items.Add(new ListItem(Localization.GetString(DateRangeAllTime, LocalResourceFile), DateRangeAllTime));
            this.cmbDateRange.Items.FindByValue(DateRangeAllTime).Selected = true;
            this.cmbDateRange.Items.Add(new ListItem(Localization.GetString(DateRangeToday, LocalResourceFile), DateRangeToday));
            this.cmbDateRange.Items.Add(new ListItem(Localization.GetString(DateRangeYesterday, LocalResourceFile), DateRangeYesterday));
            this.cmbDateRange.Items.Add(new ListItem(Localization.GetString(DateRangeThisWeek, LocalResourceFile), DateRangeThisWeek));
            this.cmbDateRange.Items.Add(new ListItem(Localization.GetString(DateRangeLast7Days, LocalResourceFile), DateRangeLast7Days));
            this.cmbDateRange.Items.Add(new ListItem(Localization.GetString(DateRangeLastWeek, LocalResourceFile), DateRangeLastWeek));
            this.cmbDateRange.Items.Add(new ListItem(Localization.GetString(DateRangeThisMonth, LocalResourceFile), DateRangeThisMonth));
            this.cmbDateRange.Items.Add(new ListItem(Localization.GetString(DateRangeLast30Days, LocalResourceFile), DateRangeLast30Days));
            this.cmbDateRange.Items.Add(new ListItem(Localization.GetString(DateRangeLastMonth, LocalResourceFile), DateRangeLastMonth));
            if (ShowCustom)
                this.cmbDateRange.Items.Add(new ListItem(Localization.GetString(DateRangeCustom, LocalResourceFile), DateRangeCustom));
        }

        private void BindInteval()
        {
            if (TrInterval.Visible)
            {
                this.RBLInterval.Items.Clear();

                foreach (Interval objInterval in Enum.GetValues(typeof(Interval)))
                {
                    ListItem item = new ListItem(Localization.GetString(objInterval.ToString(), LocalResourceFile), objInterval.ToString());
                    RBLInterval.Items.Add(item);
                }
            }
        }

        public void RefreshInterval()
        {
            switch (_Interval)
            {
                case Interval.Hour:
                    {
                        this.RBLInterval.Items[0].Selected = true;
                        break;
                    }

                case Interval.Year:
                    {
                        this.RBLInterval.Items[1].Selected = true;
                        break;
                    }

                case Interval.Month:
                    {
                        this.RBLInterval.Items[2].Selected = true;
                        break;
                    }

                case Interval.Day:
                    {
                        this.RBLInterval.Items[3].Selected = true;
                        break;
                    }

                case Interval.None:
                    {
                        this.RBLInterval.Items[4].Selected = true;
                        break;
                    }

                default:
                    {
                        switch (_DateRange)
                        {
                            case DateRangeAllTime:
                                {
                                    break;
                                }

                            case DateRangeCustom:
                                {
                                    this.RBLInterval.Items[1].Selected = true;
                                    break;
                                }

                            case DateRangeToday:
                                {
                                    break;
                                }

                            case DateRangeYesterday:
                                {
                                    this.RBLInterval.Items[0].Selected = true;
                                    break;
                                }

                            case DateRangeLast7Days:
                                {
                                    break;
                                }

                            case DateRangeLastWeek:
                                {
                                    break;
                                }

                            case DateRangeLastMonth:
                                {
                                    this.RBLInterval.Items[3].Selected = true;
                                    break;
                                }

                            default:
                                {
                                    this.RBLInterval.Items[1].Selected = true;
                                    break;
                                }
                        }

                        break;
                    }
            }
        }


        /// <summary>
        ///         ''' This javascript function registered here, controls the show and hide
        ///         ''' depending on the selected range type selected, of the available intervals
        ///         ''' </summary>
        ///         ''' <remarks></remarks>
        private void RegisterOnDateRangeChange()
        {
            string cmbDateRangeOnChangePart1 = string.Empty;
            string cmbDateRangeOnChangePart2 = string.Empty;

            if (ShowInterval)
            {
                string script = "function ShowHideIntervals (inicio) {" + 
                    "   var cmbDate = document.getElementById('" + cmbDateRange.ClientID + "'); " + 
                    "   var table = document.getElementById('" + RBLInterval.ClientID + "');" + 
                    "   var rows = table.getElementsByTagName('tr');" + 
                    "   switch (cmbDate.options[cmbDate.selectedIndex].value){ " + 
                    "           case '" + DateRangeCustom + "':" + 
                    "           case '" + DateRangeAllTime + "':" + 
                    "               rows[0].style.display = 'none';" + 
                    "               rows[1].style.display = '';" + 
                    "               rows[2].style.display = '';" + 
                    "               rows[3].style.display = '';" + 
                    "               rows[4].style.display = '';" + 
                    "               if (inicio == 'false'){ " + 
                    "                   rows[1].getElementsByTagName('input')[0].checked='checked';" + 
                    "               }" + 
                    "           break;" + 
                    "           case '" + DateRangeToday + "':" + 
                    "           case '" + DateRangeYesterday + "':" + 
                    "               rows[0].style.display = '';" + 
                    "               rows[1].style.display = 'none';" + 
                    "               rows[2].style.display = 'none';" + 
                    "               rows[3].style.display = 'none';" + 
                    "               rows[4].style.display = '';" + 
                    "               if (inicio == 'false'){ " + 
                    "                   rows[0].getElementsByTagName('input')[0].checked='checked';" + 
                    "               }" + 
                    "           break;" + 
                    "           case '" + DateRangeLast7Days + "':" + 
                    "           case '" + DateRangeLastWeek + "':" + 
                    "           case '" + DateRangeLastMonth + "':" + 
                    "           case '" + DateRangeThisWeek + "':" + 
                    "           case '" + DateRangeThisMonth + "':" + 
                    "           case '" + DateRangeLast30Days + "':" + 
                    "               rows[0].style.display = 'none';" + 
                    "               rows[1].style.display = 'none';" + 
                    "               rows[2].style.display = 'none';" + 
                    "               rows[3].style.display = '';" + 
                    "               rows[4].style.display = '';" + 
                    "               if (inicio == 'false'){ " + 
                    "                   rows[3].getElementsByTagName('input')[0].checked='checked';" + 
                    "               }" + 
                    "           break;" + 
                    "       } " + 
                    "   } ";

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "DnneLeaening_ShowOrHideIntervals", script, true);

                Page.ClientScript.RegisterStartupScript(this.GetType(), "IZMLMS_onloadShowOrHideIntervals", "ShowHideIntervals('true');", true);

                cmbDateRangeOnChangePart1 = "   ShowHideIntervals('false'); ";
            }

            TimeSpan ltimeSpan;
            ltimeSpan = new TimeSpan(7, 0, 0, 0);

            cmbDateRangeOnChangePart2 = "   var trStartDate = document.getElementById('" + TrStartDate.ClientID + "'); " + 
                "   var trEndDate = document.getElementById('" + TrEndDate.ClientID + "'); " + 
                "   var cmbDate = document.getElementById('" + cmbDateRange.ClientID + "'); " + 
                "   var validator = document.getElementById('" + valDateRange.ClientID + "');" + 
                "       switch (cmbDate.options[cmbDate.selectedIndex].value){ " + 
                "           case '" + DateRangeCustom + "':" + 
                "               trStartDate.style.display = '';" + 
                "               trEndDate.style.display = '';" + 
                "               document.getElementById('" + tblRangeValidator.ClientID + "').style.display = '';" + 
                "               ValidatorEnable(validator, true);" + 
                "           break;" + 
                "           default:" + 
                "               trStartDate.style.display = 'none';" + 
                "               trEndDate.style.display = 'none';" + 
                "               document.getElementById('" + tblRangeValidator.ClientID + "').style.display = 'none';" + 
                "               ValidatorEnable(validator, false);" + 
                "           break;" + "       }; ";

            // This is executed only when you change the dropdown, not when the page load, to avoid lost user selection
            // but if the user change to another dropdown value, and then get back to custom, the values are reset to defaults
            string cmbDateRangeOnChangePart3 = "   $find('" + rdpStartDate.ClientID + "').set_selectedDate(new Date());" + "   $find('" + rdpEndDate.ClientID + "').set_selectedDate(new Date());";

            // Register startup script because after select Generate Report you can't Refresh the Report.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "DnneLeaening_RefreshCboDates", "function RefreshCboDates () { try{ " + cmbDateRangeOnChangePart2 + "} catch(err) {} } ", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DnneLeaening_onloadRefreshCboDates", "RefreshCboDates();", true);

            cmbDateRange.Attributes.Add("onChange", cmbDateRangeOnChangePart1 + cmbDateRangeOnChangePart2 + cmbDateRangeOnChangePart3);
        }


        #endregion


        #region "Event Handlers"

        protected void Page_Init(object sender, System.EventArgs e)
        {
            RegisterOnDateRangeChange();
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                LocalizeLabels();

                if (!Page.IsPostBack)
                {
                    if (DateRange == DateRangeCustom)
                    {
                        TrEndDate.Style["display"] = string.Empty;
                        TrStartDate.Style["display"] = string.Empty;
                    }

                    if (_DateRange == Null.NullString | _DateRange != DateRangeCustom)
                    {
                        TrEndDate.Style.Clear();
                        TrEndDate.Style.Add("display", "none");
                        TrStartDate.Style.Clear();
                        TrStartDate.Style.Add("display", "none");
                    }

                    if (cmbDateRange.Items.Count == 0)
                        this.LoadDateRange();

                    if ((_DateRange != DateRangeCustom) && (rdpStartDate.SelectedDate != null & rdpEndDate.SelectedDate != null))
                    {
                        TimeSpan ltimeSpan;
                        ltimeSpan = new TimeSpan(7, 0, 0, 0);
                        rdpStartDate.SelectedDate = ConvertToPortalTimeZone(DateTime.Now.ToUniversalTime(), PortalSettings.Current).Subtract(ltimeSpan);
                        rdpEndDate.SelectedDate = new DateTime(ConvertToPortalTimeZone(DateTime.Now.ToUniversalTime(), PortalSettings.Current).Year, ConvertToPortalTimeZone(DateTime.Now.ToUniversalTime(), PortalSettings.Current).Month, ConvertToPortalTimeZone(DateTime.Now.ToUniversalTime(), PortalSettings.Current).Day, 23, 59, 59);
                    }

                    if (!ShowInterval)
                        this.TrInterval.Visible = false;
                    else
                    {
                        this.TrInterval.Visible = true;
                        BindInteval();
                        RefreshInterval();
                    }
                }

                if (DateRange == DateRangeCustom)
                {
                    TrStartDate.Attributes.Remove("style");
                    TrStartDate.Attributes.Add("style", "display:''");
                    TrEndDate.Attributes.Remove("style");
                    TrEndDate.Attributes.Add("style", "display:''");
                }
                else
                {
                    TrStartDate.Attributes.Remove("style");
                    TrStartDate.Attributes.Add("style", "display:none");
                    TrEndDate.Attributes.Remove("style");
                    TrEndDate.Attributes.Add("style", "display:none");
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion


        public static DateTime ConvertToPortalTimeZone(DateTime DateTimeToConvert, PortalSettings portalSettings)
        {
            if (DateTimeToConvert != Null.NullDate)
                return DateTimeToConvert.AddMinutes(GetPortalTimeZone(portalSettings.PortalId).GetUtcOffset(DateTime.Now).TotalMinutes);
            else
                return Null.NullDate;
        }

        public static TimeZoneInfo GetPortalTimeZone(int portalId)
        {
            try
            {
                //check if there is a PortalSetting
                string timeZoneId = PortalController.GetPortalSetting("TimeZone", portalId, string.Empty);
                if (!string.IsNullOrEmpty(timeZoneId))
                {
                    var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                    if (timeZone != null)
                        return timeZone;
                }

                return TimeZoneInfo.Local;
            }
            catch (TimeZoneNotFoundException)
            {
                //If the timezone ID could not be found, we use TimeZoneInfo.Local
                return TimeZoneInfo.Local;
            }
        }

    }
}