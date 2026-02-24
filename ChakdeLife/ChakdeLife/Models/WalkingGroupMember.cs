namespace ChakdeLife.Models
{
    public class WalkingGroupMember
    {
        public int WalkingGroupMemberId { get; set; }

        public int WalkingGroupId { get; set; } // Foreign key to WalkingGroup

        public string UserId { get; set; } //Foreign key to User

        public DateOnly JoinedDate { get; set; }
        public WalkingGroup WalkingGroup { get; set; }

    }
}
