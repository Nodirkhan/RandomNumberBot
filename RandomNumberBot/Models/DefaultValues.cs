using System.Collections.Generic;

namespace RandomNumberBot.Entity
{
    public static class DefaultValues
    {
        public static string TOKEN { get; set; } = "5591277108:AAF2XMvPSoOj9ivnITuYRJk-3QcK1jGX-rs";
        public static string CONNECTION { get; set; } = "Server=localhost;Port=5432;Database=VoterBot;Username=postgres;Password=20020623;";

        public static List<long> admins = new List<long>
        {
            738082106
        };
    }
}
