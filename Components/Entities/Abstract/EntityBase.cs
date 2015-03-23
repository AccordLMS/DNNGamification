namespace DNNGamification.Components.Entities
{
    using DotNetNuke.Entities.Modules;

    using System;
    using System.Data;

    using System.Runtime;
    using System.Runtime.Serialization;

    using System.Xml.Serialization;

    /// <summary>
    /// Base entity class.
    /// </summary>
    [DataContract, Serializable]
    public abstract class EntityBase : IHydratable
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets key ID.
        /// </summary>
        [IgnoreDataMember, XmlIgnore]
        public abstract int KeyID { get; set; }

        #endregion

        #region Public Methods : Abstract

        /// <summary>
        /// Provides hydration.
        /// </summary>
        public abstract void Fill(IDataReader r);

        #endregion
    }
}
