using DAL;
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
        private readonly AuthTokenCache tokenCache;
        private readonly UserOps userOps;

        public AuthTokenSerializer(UserOps userOps)
        {
            this.tokenCache = AuthTokenCache.Instance;
            this.userOps = userOps;
        }

        public bool ValidateAndSerialize(UserCredentials userCredentials, out AuthenticatedIdentity serializedIdentity)
        {
            serializedIdentity = null;
            UserInfo loggedUser;
            if (userOps.TryLogin(userCredentials, out loggedUser))
            {
                string encryptedUsername = Encrypt(userCredentials.Username);
                string encryptedPassword = Encrypt(userCredentials.Password);
                string tokenString = String.Format(tokenSerializationFormat, encryptedUsername, encryptedPassword);
                int userId = loggedUser.ID;
                this.tokenCache.TokenCache.TryAdd(tokenString, userId);

                serializedIdentity = new AuthenticatedIdentity
                    {
                        Token = new AuthenticationToken(tokenString),
                        UserID = userId
                    };
                return true;
            }

            return false;
        }

        public int Deserialize(AuthenticationToken authToken)
        {
            int userId;
            if (!this.tokenCache.TokenCache.TryGetValue(authToken.TokenString, out userId))
            {
                string[] tokenParts = authToken.TokenString.Split(';');
                if (tokenParts.Length > 2)
                {
                    throw new InvalidOperationException(
                        "Invalid authentication token format received. Auth token should contain one and only one ';' character");
                }

                string decryptedUsername = Decrypt(tokenParts[0]);
                string decryptedPassword = Decrypt(tokenParts[1]);

                UserCredentials userCredentials = new UserCredentials(decryptedUsername, decryptedPassword);
                UserInfo loggedUser;
                if (this.userOps.TryLogin(userCredentials, out loggedUser))
                {
                    userId = loggedUser.ID;
                    this.tokenCache.TokenCache.TryAdd(authToken.TokenString, userId);
                }
                else
                {
                    throw new ArgumentException("Authentication token could not be deserialized or decrypted");
                }
            }

            return userId;
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