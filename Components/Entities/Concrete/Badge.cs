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
    /// Badge entity.
    /// </summary>
    [DataContract, Serializable]
    public class Badge : EntityBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets badge ID.
        /// </summary>
        [DataMember]
        public int BadgeId { get; set; }

        /// <summary>
        /// Gets or sets key ID.
        /// </summary>
        [DataMember]
        public override int KeyID
        {
            get { return BadgeId; } set { BadgeId = value; }
        }

        /// <summary>
        /// Gets or sets image file ID.
        /// </summary>
        [DataMember]
        public int ImageFileId { get; set; }

        /// <summary>
        /// Gets or sets image file URL.
        /// </summary>
        [IgnoreDataMember]
        public string ImageFileUrl
        {
            get // gets badge image-file url
            {
                string result = null; FileInfo fileInfo = null;

                if ((fileInfo = (FileInfo)FileManager.Instance.GetFile(ImageFileId)) != null)
                {
                    result = Globals.ResolveUrl(FileManager.Instance.GetUrl(fileInfo)); // get image url
                }

                return result;
            }
        }

        /// <summary>
        /// Gets or sets portal ID.
        /// </summary>
        [DataMember]
        public int PortalId { get; set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? ExpirationUnit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? ExpirationQuantity { get; set; }

        /// <summary>
        /// 
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
            BadgeId            = Utils.ConvertTo<int>(r["BadgeId"]);
            ImageFileId        = Utils.ConvertTo<int>(r["ImageFileId"]);
            PortalId           = Utils.ConvertTo<int>(r["PortalId"]);
            Name               = Utils.ConvertTo<string>(r["Name"]);
            Description        = Utils.ConvertTo<string>(r["Description"]);
            ExpirationUnit     = Utils.ConvertTo<int?>(r["ExpirationUnit"]);
            ExpirationQuantity = Utils.ConvertTo<int?>(r["ExpirationQuantity"]);
            Expirable          = Utils.ConvertTo<bool>(r["Expirable"]);
        }

        #endregion
    }
}
