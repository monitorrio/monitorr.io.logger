using MongoDB.Bson.Serialization.Attributes;
using System;

namespace monitorr.logger.Infrastructure.Domain
{
    public class User : MongoEntity
    {
        public string Auth0Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool RegistrationComplete { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string PictureUrl { get; set; }
        public string RoleId { get; set; }

        [BsonIgnore]
        public string FullName => FirstName + " " + LastName;

    }
}
