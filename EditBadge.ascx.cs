namespace DNNGamification
{
    using DNNGamification.Components.Entities;

    using DotNetNuke.Common;
    using DotNetNuke.Common.Utilities;

    using DotNetNuke.Services.Exceptions;

    using DotNetNuke.Web.Client;
    using DotNetNuke.Web.Client.ClientResourceManagement;

    using System;

    using System.Collections;
    using System.Collections.Generic;

    using System.Linq;
    using System.Transactions;

    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI;

    using Telerik.Web.UI;

    /// <summary>
    /// Badge activity row.
    /// </summary>
    public class BadgeActivityRow
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets unique key.
        /// </summary>
        public Guid UniqueKey { get; set; }

        /// <summary>
        ///  Gets or sets data item.
        /// </summary>
        public BadgeActivity DataItem { get; set; }

        #endregion

        #region Public Properties : State

        /// <summary>
        ///  Gets or sets state.
        /// </summary>
        public RowState State { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public BadgeActivityRow() { UniqueKey = Guid.NewGuid(); }

        /// <summary>
        /// 
        /// </summary>
        public BadgeActivityRow(BadgeActivity data, RowState state) : this()
        {
            DataItem = data; State = state;
        }

        #endregion
    }

    /// <summary>
    /// Edit badge module control.
    /// </summary>
    public partial class EditBadge : ModuleBase
    {
        #region Defines

        /// <summary>
        /// 
        /// </summary>
        private const string ROWS_OBJECT = "BadgeActivityRows";

        #endregion

        #region Defines : Control

        /// <summary>
        /// 
        /// </summary>
        private const string TITLE_EDT = "Edit \"{0}\" Badge";

        #endregion

        #region Private Fields

        /// <summary>
        /// Rad AJAX manager.
        /// </summary>
        RadAjaxManager _ajaxManager = null;

        #endregion

        #region Private Fields : Temp

        /// <summary>
        /// Gets or sets editing badge.
        /// </summary>
        private Badge _editingBadge = null;

        #endregion

        #region Protected Methods : Event Handlers

        /// <summary>
        /// OnInit handler.
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            try // try to handle OnInit
            {
                LocalResourceFile = TemplateSourceDirectory + "/App_LocalResources/EditBadge.resx";

                ClientResourceManager.RegisterScript(Page, TemplateSourceDirectory + "/Scripts/mechanics.badge.js", FileOrder.Js.DefaultPriority + 1);
                {
                    ClientResourceManager.RegisterStyleSheet(Page, TemplateSourceDirectory + "/Css/mechanics.badge.css", FileOrder.Css.DefaultPriority + 1);
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
                    if (_editingBadge == null)
                    {
                        ((Label)title).Text = LocalizeString("Add.Title"); return;
                    }

                    string badgeName = _editingBadge.Name;

                    string text = String.Format(LocalizeString("Edit.Title"), badgeName);
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
        /// Page_Load hadler.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try // try to handle Page_Load
            {
                _ajaxManager = null;

                if ((_ajaxManager = RadAjaxManager.GetCurrent(Page)) == null)
                {
                    _ajaxManager = new RadAjaxManager() { ID = "amEditBadge", EnableAJAX = true };
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
                var apActivitiesSetting = new AjaxSetting("apActivities");
                {
                    apActivitiesSetting.UpdatedControls.Add(new AjaxUpdatedControl("apActivities", "alpMain"));
                    {
                        _ajaxManager.AjaxSettings.Add(apActivitiesSetting);
                    }
                }
                var apSelectorSetting = new AjaxSetting("apSelector");
                {
                    apSelectorSetting.UpdatedControls.Add(new AjaxUpdatedControl("apSelector", "alpMain"));
                    {
                        _ajaxManager.AjaxSettings.Add(apSelectorSetting);
                    }
                }

                if (!IsPostBack)
                {
                    int badgeId = -1; Badge badge = null;

                    cbUnit.Items.Insert(0, new RadComboBoxItem("Not expire", "-1"));
                    {
                        Session[ROWS_OBJECT] = new List<BadgeActivityRow>();
                    }

                    var activities = UnitOfWork.Activities.GetAll();
                    {
                        cbActivities.DataSource = activities.OrderBy(d => d.DisplayName);
                        {
                            cbActivities.DataBind();
                        }
                    }

                    if (Int32.TryParse(Request.QueryString["id"], out badgeId))
                    {
                        hfId.Value = badgeId.ToString();

                        if ((_editingBadge = (badge = UnitOfWork.Badges.GetBy(badgeId))) == null)
                        {
                            throw new NullReferenceException(String.Format("Badge {0} is not found", badgeId));
                        }

                        tbName.Text = badge.Name; tbDescription.Text = badge.Description;
                        {
                            fsBadgeImage.FileID = badge.ImageFileId; // define badge image file
                        }

                        foreach (var activity in UnitOfWork.BadgeActivities.GetManyBy(badgeId))
                        {
                            (Session[ROWS_OBJECT] as List<BadgeActivityRow>).Add(new BadgeActivityRow(activity, RowState.Existing));
                        }

                        if (badge.ExpirationQuantity.HasValue && badge.ExpirationUnit.HasValue)
                        {
                            tbQuantity.Text = badge.ExpirationQuantity.ToString();
                            {
                                int unit = badge.ExpirationUnit.Value; cbUnit.SelectedValue = unit.ToString();
                            }
                        }

                        rptBadgeActivities.DataBind();
                    }

                    string returnUrl = Request.QueryString["returnUrl"] ?? "/";
                    {
                        btnCancel.NavigateUrl = returnUrl; // bind cancel button return url
                    }
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// csvImage_ServerValidate handler.
        /// </summary>
        protected void csvImage_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (fsBadgeImage.FileID != Null.NullInteger);
        }

        /// <summary>
        /// csvQuantity_ServerValidate handler.
        /// </summary>
        protected void csvQuantity_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = cbUnit.SelectedValue == "-1" || (cbUnit.SelectedValue != "-1" && args.Value != "");
        }

        /// <summary>
        /// rptBadgeActivities_OnItemCommand handler.
        /// </summary>
        protected void rptBadgeActivities_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            try // try to handle rptBadgeActivities_OnItemCommand
            {
                List<BadgeActivityRow> source = null;

                if ((source = (Session[ROWS_OBJECT] as List<BadgeActivityRow>)) == null)
                {
                    throw new Exception();
                }

                BadgeActivityRow item = null;

                if ((item = source.FirstOrDefault(r => r.UniqueKey == Guid.Parse(e.CommandArgument.ToString()))) != null)
                {
                    item.State = RowState.Deleted; // mark as deleted
                }

                rptBadgeActivities.DataBind();
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// btnAddActivity_OnClick handler.
        /// </summary>
        protected void btnAddActivity_OnClick(object sender, EventArgs e)
        {
            try // try to handle btnAddActivity_OnClick
            {
                if (!Page.IsValid) return;

                int id = -1; int activityPoints = 0;

                if (!Int32.TryParse(cbActivities.SelectedValue, out id))
                {
                    throw new Exception(String.Format("{0} value can't be parsed", "cbActivities"));
                }
                if (!Int32.TryParse(tbPoints.Text, out activityPoints))
                {
                    throw new Exception(String.Format("{0} value can't be parsed", "tbPoints"));
                }

                var rows = (Session[ROWS_OBJECT] as List<BadgeActivityRow>);
                {
                    if (rows.Any(r => r.DataItem.ActivityId == id && r.State != RowState.Deleted))
                    {
                        return; // return if reference is already added
                    }
                }

                Activity activity = null;

                if ((activity = UnitOfWork.Activities.GetBy(id)) == null)
                {
                    throw new NullReferenceException(String.Format("Activity with ID: {0} is not found", id));
                }

                var item = new BadgeActivity(-1, activityPoints)
                {
                    ActivityId        = activity.ActivityId,        // define activity ID
                    DesktopModuleId   = activity.DesktopModuleId,   // define desktop module ID
                    DesktopModuleName = activity.DesktopModuleName, // define desktop module name
                    Name              = activity.Name               // define name
                };

                rows.Add(new BadgeActivityRow(item, RowState.New));
                {
                    rptBadgeActivities.DataBind();
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

                using (var transaction = new TransactionScope())
                {
                    int? unit = null, quantity = null;

                    int imageFileId = fsBadgeImage.FileID, portalId = PortalId;

                    int _quantity = -1, _unit = -1;

                    bool expirable = false;

                    if (Int32.TryParse(tbQuantity.Text, out _quantity) && Int32.TryParse(cbUnit.SelectedValue, out _unit))
                    {
                        quantity = _quantity; unit = _unit == -1 ? null : (int?)_unit;
                        {
                            expirable = true; // set badge is expirable if expiration period is defined
                        }
                    }

                    int badgeId = -1; if (Int32.TryParse(hfId.Value, out badgeId) && badgeId != -1)
                    {
                        UnitOfWork.Badges.Update // update existing badge with values
                        (
                            badgeId, imageFileId, portalId, tbName.Text, tbDescription.Text, unit, quantity, expirable
                        );
                    }
                    else
                    {
                        badgeId = UnitOfWork.Badges.Add // add new badge
                        (
                            imageFileId, portalId, tbName.Text, tbDescription.Text, unit, quantity, expirable
                        );
                    }

                    var rows = (Session[ROWS_OBJECT] as List<BadgeActivityRow>);

                    foreach (var item in rows.Where(r => r.State == RowState.New).Select(r => r.DataItem))
                    {
                        UnitOfWork.BadgeActivities.Add(portalId, item.ActivityId, badgeId, item.ActivityPoints);
                    }

                    foreach (var item in rows.Where(r => r.State == RowState.Deleted).Select(r => r.DataItem))
                    {
                        int id = item.BadgeActivityId; UnitOfWork.BadgeActivities.Delete(id);
                    }

                    transaction.Complete();
                }

                string returnUrl = Request.QueryString["returnUrl"] ?? "/";
                {
                    Response.Redirect(returnUrl);
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        #endregion

        #region Data Methods

        /// <summary>
        /// odsBadgeActivities_Select method.
        /// </summary>
        public List<BadgeActivityRow> odsBadgeActivities_Select()
        {
            var result = new List<BadgeActivityRow>();

            try // try to handle odsBadgeActivities_Select
            {
                if (HttpContext.Current.Session[ROWS_OBJECT] != null)
                {
                    var rows = (HttpContext.Current.Session[ROWS_OBJECT] as List<BadgeActivityRow>);

                    IEnumerable<BadgeActivityRow> actual = rows.Where(r => r.State != RowState.Deleted); // get actual
                    {
                        result = actual.OrderBy(r => r.DataItem.DisplayName).ToList();
                    }
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

            return result;
        }

        #endregion
    }
}