using System;
using System.Text;
using System.Xml;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;

namespace DNNGamification.Components.Controllers
{
    public class ActivitySummaryController : ControllerBase
    {
        protected override string RootElement
        {
            get { return "DNNGamificationActivitySummary"; }
        }        
    }
}
