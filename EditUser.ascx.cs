namespace DNNGamification
{
    using DNNGamification.Components;
    using DNNGamification.Components.Entities;

    using DotNetNuke.Common;

    using DotNetNuke.Entities.Users;

    using DotNetNuke.Web.Client;
    using DotNetNuke.Web.Client.ClientResourceManagement;

    using DotNetNuke.Services.Exceptions;

    using System;
    using System.Linq;

    using System.Collections;
    using System.Collections.Generic;

    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Telerik.Web.UI;

    /// <summary>
    /// Edit user module control.
    /// </summary>
    public partial class EditUser : ModuleBase
    {
        #region Private Fields

        /// <summary>
        /// Rad AJAX manager.
        /// </summary>
        RadAjaxManager _ajaxManager = null;

        #endregion

        #region Private Fields : Temp

        /// <summary>
        /// Gets or sets editing user.
        /// </summary>
        private UserInfo _editingUser = null;

        #endregion

        #region Private Methods

        /// <summary>
        /// Rebinds badges.
        /// </summary>
        private void RebindBadges(int userId, int portalId)
        {
            List<UserBadge> userBadges = UnitOfWork.UserBadges.GetManyBy(userId, portalId);
            {
                rptUserBadges.DataSource = userBadges;
                {
                    rptUserBadges.DataBind();
                }
            }

            IEnumerable<int> exclude = userBadges.Select(u => u.BadgeId);

            List<Badge> badges = UnitOfWork.Badges.GetAll(portalId);
            {
                ddlBadges.DataSource = badges.Where(b => !exclude.Contains(b.BadgeId)).OrderBy(d => d.Name);
                {
                    ddlBadges.DataBind();
                }
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
                LocalResourceFile = TemplateSourceDirectory + "/App_LocalResources/EditUser.resx";

                ClientResourceManager.RegisterScript(Page, TemplateSourceDirectory + "/Scripts/mechanics.user.js", FileOrder.Js.DefaultPriority + 1);
                {
                    ClientResourceManager.RegisterStyleSheet(Page, TemplateSourceDirectory + "/Css/mechanics.user.css", FileOrder.Css.DefaultPriority + 1);
                }

                base.OnInit(e);
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// OnPreRender handler.
        /// </summary>
        protected override void OnPreRender(EventArgs e)
        {
            try // try to handle OnPreRender
            {
                base.OnPreRender(e); Control title = null;

                if ((title = Globals.FindControlRecursiveDown(NamingContainer, "titleLabel")) != null)
                {
                    if (_editingUser == null) return;

                    string username = _editingUser.Username;

                    string text = String.Format(LocalizeString("Edit.Title"), username);
                    {
                        ((Label)title).Text = text;
                    }
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
                _ajaxManager = null;

                if ((_ajaxManager = RadAjaxManager.GetCurrent(Page)) == null)
                {
                    _ajaxManager = new RadAjaxManager() { ID = "amEditUser", EnableAJAX = true };
                    {
                        phAjaxManager.Controls.Add(_ajaxManager);
                    }
                }

                var apMainSetting = new AjaxSetting("apMain");
                {
                    apMainSetting.UpdatedControls.Add(new AjaxUpdatedControl("apMain", "alpMain"));
                    {
                        _ajaxManager.AjaxSettings.Add(apMainSetting);
                    }
                }
                var apScoringActionsSetting = new AjaxSetting("apScoringActions");
                {
                    apScoringActionsSetting.UpdatedControls.Add(new AjaxUpdatedControl("apScoringActions", "alpMain"));
                    {
                        _ajaxManager.AjaxSettings.Add(apScoringActionsSetting);
                    }
                }
                var apUserBadgesSetting = new AjaxSetting("apUserBadges");
                {
                    apUserBadgesSetting.UpdatedControls.Add(new AjaxUpdatedControl("apUserBadges", "alpMain"));
                    {
                        _ajaxManager.AjaxSettings.Add(apUserBadgesSetting);
                    }
                }

                if (!IsPostBack)
                {
                    int userId = -1; // define user ID to parse

                    if (Int32.TryParse(Request.QueryString["id"], out userId))
                    {
                        hfId.Value = userId.ToString();

                        if (Int32.TryParse(Request.QueryString["id"], out userId))
                        {
                            _editingUser = UserController.GetUserById(PortalId, userId);
                        }

                        int portalId = PortalId;

                        UserActivity userActivity = UnitOfWork.UserActivities.GetBy(userId, portalId);
                        {
                            int activityPoints = (userActivity != null ? userActivity.ActivityPoints : 0);
                            {
                                tbActivityPoints.Text = activityPoints.ToString();
                            }
                        }

                        RebindBadges(userId, portalId);
                    }

                    string backUrl = Request.QueryString["returnUrl"] ?? "/";
                    {
                        btnCancel.NavigateUrl = backUrl;
                    }
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
        protected void odsUserBadges_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try // try to handle odsUserBadges_Selecting
            {
                int userId = -1; // define user ID to parse into

                if (Int32.TryParse(hfId.Value, out userId))
                {
                    e.InputParameters.Clear(); e.InputParameters["userId"] = userId;
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// rptUserBadges_OnItemCommand handler.
        /// </summary>
        protected void rptUserBadges_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            try // try to handle rptUserBadges_OnItemCommand
            {
                int userId = -1, portalId = PortalId;

                if (!Int32.TryParse(hfId.Value, out userId))
                {
                    throw new Exception(String.Format("{0} value can't be parsed", hfId.Value));
                }

                int userBadgeId = -1; // define IDs to parse into

                if (Int32.TryParse(e.CommandArgument.ToString(), out userBadgeId))
                {
                    UnitOfWork.UserBadges.Delete(userBadgeId);
                    {
                        RebindBadges(userId, portalId); // delete user badge with specified ID
                    }
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// rptUserBadges_ItemDataBound handler.
        /// </summary>
        protected void rptUserBadges_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try // try to handle ddlBadges_OnItemDataBound
            {
                if (e.Item.DataItem == null) return;

                LinkButton hlDeleteBadge = (e.Item.FindControl("hlDeleteBadge") as LinkButton);
                {
                    DotNetNuke.UI.Utilities.ClientAPI.AddButtonConfirm(hlDeleteBadge, LocalizeString("Delete.Confirm"));
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// ddlBadges_OnItemDataBound handler.
        /// </summary>
        protected void ddlBadges_OnItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            try // try to handle ddlBadges_OnItemDataBound
            {
                e.Item.ImageUrl = (e.Item.DataItem as Badge).ImageFileUrl;
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// btnAddUserBadge_OnClick handler.
        /// </summary>
        protected void btnAddUserBadge_OnClick(object sender, EventArgs e)
        {
            try // try to handle btnAddUserBadge_OnClick
            {
                if (!Page.IsValid) return;

                int userId = -1, portalId = PortalId;

                int badgeId = -1; // define badge ID to parase into

                if (!Int32.TryParse(hfId.Value, out userId))
                {
                    throw new Exception(String.Format("{0} value can't be parsed", hfId.Value));
                }
                if (!Int32.TryParse(ddlBadges.SelectedValue, out badgeId))
                {
                    throw new Exception(String.Format("{0} value can't be parsed", ddlBadges.SelectedValue));
                }

                UnitOfWork.UserBadges.Add(badgeId, userId, portalId);
                {
                    RebindBadges(userId, portalId);
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// btnPrimary_OnClick handler.
        /// </summary>
        protected void btnPrimary_OnClick(object sender, EventArgs e)
        {
            try // try to handle btnPrimary_OnClick
            {
                if (!Page.IsValid) return;

                int userId = -1, portalId = PortalId; int points = 0;

                if (!Int32.TryParse(hfId.Value, out userId))
                {
                    throw new Exception(String.Format("{0} - can't be parsed", hfId.Value));
                }
                if (!Int32.TryParse(tbActivityPoints.Text, out points))
                {
                    throw new Exception(String.Format("{0} - can't be parsed", tbActivityPoints.Text));
                }

                UserActivity userActivity = UnitOfWork.UserActivities.GetBy(userId, portalId);
                {
                    if (userActivity != null) // update activity
                    {
                        UnitOfWork.UserActivities.Update(userActivity.UserActivityId, userId, portalId, points);
                    }
                    else // add new user activity if not created before for this user
                    {
                        UnitOfWork.UserActivities.Add(userId, portalId, points);
                    }
                }

                string backUrl = Request.QueryString["returnUrl"] ?? "/";
                {
                    Response.Redirect(backUrl);
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        #endregion

        #region Data Methods

        #endregion
    }
}