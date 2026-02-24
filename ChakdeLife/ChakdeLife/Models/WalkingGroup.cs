namespace ChakdeLife.Models
{
    public class WalkingGroup
    {
        public int WalkingGroupId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public DateOnly EventDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public ICollection<WalkingGroupMember>? Members { get; set; }
    }
}
