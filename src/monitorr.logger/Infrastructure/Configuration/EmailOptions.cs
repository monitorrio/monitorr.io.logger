namespace monitorr.logger.Infrastructure.Configuration
{
    public class EmailOptions
    {
        public string From { get; set; }
        public string NewErrorSubject { get; set; }
        public string TestEmailRecivers { get; set; }
    }
}
