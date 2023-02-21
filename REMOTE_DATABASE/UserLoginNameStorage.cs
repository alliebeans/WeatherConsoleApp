using System;

namespace WeatherApp.REMOTE_DATABASE
{
    public class UserLoginNameStorage
    {
        public int Count { get; private set; }
        public Dictionary<string, string> StoredUserLoginNames { get; private set; }
        public UserLoginNameStorage()  
        {
            Count = 0;
            StoredUserLoginNames = new Dictionary<string, string>();
        }
    }
}