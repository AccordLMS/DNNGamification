namespace DNNGamification.Components.Entities
{
    using DNNGamification.Components.Repositories;

    using DotNetNuke.Common;
    using DotNetNuke.Common.Utilities;

    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Entities.Users;

    using DotNetNuke.Services.Tokens;

    using System;

    using System.Runtime;
    using System.Runtime.Serialization;

    using System.Collections;
    using System.Collections.Generic;

    using System.Data;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Scoring leaderboard entity.
    /// </summary>
    [DataContract, Serializable]
    public class UserActivitySummary : EntityBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets row.
        /// </summary>
        [DataMember]
        public int Row { get; set; }

        /// <summary>
        /// Gets or sets points.
        /// </summary>
        [DataMember]
        public int Points { get; set; }

        /// <summary>
        /// Gets or sets points.
        /// </summary>
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets rank.
        /// </summary>
        [DataMember]
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets key ID.
        /// </summary>
        [IgnoreDataMember]
        public override int KeyID
        {
            get { return UserId; } set { UserId = value; }
        }

        #endregion

        #region Public Methods : IHydratable

        /// <summary>
        /// Provides hydration.
        /// </summary>
        public override void Fill(IDataReader r)
        {
            Rank = Utils.ConvertTo<int>(r["Rank"]);
            Row = Utils.ConvertTo<int>(r["Row"]);
            Points           = Utils.ConvertTo<int>(r["Points"]);         
            UserId         = Utils.ConvertTo<int>(r["UserId"]);
            Name      = Utils.ConvertTo<string>(r["Name"]);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UserActivitySummary() { }

        #endregion
    }
}
