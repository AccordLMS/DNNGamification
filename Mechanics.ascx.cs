using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Skins;
using DotNetNuke.UI.Skins.Controls;

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

                var apAssignmentas = new AjaxSetting("apAssignment");
                {
                    apAssignmentas.UpdatedControls.Add(new AjaxUpdatedControl("apAssignment", "alpMechanics"));
                    {
                        _ajaxManager.AjaxSettings.Add(apAssignmentas);
                    }
                }

                var grActivitiesas = new AjaxSetting("grActivities");
                {
                    grActivitiesas.UpdatedControls.Add(new AjaxUpdatedControl("grActivities", "alpMechanics"));
                    {
                        _ajaxManager.AjaxSettings.Add(grActivitiesas);
                    }
                }
                var grBadgesas = new AjaxSetting("grBadges");
                {
                    grBadgesas.UpdatedControls.Add(new AjaxUpdatedControl("grBadges", "alpMechanics"));
                    {
                        _ajaxManager.AjaxSettings.Add(grBadgesas);
                    }
                }
                var grAssignmentas = new AjaxSetting("grAssignment");
                {
                    grAssignmentas.UpdatedControls.Add(new AjaxUpdatedControl("grAssignment", "alpMechanics"));
                    {
                        _ajaxManager.AjaxSettings.Add(grAssignmentas);
                    }
                }

                if (!IsPostBack)
                {
                    LoadgrActivities();
                    loadgrBadges();
                    loadgrdAssignment();
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
        protected void LoadgrActivities()
        {
            try // try to handle grdActivities_OnNeedDataSource
            {
                string orderBy = "Name"; string orderByDirection = "ASC";

                //if (grdActivities.MasterTableView != null && grdActivities.MasterTableView.SortExpressions.Count > 0)
                //{
                //    GridSortExpression expression = grdActivities.MasterTableView.SortExpressions[0];

                //    orderBy = expression.FieldName; // define order by options
                //    {
                //        orderByDirection = expression.SortOrder == GridSortOrder.Descending ? "DESC" : "ASC"; // define sorting
                //    }
                //}

                int totalCount = -1, start = grActivities.PageIndex * grActivities.PageSize;

                grActivities.DataSource = UnitOfWork.Activities.GetView
                (
                    start, grActivities.PageSize, orderBy, orderByDirection, out totalCount // get paged view
                );

                grActivities.VirtualItemCount = totalCount; // bind total count
                grActivities.DataBind();
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// grdActivities_OnItemDataBound handler.
        /// </summary>
        protected void grActivities_OnDataBound(object sender, GridViewRowEventArgs e)
        {
            try // try to handle grdActivities_OnItemDataBound
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HyperLink hlEditDefinition = (e.Row.FindControl("hlEditDefinition") as HyperLink);

                    var definition = (e.Row.DataItem as Activity); string id = definition.KeyID.ToString();

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
        /// grBadges_OnNeedDataSource handler.
        /// </summary>
        protected void loadgrBadges()
        {
            try // try to handle grBadges_OnNeedDataSource
            {
                int portalId = PortalId; // define portal ID to get badges

                string orderBy = "Name", orderByDirection = "ASC";

                //if (grBadges.MasterTableView != null && grBadges.MasterTableView.SortExpressions.Count > 0)
                //{
                //    GridSortExpression expression = grBadges.MasterTableView.SortExpressions[0];

                //    orderBy = expression.FieldName; // define order by options
                //    {
                //        orderByDirection = expression.SortOrder == GridSortOrder.Descending ? "DESC" : "ASC";
                //    }
                //}

                int totalCount = -1, start = grBadges.PageIndex * grBadges.PageSize;

                grBadges.DataSource = UnitOfWork.Badges.GetView
                (
                    portalId, start, grBadges.PageSize, orderBy, orderByDirection, out totalCount // get paged view
                );

                grBadges.VirtualItemCount = totalCount; // bind total count
                grBadges.DataBind();
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// grBadges_OnItemDataBound handler.
        /// </summary>
        protected void grBadges_OnItemDataBound(object sender, GridViewRowEventArgs e)
        {
            try // try to handle grBadges_OnItemDataBound
            {
                if (e.Row.RowType != DataControlRowType.DataRow) return;

                HyperLink hlEditBadge = (e.Row.FindControl("hlEditBadge") as HyperLink);

                var badge = (e.Row.DataItem as Badge); string id = badge.KeyID.ToString();

                string returnUrl = HttpContext.Current.Request.RawUrl;
                {
                    hlEditBadge.NavigateUrl = GetControlUrl("EditBadge", "id=" + id, "mid=" + ModuleId, "returnUrl=" + returnUrl);
                }

                string confirm = LocalizeString("Delete.Confirm"); // define text

                LinkButton hlDeleteBadge = (e.Row.FindControl("hlDeleteBadge") as LinkButton);
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
        /// grBadges_OnItemCommand handler.
        /// </summary>
        protected void grBadges_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try // try to handle grBadges_OnItemCommand
            {
                int badgeId = -1;

                if (Int32.TryParse(e.CommandArgument.ToString(), out badgeId))
                {
                    UnitOfWork.Badges.Delete(badgeId); grBadges.DataBind(); // rebind grid
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
        protected void loadgrdAssignment()
        {
            try // try to handle grdAssignment_OnNeedDataSource
            {
                int portalId = PortalId; // define portal ID to get users

                string orderBy = "UserName"; string orderByDirection = "ASC";

                //if (grdAssignment.MasterTableView != null && grdAssignment.MasterTableView.SortExpressions.Count > 0)
                //{
                //    GridSortExpression expression = grdAssignment.MasterTableView.SortExpressions[0];

                //    orderBy = expression.FieldName; // define order by options
                //    {
                //        orderByDirection = expression.SortOrder == GridSortOrder.Descending ? "DESC" : "ASC";
                //    }
                //}

                int totalCount = -1, start = grAssignment.PageIndex * grAssignment.PageSize;

                grAssignment.DataSource = UnitOfWork.UserActivitiesLog.GetUsers
                (
                    portalId, txtUserSearch.Text + "%", start, grAssignment.PageSize, orderBy, orderByDirection, out totalCount
                );

                grAssignment.VirtualItemCount = totalCount; // bind total count
                grAssignment.DataBind();
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// grdAssignment_OnItemDataBound handler.
        /// </summary>
        protected void grdAssignment_OnItemDataBound(object sender, GridViewRowEventArgs e)
        {
            try // try to handle grdAssignment_OnItemDataBound
            {
                if (e.Row.GetType() == typeof(GridDataItem))
                {
                    HyperLink hlEditUser = (e.Row.FindControl("hlEditUser") as HyperLink);

                    var user = (e.Row.DataItem as ScoringUser); string id = user.KeyID.ToString();

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
        /// btnSearch_Click handler.g
        /// </summary>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grAssignment.Visible = true; grAssignment.Visible = true;
            {
                grAssignment.DataBind(); grAssignment.DataBind(); // rebind with term
            }
        }

        #endregion

        #region Public Methods

        #endregion
    }
}