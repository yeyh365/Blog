namespace Blog.Core.Helper
{
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System;
    using System.Web;
    public static class SessionHelper
    {
        public static void SetSessionValue<T>(this ISession Session, string key, T value)
        {
            Session.SetSessionValue(key, JsonConvert.SerializeObject(value));
        }

        public static T? GetSessionValue<T>(this ISession Session, string key)
        {
            var value = Session.GetSessionValue<string>(key);
            return string.IsNullOrEmpty(value) ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}