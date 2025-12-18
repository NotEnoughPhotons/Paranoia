using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEP.Paranoia.Data
{
    [JsonObject(MemberSerialization.Fields)]
    public sealed class EventDefinition
    {
        [JsonObject(MemberSerialization.Fields)]
        public struct TimeDefinition
        {
            [JsonProperty("min_time")] public float MinTime;
            [JsonProperty("max_time")] public float MaxTime;
        }

        [JsonObject(MemberSerialization.Fields)]
        public struct RandomDefinition
        {
            [JsonProperty("min_rng")] public float MinRng;
            [JsonProperty("max_rng")] public float MaxRng;
        }

        [JsonProperty("event")] public string Event;
        [JsonProperty("insanity")] public float Insanity;
        [JsonProperty("time")] public TimeDefinition Time;
        [JsonProperty("random")] public RandomDefinition Random;
    }
}
