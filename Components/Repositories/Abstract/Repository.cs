namespace DNNGamification.Components.Repositories
{
    using DotNetNuke.Data;

    using System;

    /// <summary>
    /// Repository base class.
    /// </summary>
    public abstract class RepositoryBase : IDisposable
    {
        #region Protected Properties

        /// <summary>
        /// Gets or sets data provider.
        /// </summary>
        protected DataProvider DataProvider { get; set; }

        #endregion

        #region Public Methods : IDisposable

        /// <summary>
        /// Disposes current instance.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        public RepositoryBase(DataProvider provider)
        {
            DataProvider = provider;
        }

        #endregion
    }
}