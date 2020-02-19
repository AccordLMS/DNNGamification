namespace DNNGamification
{
    using DNNGamification.Components;
    using DNNGamification.Components.Entities;

    using DotNetNuke.UI;
    using DotNetNuke.UI.Modules;

    using DotNetNuke.Web;
    using DotNetNuke.Web.Razor;

    using System.IO;

    /// <summary>
    /// Profile web page.
    /// </summary>
    public abstract class ProfileWebPage : DotNetNukeWebPage<UserBadge>
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets activity points.
        /// </summary>
        public decimal ActivityPoints { get; set; }

        #endregion
    }

    /// <summary>
    /// Leaderboard web page.
    /// </summary>
    public abstract class LeaderboardWebPage : DotNetNukeWebPage<ScoringLeaderboard>
    {
        #region Public Properties

        // reserved for something awesome

        #endregion
    }

    /// <summary>
    /// Leaderboard web page.
    /// </summary>
    public abstract class ActivitySummaryWebPage : DotNetNukeWebPage<UserActivitySummary>
    {
        #region Public Properties

        // reserved for something awesome

        #endregion
    }

    /// <summary>
    /// Razor base template evaluator.
    /// </summary>
    public abstract class RazorEvaluator<TEntity> where TEntity : EntityBase
    {
        #region Protected Proeprties

        /// <summary>
        /// 
        /// </summary>
        protected string LocalResourceFile { get; set; }

        #endregion

        #region Protected Proeprties : Context

        /// <summary>
        /// 
        /// </summary>
        protected ModuleInstanceContext ModuleContext { get; set; }

        #endregion

        #region Public Methods : Evaluator

        /// <summary>
        /// Evaluates token replacement.
        /// </summary>
        public string Evaluate(TEntity data, string template)
        {
            var razor = new RazorEngine(template, ModuleContext, LocalResourceFile);
            {
                var w = new StringWriter(); razor.Render<TEntity>(w, data);
                {
                    return w.ToString();
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        public RazorEvaluator(ModuleInstanceContext context, string resource)
        {
            ModuleContext = context; LocalResourceFile = resource;
        }

        #endregion
    }

    /// <summary>
    /// Leaderboard template evaluator.
    /// </summary>
    public class LeaderboardEvaluator : RazorEvaluator<ScoringLeaderboard>
    {
        #region Constructors

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        public LeaderboardEvaluator(ModuleInstanceContext context, string resource)

            : base(context, resource) { }

        #endregion
    }

    /// <summary>
    /// Leaderboard template evaluator.
    /// </summary>
    public class ActivitySummaryEvaluator : RazorEvaluator<UserActivitySummary>
    {
        #region Constructors

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        public ActivitySummaryEvaluator(ModuleInstanceContext context, string resource)

            : base(context, resource) { }

        #endregion
    }

    /// <summary>
    /// Profile template evaluator.
    /// </summary>
    public class ProfileEvaluator : RazorEvaluator<UserBadge>
    {
        #region Private Fields

        #endregion

        #region Public Methods : Evaluator

        /// <summary>
        /// Evaluates token replacement.
        /// </summary>
        public string Evaluate(UserBadge data, decimal points, string template)
        {
            var razor = new RazorEngine(template, ModuleContext, LocalResourceFile);
            {
                var page = (razor.Webpage as ProfileWebPage); page.ActivityPoints = points;

                var w = new StringWriter(); razor.Render<UserBadge>(w, data);
                {
                    return w.ToString();
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        public ProfileEvaluator(ModuleInstanceContext context, string resource)

            : base(context, resource) { }

        #endregion
    }
}
