namespace tellahs_library.Dtos
{
    public class Registration
    {
        public required string UserName { get; set; }
        public ulong GuildId { get; set; }
        public ulong UserId { get; set; }
    }
}
