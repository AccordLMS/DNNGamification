namespace DNNGamification
{
    using DNNGamification.Components.Repositories;

    using DotNetNuke.Entities;
    using DotNetNuke.Entities.Modules;

    using DotNetNuke.Services.Exceptions;

    using DotNetNuke.Framework;
    using DotNetNuke.Framework.JavaScriptLibraries;

    using DotNetNuke.Web.Client;
    using DotNetNuke.Web.Client.ClientResourceManagement;

    using System.Web.UI;
    using System.Web.UI.WebControls;

    using System;

    /// <summary>
    /// Leaderboard module base.
    /// </summary>
    public class ModuleBase : PortalModuleBase
    {
        #region Private Fields

        /// <summary>
        /// Unit of work instance.
        /// </summary>
        private UnitOfWork _uow = new UnitOfWork();

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets js files priority (order).
        /// </summary>
        protected int JsPriority
        {
            get { return (int)FileOrder.Js.DefaultPriority + 1; }
        }

        /// <summary>
        /// Gets css files priority (order).
        /// </summary>
        protected int TemplateJsPriority
        {
            get { return (int)FileOrder.Js.DefaultPriority + 2; }
        }

        /// <summary>
        /// Gets css files priority (order) for templates.
        /// </summary>
        protected int CssPriority
        {
            get { return (int)FileOrder.Css.DefaultPriority + 1; }
        }

        /// <summary>
        /// Gets css files priority (order) for templates.
        /// </summary>
        protected int TemplateCssPriority
        {
            get { return (int)FileOrder.Css.DefaultPriority + 2; }
        }

        #endregion

        #region Protected Properties : Data Access

        /// <summary>
        /// Gets unit of work.
        /// </summary>
        protected UnitOfWork UnitOfWork
        {
            get { return _uow; }
        }

        #endregion

        #region Protected Methods : Protected Event Handlers

        /// <summary>
        /// OnInit handler.
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            try // try to handle OnInit
            {
                WebControl.DisabledCssClass = "aspNetDisabled disabled";

                ClientResourceManager.RegisterScript(Page, TemplateSourceDirectory + "/Scripts/auxiliary.js");
                {
                    base.OnInit(e);
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// OnLoad handler.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            try // try to handle OnLoad
            {
                JavaScript.RequestRegistration("jQuery");
                JavaScript.RequestRegistration("jQuery-UI");

                ServicesFramework.Instance.RequestAjaxAntiForgerySupport();
                {
                    base.OnLoad(e);
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        #endregion
    }
}