namespace tellahs_library.Dtos
{
    public class RegistrationResponse
    {
        public int RegistrantCount { get; set; }
        public bool IsRegistered { get; set; } = false;
        public uint EntrantTrackingChannelId { get; set; }
        public uint EntrantTrackingMessageId { get; set; }
    }
}
