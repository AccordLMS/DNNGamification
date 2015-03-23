namespace DNNGamification.Components.Entities
{
    using DotNetNuke.Common;

    using DotNetNuke.Services;
    using DotNetNuke.Services.FileSystem;

    using System;
    using System.Data;

    using System.Runtime;
    using System.Runtime.Serialization;

    /// <summary>
    /// User badge entity.
    /// </summary>
    [DataContract, Serializable]
    public class UserBadge : EntityBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets user badge ID.
        /// </summary>
        [DataMember]
        public int UserBadgeId { get; set; }

        /// <summary>
        /// Gets or sets is expired.
        /// </summary>
        [IgnoreDataMember]
        public bool Expired
        {
            get // gets badge is expired
            {
                return (Expire.HasValue && Expire < DateTime.UtcNow);
            }
        }

        /// <summary>
        /// Gets or sets key ID.
        /// </summary>
        [IgnoreDataMember]
        public override int KeyID
        {
            get { return UserBadgeId; } set { UserBadgeId = value; }
        }

        /// <summary>
        /// Gets or sets image absolute URL.
        /// </summary>
        [IgnoreDataMember]
        public string ImageFileUrl
        {
            get // gets badge image absolute url
            {
                FileInfo file = null;

                if ((file = (FileInfo)FileManager.Instance.GetFile(ImageFileId)) != null)
                {
                    return Globals.ResolveUrl(FileManager.Instance.GetUrl(file)); // try to resolve url
                }

                return null;
            }
        }

        /// <summary>
        /// Gets or sets user ID.
        /// </summary>
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets portal ID.
        /// </summary>
        [DataMember]
        public int PortalId { get; set; }

        /// <summary>
        /// Gets or sets image file ID.
        /// </summary>
        [DataMember]
        public int ImageFileId { get; set; }

        /// <summary>
        /// Gets or sets badge ID.
        /// </summary>
        [DataMember]
        public int BadgeId { get; set; }

        /// <summary>
        /// Gets or sets badge description.
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets badge name.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets expire.
        /// </summary>
        [DataMember]
        public DateTime? Expire { get; set; }

        /// <summary>
        /// Gets or sets create date display.
        /// </summary>
        [DataMember]
        public string ExpireDisplay
        {
            get { return Expire.HasValue ? Expire.Value.ToString("dd/MM/yyyy HH:mm") : null; }
        }

        /// <summary>
        /// Gets or sets expire display.
        /// </summary>
        [DataMember]
        public string ExpireISO
        {
            get { return Expire.HasValue ? Expire.Value.ToString("o") : null; }
        }

        /// <summary>
        /// Gets or sets create date.
        /// </summary>
        [DataMember]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets create date display.
        /// </summary>
        [DataMember]
        public string CreateDateDisplay
        {
            get { return CreateDate.ToString("dd/MM/yyyy HH:mm"); }
        }

        /// <summary>
        /// Gets or sets create date display ISO.
        /// </summary>
        [DataMember]
        public string CreateDateISO
        {
            get { return CreateDate.ToString("o"); }
        }

        /// <summary>
        /// Gets or sets expiration unit.
        /// </summary>
        [DataMember]
        public int? ExpirationUnit { get; set; }

        /// <summary>
        /// Gets or sets expiration quantity.
        /// </summary>
        [DataMember]
        public int? ExpirationQuantity { get; set; }

        /// <summary>
        /// Gets or sets expirable.
        /// </summary>
        [DataMember]
        public bool Expirable { get; set; }

        #endregion

        #region Public Methods : IHydratable

        /// <summary>
        /// Provides hydration.
        /// </summary>
        public override void Fill(IDataReader r)
        {
            UserBadgeId        = Utils.ConvertTo<int>(r["UserBadgeId"]);
            UserId             = Utils.ConvertTo<int>(r["UserId"]);
            PortalId           = Utils.ConvertTo<int>(r["PortalId"]);
            ImageFileId        = Utils.ConvertTo<int>(r["ImageFileId"]);
            BadgeId            = Utils.ConvertTo<int>(r["BadgeId"]);
            Description        = Utils.ConvertTo<string>(r["Description"]);
            Name               = Utils.ConvertTo<string>(r["Name"]);
            CreateDate         = Utils.ConvertTo<DateTime>(r["CreateDate"]);
            ExpirationUnit     = Utils.ConvertTo<int?>(r["ExpirationUnit"]);
            ExpirationQuantity = Utils.ConvertTo<int?>(r["ExpirationQuantity"]);
            Expirable          = Utils.ConvertTo<bool>(r["Expirable"]);
            Expire             = Utils.ConvertTo<DateTime?>(r["Expire"]);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UserBadge() { }

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        public UserBadge(int id) { UserBadgeId = id; }

        #endregion
    }
}
