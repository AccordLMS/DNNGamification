namespace DNNGamification
{
    using DNNGamification.Components;
    using DNNGamification.Components.Repositories;
    using DNNGamification.Components.Entities;
    using DNNGamification.Components.Controllers;

    using DotNetNuke.Framework.JavaScriptLibraries;

    using DotNetNuke.Services.Exceptions;

    using DotNetNuke.Web.Client;
    using DotNetNuke.Web.Client.ClientResourceManagement;

    using DotNetNuke.UI.Utilities;

    using System;

    using System.Web;
    using System.Web.UI.WebControls;

    using Telerik.Web.UI;

    /// <summary>
    /// Module view control.
    /// </summary>
    public partial class Mechanics : ModuleBase
    {
        #region Private Fields

        /// <summary>
        /// Rad AJAX manager.
        /// </summary>
        RadAjaxManager _ajaxManager = null;

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets url of module control.
        /// </summary>
        public string GetControlUrl(string controlKey, params string[] parameters)
        {
            string url = DotNetNuke.Common.Globals.NavigateURL(controlKey, parameters);
            {
                return url; // return url of module control
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
                LocalResourceFile = TemplateSourceDirectory + "/App_LocalResources/Mechanics.resx";

                ClientResourceManager.RegisterStyleSheet(Page, TemplateSourceDirectory + "/Css/mechanics.css", FileOrder.Css.DefaultPriority + 1);

                JavaScript.RequestRegistration(CommonJs.jQuery);
                JavaScript.RequestRegistration(CommonJs.jQueryMigrate);
                JavaScript.RequestRegistration(CommonJs.jQueryUI);
                {
                    ClientResourceManager.RegisterScript(Page, TemplateSourceDirectory + "/Scripts/mechanics.js", FileOrder.Js.DefaultPriority + 1);
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
                    _ajaxManager = new RadAjaxManager() { ID = "amMechanics", EnableAJAX = true };
                    {
                        plhAjaxManager.Controls.Add(_ajaxManager);
                    }
                }

                var apAssignment = new AjaxSetting("apAssignment");
                {
                    apAssignment.UpdatedControls.Add(new AjaxUpdatedControl("apAssignment", "alpMechanics"));
                    {
                        _ajaxManager.AjaxSettings.Add(apAssignment);
                    }
                }

                var grdActivities = new AjaxSetting("grdActivities");
                {
                    grdActivities.UpdatedControls.Add(new AjaxUpdatedControl("grdActivities", "alpMechanics"));
                    {
                        _ajaxManager.AjaxSettings.Add(grdActivities);
                    }
                }
                var grdBadges = new AjaxSetting("grdBadges");
                {
                    grdBadges.UpdatedControls.Add(new AjaxUpdatedControl("grdBadges", "alpMechanics"));
                    {
                        _ajaxManager.AjaxSettings.Add(grdBadges);
                    }
                }
                var grdAssignment = new AjaxSetting("grdAssignment");
                {
                    grdAssignment.UpdatedControls.Add(new AjaxUpdatedControl("grdAssignment", "alpMechanics"));
                    {
                        _ajaxManager.AjaxSettings.Add(grdAssignment);
                    }
                }

                if (!IsPostBack)
                {
                    string returnUrl = HttpContext.Current.Request.RawUrl;
                    {
                        hlAddBadge.NavigateUrl = GetControlUrl("EditBadge", "mid=" + ModuleId, "returnUrl=" + returnUrl);
                    }
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// grdActivities_OnNeedDataSource handler.
        /// </summary>
        protected void grdActivities_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try // try to handle grdActivities_OnNeedDataSource
            {
                string orderBy = "Name"; string orderByDirection = "ASC";

                if (grdActivities.MasterTableView != null && grdActivities.MasterTableView.SortExpressions.Count > 0)
                {
                    GridSortExpression expression = grdActivities.MasterTableView.SortExpressions[0];

                    orderBy = expression.FieldName; // define order by options
                    {
                        orderByDirection = expression.SortOrder == GridSortOrder.Descending ? "DESC" : "ASC"; // define sorting
                    }
                }

                int totalCount = -1, start = grdActivities.CurrentPageIndex * grdActivities.PageSize;

                grdActivities.DataSource = UnitOfWork.Activities.GetView
                (
                    start, grdActivities.PageSize, orderBy, orderByDirection, out totalCount // get paged view
                );

                grdActivities.VirtualItemCount = totalCount; // bind total count
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// grdActivities_OnItemDataBound handler.
        /// </summary>
        protected void grdActivities_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try // try to handle grdActivities_OnItemDataBound
            {
                if (e.Item.GetType() == typeof(GridDataItem))
                {
                    HyperLink hlEditDefinition = (e.Item.FindControl("hlEditDefinition") as HyperLink);

                    var definition = (e.Item.DataItem as Activity); string id = definition.KeyID.ToString();

                    string returnUrl = HttpContext.Current.Request.RawUrl;
                    {
                        hlEditDefinition.NavigateUrl = GetControlUrl("EditDefinition", "id=" + id, "mid=" + ModuleId, "returnUrl=" + returnUrl);
                    }
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// grdBadges_OnNeedDataSource handler.
        /// </summary>
        protected void grdBadges_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try // try to handle grdBadges_OnNeedDataSource
            {
                int portalId = PortalId; // define portal ID to get badges

                string orderBy = "Name", orderByDirection = "ASC";

                if (grdBadges.MasterTableView != null && grdBadges.MasterTableView.SortExpressions.Count > 0)
                {
                    GridSortExpression expression = grdBadges.MasterTableView.SortExpressions[0];

                    orderBy = expression.FieldName; // define order by options
                    {
                        orderByDirection = expression.SortOrder == GridSortOrder.Descending ? "DESC" : "ASC";
                    }
                }

                int totalCount = -1, start = grdBadges.CurrentPageIndex * grdBadges.PageSize;

                grdBadges.DataSource = UnitOfWork.Badges.GetView
                (
                    portalId, start, grdBadges.PageSize, orderBy, orderByDirection, out totalCount // get paged view
                );

                grdBadges.VirtualItemCount = totalCount; // bind total count
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// grdBadges_OnItemDataBound handler.
        /// </summary>
        protected void grdBadges_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try // try to handle grdBadges_OnItemDataBound
            {
                if (e.Item.GetType() != typeof(GridDataItem)) return;

                HyperLink hlEditBadge = (e.Item.FindControl("hlEditBadge") as HyperLink);

                var badge = (e.Item.DataItem as Badge); string id = badge.KeyID.ToString();

                string returnUrl = HttpContext.Current.Request.RawUrl;
                {
                    hlEditBadge.NavigateUrl = GetControlUrl("EditBadge", "id=" + id, "mid=" + ModuleId, "returnUrl=" + returnUrl);
                }

                string confirm = LocalizeString("Delete.Confirm"); // define text

                LinkButton hlDeleteBadge = (e.Item.FindControl("hlDeleteBadge") as LinkButton);
                {
                    ClientAPI.AddButtonConfirm(hlDeleteBadge, confirm);
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// grdBadges_OnItemCommand handler.
        /// </summary>
        protected void grdBadges_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            try // try to handle grdBadges_OnItemCommand
            {
                int badgeId = -1;

                if (Int32.TryParse(e.CommandArgument.ToString(), out badgeId))
                {
                    UnitOfWork.Badges.Delete(badgeId); grdBadges.MasterTableView.Rebind(); // rebind grid
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// grdAssignment_OnNeedDataSource handler.
        /// </summary>
        protected void grdAssignment_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try // try to handle grdAssignment_OnNeedDataSource
            {
                int portalId = PortalId; // define portal ID to get users

                string orderBy = "UserName"; string orderByDirection = "ASC";

                if (grdAssignment.MasterTableView != null && grdAssignment.MasterTableView.SortExpressions.Count > 0)
                {
                    GridSortExpression expression = grdAssignment.MasterTableView.SortExpressions[0];

                    orderBy = expression.FieldName; // define order by options
                    {
                        orderByDirection = expression.SortOrder == GridSortOrder.Descending ? "DESC" : "ASC";
                    }
                }

                int totalCount = -1, start = grdAssignment.CurrentPageIndex * grdAssignment.PageSize;

                grdAssignment.DataSource = UnitOfWork.UserActivitiesLog.GetUsers
                (
                    portalId, txtUserSearch.Text + "%", start, grdAssignment.PageSize, orderBy, orderByDirection, out totalCount
                );

                grdAssignment.VirtualItemCount = totalCount; // bind total count
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// grdAssignment_OnItemDataBound handler.
        /// </summary>
        protected void grdAssignment_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try // try to handle grdAssignment_OnItemDataBound
            {
                if (e.Item.GetType() == typeof(GridDataItem))
                {
                    HyperLink hlEditUser = (e.Item.FindControl("hlEditUser") as HyperLink);

                    var user = (e.Item.DataItem as ScoringUser); string id = user.KeyID.ToString();

                    string returnUrl = HttpContext.Current.Request.RawUrl;
                    {
                        hlEditUser.NavigateUrl = GetControlUrl("EditUser", "id=" + id, "mid=" + ModuleId, "returnUrl=" + returnUrl);
                    }
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// btnSearch_Click handler.
        /// </summary>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grdAssignment.MasterTableView.Visible = true; grdAssignment.Visible = true;
            {
                grdAssignment.Rebind(); grdAssignment.MasterTableView.Rebind(); // rebind with term
            }
        }

        #endregion

        #region Public Methods

        #endregion
    }
}