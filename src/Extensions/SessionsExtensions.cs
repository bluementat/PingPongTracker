using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace PingPongTracker.Extensions;

#pragma warning disable CS8603 // Possible null reference return.
public static class SessionsExtensions
{
    public static void Set<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static T Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonConvert.DeserializeObject<T>(value);
    }
}
