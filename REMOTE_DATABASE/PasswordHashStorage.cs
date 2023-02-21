using System;

namespace WeatherApp.REMOTE_DATABASE
{
    public class PasswordHashStorage
    {
        public Dictionary<string, string> StoredPasswordHashes { get; private set; }
        public PasswordHashStorage()  
        {
            StoredPasswordHashes = new Dictionary<string, string>();
        }
    }
}