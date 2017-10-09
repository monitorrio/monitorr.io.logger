namespace log.elmahbucket.io.Infrastructure.Domain
{
    public class UserAccess : MongoEntity
    {
        public string UserId { get; set; }
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
    }
}
