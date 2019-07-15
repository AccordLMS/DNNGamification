using System.Web;
using DotNetNuke.Services.Localization;

namespace DNNGamification
{
    using DNNGamification.Models;

    using DNNGamification.Components;
    using DNNGamification.Components.Entities;

    using DotNetNuke.Common.Utilities;

    using DotNetNuke.Entities;
    using DotNetNuke.Entities.Users;

    using DotNetNuke.Web.Client;
    using DotNetNuke.Web.Client.ClientResourceManagement;

    using DotNetNuke.Services.Exceptions;

    using System;
    using System.Data;
    using System.IO;

    using System.Collections.Generic;

    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Telerik.Web.UI;

  


    /// <summary>
    /// Module view control.
    /// </summary>
    public partial class ActivitySummary : ModuleBase
    {
        #region Defines

        public const string TEMPLATES_PATH = "~\\DesktopModules\\DNNGamification\\Templates";

        #endregion

        #region Private Fields

        /// <summary>
        /// Rad AJAX manager.
        /// </summary>
        private RadAjaxManager _ajaxManager = null;

        #endregion

        #region Private Fields : Templating

        /// <summary>
        /// HTML templates.
        /// </summary>
        private Dictionary<string, string> _templates = null;

        /// <summary>
        /// ActivitySummary module settings.
        /// </summary>
        private Infrastructure.ActivitySummaryModuleSettings _settings = null;

        /// <summary>
        /// Template evaluator.
        /// </summary>
        private ActivitySummaryEvaluator _razor = null;

        #endregion

        #region Private Properties

        /// <summary>
        /// 
        /// </summary>
        private int CurrentPage
        {
            get { return ViewState["CurrentPage"] == null ? 0 : (int)ViewState["CurrentPage"]; }

            set { ViewState["CurrentPage"] = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Bind repeater.
        /// </summary>
        private void BindActivitySummary()
        {
            try // try to handle BindActivitySummary
            {                        
                int? userId = UserInfo.UserID;
                
                int total = -1; // define activity summary total records
                int portalId = (_settings.PortalId <0 ? PortalId : _settings.PortalId);

                DateTime beginDate = _settings.BeginDate;
                DateTime endDate = _settings.EndDate;         

                List<UserActivitySummary> dataSource = UnitOfWork.UserActivitiesLog.GetUserActivitySummary
                (
                    userId, portalId, beginDate, endDate
                );

                total = dataSource.Count;

                var source = new PagedDataSource { DataSource = dataSource, VirtualCount = total };
                {
                    source.AllowPaging = true; source.AllowServerPaging = true; // define allow server paging
                }

                source.PageSize = _settings.PageSize; source.CurrentPageIndex = CurrentPage;
                {
                    ViewState["TotalPages"] = source.PageCount;
                }

                pnlPagination.Visible = (source.PageCount > 1 && _settings.ShowPaging); // define navigation enabled
                {
                    btnPrevious.Enabled = !source.IsFirstPage; btnNext.Enabled = !source.IsLastPage;
                }

                lblTotal.Text = String.Format // build total label text
                (
                    @"{0} {1} {2} {3}", LocalizeString("Page.Label"), (CurrentPage + 1), LocalizeString("PageOf.Label"), source.PageCount
                );

                rptLeaderboard.DataSource = source;
                {
                    rptLeaderboard.DataBind(); // bind repeater
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

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

        #region Protected Methods : Event Handlers

        /// <summary>
        /// OnInit handler.
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            try // try to handle OnInit
            {
                string[] patterns = { "*.js", "*.css", "*.cshtml" };

                LocalResourceFile = TemplateSourceDirectory + "/App_LocalResources/ActivitySummary.resx";                
                {
                    _settings = Infrastructure.ActivitySummaryModuleSettings.Load(ModuleId);
                }

                ClientResourceManager.RegisterScript(Page, TemplateSourceDirectory + "/Scripts/leaderboard.js", FileOrder.Js.DefaultPriority + 1);
                {
                    ClientResourceManager.RegisterStyleSheet(Page, TemplateSourceDirectory + "/Css/leaderboard.css", FileOrder.Css.DefaultPriority + 1);
                }

                _razor = new ActivitySummaryEvaluator(ModuleContext, LocalResourceFile);
                {
                    _templates = new Dictionary<string, string>();
                }

                DirectoryInfo rootDirectory = null;

                string path = String.Format("{0}\\ActivitySummary\\{1}", TEMPLATES_PATH, _settings.TemplateDirectory);
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
                _ajaxManager = null;

                if ((_ajaxManager = RadAjaxManager.GetCurrent(Page)) == null)
                {
                    _ajaxManager = new RadAjaxManager() { ID = "amLeaderboard", EnableAJAX = true };
                    {
                        plhAjaxManager.Controls.Add(_ajaxManager);
                    }
                }

                var apMain = new AjaxSetting("apMain");
                {
                    apMain.UpdatedControls.Add(new AjaxUpdatedControl("apMain", "alpMain"));
                    {
                        _ajaxManager.AjaxSettings.Add(apMain);
                    }
                }

                if (!IsPostBack)
                {
                    BindActivitySummary();
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// rptPages_ItemCommand handler.
        /// </summary>
        protected void rptPages_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try // try to handle rptPages_ItemCommand
            {
                if (e.CommandName.Equals("navigate"))
                {
                    CurrentPage = Utils.ConvertTo<int>(e.CommandArgument.ToString()); BindActivitySummary();
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// rptPages_ItemDataBound handler.
        /// </summary>
        protected void rptPages_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try // try to handle rptPages_ItemDataBound
            {
                LinkButton btnPage = (LinkButton)e.Item.FindControl("btnPage");

                if (btnPage != null && btnPage.CommandArgument.ToString() == CurrentPage.ToString())
                {
                    btnPage.Enabled = false;
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// btnFirst_Click handler.
        /// </summary>
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            try // try to handle btnFirst_Click
            {
                CurrentPage = 0; BindActivitySummary(); // bind activity summary with data
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// btnLast_Click handler.
        /// </summary>
        protected void btnLast_Click(object sender, EventArgs e)
        {
            try // try to handle btnLast_Click
            {
                CurrentPage = (Utils.ConvertTo<int>(ViewState["TotalPages"]) - 1); BindActivitySummary();
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// btnPrevious_Click handler.
        /// </summary>
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            try // try to handle btnPrevious_Click
            {
                CurrentPage -= 1; BindActivitySummary(); // bind activity summary with data
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// btnNext_Click handler.
        /// </summary>
        protected void btnNext_Click(object sender, EventArgs e)
        {
            try // try to handle btnNext_Click
            {
                CurrentPage += 1; BindActivitySummary(); // bind activity summary with data
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
            int userId = UserInfo.UserID; var item = (data as UserActivitySummary);
            {
                return _razor.Evaluate(item, _templates["Header"]);
            }
        }

        /// <summary>
        /// Evaluates alternating template.
        /// </summary>
        protected string EvaluateAlternating(object data)
        {
            int userId = UserInfo.UserID; var item = (data as UserActivitySummary);

            string template = (userId == item.UserId) ? _templates["AlternatingHighlighted"] : _templates["Alternating"];
            {
                return _razor.Evaluate(item, template);
            }
        }

        /// <summary>
        /// Evaluates item template.
        /// </summary>
        protected string EvaluateItem(object data)
        {
            int userId = UserInfo.UserID; var item = (data as UserActivitySummary);

            string template = (userId == item.UserId) ? _templates["ItemHighlighted"] : _templates["Item"];
            {
                return _razor.Evaluate(item, template);
            }
        }

        /// <summary>
        /// Evaluates footer template.
        /// </summary>
        protected string EvaluateFooter(object data)
        {
            int userId = UserInfo.UserID; var item = (data as UserActivitySummary);

            string template = (rptLeaderboard.Items.Count > 0) ? _templates["Footer"] : _templates["FooterEmpty"];
            {
                return _razor.Evaluate(item, template);
            }
        }

        #endregion
    }
}
