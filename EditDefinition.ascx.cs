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
    /// Edit definition module control.
    /// </summary>
    public partial class EditDefinition : ModuleBase
    {
        #region Private Fields

        /// <summary>
        /// Rad AJAX manager.
        /// </summary>
        RadAjaxManager _ajaxManager = null;

        #endregion

        #region Private Fields : Temp

        /// <summary>
        /// Gets or sets editing activity.
        /// </summary>
        private Activity _editingActivity = null;

        #endregion

        #region Protected Methods : Event Handlers

        /// <summary>
        /// OnInit handler.
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            try // try to handle OnInit
            {
                LocalResourceFile = TemplateSourceDirectory + "/App_LocalResources/EditDefinition.resx";

                ClientResourceManager.RegisterScript(Page, TemplateSourceDirectory + "/Scripts/mechanics.definition.js", FileOrder.Js.DefaultPriority + 1);
                {
                    ClientResourceManager.RegisterStyleSheet(Page, TemplateSourceDirectory + "/Css/mechanics.definition.css", FileOrder.Css.DefaultPriority + 1);
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
                    if (_editingActivity == null)
                    {
                        ((Label)title).Text = LocalizeString("Add.Title"); return;
                    }

                    string activityName = _editingActivity.Name;

                    string text = String.Format(LocalizeString("Edit.Title"), activityName);
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
                    _ajaxManager = new RadAjaxManager() { ID = "amEditDefinition", EnableAJAX = true };
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

                if (!IsPostBack)
                {
                    int activityId = 0;

                    if (Int32.TryParse(Request.QueryString["id"], out activityId))
                    {
                        Activity activity = null;

                        if ((_editingActivity = (activity = UnitOfWork.Activities.GetBy(activityId))) == null)
                        {
                            throw new Exception(String.Format("Activity {0} - is not found", activityId));
                        }

                        hfId.Value = activityId.ToString(); tbDescription.Text = activity.Description;

                        tbActivityPoints.Text = activity.ActivityPoints.ToString();
                        {
                            chbOnce.Checked = activity.Once;
                        }
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
        /// btnPrimary_OnClick handler.
        /// </summary>
        protected void btnPrimary_OnClick(object sender, EventArgs e)
        {
            try // try to handle btnPrimary_OnClick
            {
                int activityId = 0; if (Int32.TryParse(hfId.Value, out activityId))
                {
                    Activity definition = null;

                    if ((definition = UnitOfWork.Activities.GetBy(activityId)) == null)
                    {
                        throw new Exception("Activity is not found");
                    }

                    decimal activityPoints = 0;

                    if (Decimal.TryParse(tbActivityPoints.Text, out activityPoints))
                    {
                        UnitOfWork.Activities.Update(activityId, null, null, tbDescription.Text, null, activityPoints, chbOnce.Checked);
                    }

                    string returnUrl = Request.QueryString["returnUrl"] ?? "/";
                    {
                        Response.Redirect(returnUrl);
                    }
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