using System.Text.Json;
namespace PigGame.Models
{
    public static class SessionHelper
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString("key", JsonSerializer.Serialize(value));
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            string? json = session.GetString("key");
            if(String.IsNullOrEmpty(json)) {
                return default(T);
            }
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
