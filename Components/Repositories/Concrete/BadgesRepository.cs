namespace DNNGamification.Components.Repositories
{
    using DotNetNuke.Common;
    using DotNetNuke.Common.Utilities;

    using DNNGamification.Components.Entities;

    using DotNetNuke.Data;

    using System.Collections;
    using System.Collections.Generic;

    using System.Data;

    /// <summary>
    /// Badges repository.
    /// </summary>
    public class BadgesRepository : RepositoryBase
    {
        #region Public Properties : Insert

        /// <summary>
        /// Adds badge.
        /// </summary>
        public int Add(int imageFileId, int portalId, string name, string description, int? unit, int? quantity, bool expirable)
        {
            return DataProvider.ExecuteScalar<int>("DNNGamification_AddBadge", imageFileId, portalId, name, description, unit, quantity, expirable);
        }

        #endregion

        #region Public Properties : Select

        /// <summary>
        /// Gets badge by ID.
        /// </summary>
        public Badge GetBy(int id)
        {
            return CBO.FillObject<Badge>(DataProvider.ExecuteReader("DNNGamification_GetBadgeById", id));
        }

        /// <summary>
        /// Gets badges paged view.
        /// </summary>
        public List<Badge> GetView(int portalId, int startIndex, int length, string orderBy, string orderByDirection, out int total)
        {
            IDataReader reader = DataProvider.ExecuteReader("DNNGamification_GetBadgesView", portalId, startIndex, length, orderBy, orderByDirection);

            reader.Read(); total = Utils.ConvertTo<int>(reader["TotalCount"]); reader.NextResult();
            {
                return CBO.FillCollection<Badge>(reader); // return total first - badges paged list data next
            }
        }

        /// <summary>
        /// Gets all badges.
        /// </summary>
        public List<Badge> GetAll(int portalId)
        {
            return CBO.FillCollection<Badge>(DataProvider.ExecuteReader("DNNGamification_GetBadges", portalId));
        }

        #endregion

        #region Public Properties : Update

        /// <summary>
        /// Updates badge.
        /// </summary>
        public void Update(int id, int? imageFileId, int? portalId, string name, string description, int? unit, int? quantity, bool expirable)
        {
            DataProvider.ExecuteNonQuery("DNNGamification_UpdateBadge", id, imageFileId, portalId, name, description, unit, quantity, expirable);
        }

        #endregion

        #region Public Properties : Delete

        /// <summary>
        /// Deletes badge.
        /// </summary>
        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("DNNGamification_DeleteBadgeById", id);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        public BadgesRepository(DataProvider provider)
            : base(provider)
        { }

        #endregion
    }
}
