namespace DNNGamification.Components.Services
{
    using DNNGamification.Components;
    using DNNGamification.Components.Repositories;

    using DotNetNuke.Web.Api;

    using System.Net;
    using System.Net.Http.Formatting;
    using System.Net.Http;

    /// <summary>
    /// Web controller.
    /// </summary>
    public class WebController : DnnApiController
    {
        #region Private Fields

        /// <summary>
        /// Unit of work instance.
        /// </summary>
        private UnitOfWork _uow = new UnitOfWork();

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets unit of work.
        /// </summary>
        protected UnitOfWork UnitOfWork
        {
            get { return _uow; }
        }

        #endregion

        #region Private Methods : Helpers

        /// <summary>
        /// 
        /// </summary>
        private HttpResponseMessage ResponseOK(object data)
        {
            JsonMediaTypeFormatter formatter = Configuration.Formatters.JsonFormatter;
            {
                return Request.CreateResponse(HttpStatusCode.OK, data, formatter); // return response
            }
        }

        #endregion

        #region Public Methods

        

        #endregion
    }
}