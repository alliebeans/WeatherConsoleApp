using System;
using WeatherApp.REMOTE_DATABASE;
using Isopoh.Cryptography.Argon2;

namespace WeatherApp.UserAccount
{
    public class LoginVerifier
    {
        REMOTE_DATABASE.UserLoginNameStorage names;
        REMOTE_DATABASE.PasswordHashStorage passwords;
        
        public LoginVerifier(REMOTE_DATABASE.UserLoginNameStorage names, REMOTE_DATABASE.PasswordHashStorage passwords)
        {
            this.names = names;
            this.passwords = passwords;
        }
        public bool VerifyLogin(string username, string password)
        {
            if (VerifyUsername(username))
                if (VerifyPassword(username, password))
                    return true;
            return false;
        }

        private bool VerifyUsername(string username)
        { 
            string? userAccountID;
            if (!names.StoredUserLoginNames.TryGetValue(username, out userAccountID))
                return false;
            return true;
        }

        private bool VerifyPassword(string username, string password)
        {
            string? userAccountID;
            string? passwordHash;
            names.StoredUserLoginNames.TryGetValue(username, out userAccountID);
            if (!passwords.StoredPasswordHashes.TryGetValue(userAccountID!, out passwordHash))
                return false;
            return Argon2.Verify(passwordHash, password);
        }
    }
}