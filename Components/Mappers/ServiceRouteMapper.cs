namespace DNNGamification.Components.Mappers
{
    using DotNetNuke.Web.Api;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using System.Web.Http;

    /// <summary>
    /// 
    /// </summary>
    public class ServiceRouteMapper : IServiceRouteMapper
    {
        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        public void RegisterRoutes(IMapRoute routeManager)
        {
            routeManager.MapHttpRoute("dnngmf", "default", "{controller}/{action}", new string[1]
            {
                "DNNGamification.Components.Services"
            });
        }

        #endregion
    }
}
