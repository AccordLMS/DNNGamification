namespace DNNGamification
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Available row states.
    /// </summary>
    public enum RowState
    {
        [EnumMember] Existing,
        [EnumMember] New,
        [EnumMember] Deleted
    }

    /// <summary>
    /// Available leaderboard modes.
    /// </summary>
    public enum LeaderboardMode
    {
        [EnumMember] All,
        [EnumMember] GroupMembers,
        [EnumMember] UserCurrent,
        [EnumMember] UserProfile,
        [EnumMember] FriendsCurrent,
        [EnumMember] FriendsProfile
    }

    /// <summary>
    /// Available expiration units.
    /// </summary>
    [DataContract]
    public enum ExpirationUnits
    {
        [EnumMember] Days,
        [EnumMember] Months,
        [EnumMember] Years
    }
}
