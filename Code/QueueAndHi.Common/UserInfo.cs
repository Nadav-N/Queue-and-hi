using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common
{
    [DataContract]
    public class UserInfo : IEquatable<UserInfo>
    {
        [DataMember]
        public int Ranking { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public bool IsAdmin { get; set; }

        [DataMember]
        public bool IsOwner { get; set; }

        [DataMember]
        public bool IsMuted { get; set; }

        [DataMember]
        public int ID { get; set; }

        public override int GetHashCode()
        {
            return Ranking.GetHashCode() * 17 +
            Username.GetHashCode() * 17 +
            EmailAddress.GetHashCode() * 17 +
            IsAdmin.GetHashCode() * 17 +
            IsMuted.GetHashCode() * 17 +
            IsOwner.GetHashCode() * 17 +
            ID.GetHashCode();
        }

        public bool Equals(UserInfo other)
        {
            return Ranking.Equals(other.Ranking) &&
                Username.Equals(other.Username) &&
                EmailAddress.Equals(other.EmailAddress) &&
                IsAdmin.Equals(other.IsAdmin) &&
                IsOwner.Equals(other.IsOwner) &&
                IsMuted.Equals(other.IsMuted) &&
                ID.Equals(other.ID);
        }
    }
}
