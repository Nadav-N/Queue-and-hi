using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace QueueAndHi.BL.Authentication
{
    public class AuthTokenSerializer : IAuthTokenSerializer
    {
        private const string tokenSerializationFormat = "{0};{1}";
        public AuthenticationToken Serialize(UserCredentials userCredentials)
        {
            string encryptedUsername = Encrypt(userCredentials.Username);
            string encryptedPassword = Encrypt(userCredentials.Password);
            return new AuthenticationToken(String.Format(tokenSerializationFormat, encryptedUsername, encryptedPassword));
        }

        public UserCredentials Deserialize(AuthenticationToken authToken)
        {
            string[] tokenParts = authToken.TokenString.Split(';');
            if(tokenParts.Length > 2)
            {
                throw new InvalidOperationException("Invalid authentication token format received. Auth token should contain one and only one ';' character");
            }

            string decryptedUsername = Decrypt(tokenParts[0]);
            string decryptedPassword = Decrypt(tokenParts[1]);

            return new UserCredentials(decryptedUsername, decryptedPassword);
        }

        private string Encrypt(string decryptedString)
        {
            byte[] decryptedStringBytes = System.Text.Encoding.UTF8.GetBytes(decryptedString);
            return Convert.ToBase64String(decryptedStringBytes);
        }

        private string Decrypt(string encryptedString)
        {
            var encryptedStringBytes = System.Convert.FromBase64String(encryptedString);
            return Encoding.UTF8.GetString(encryptedStringBytes);
        }
    }
}