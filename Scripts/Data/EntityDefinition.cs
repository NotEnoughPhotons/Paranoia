using Newtonsoft.Json;

namespace NEP.Paranoia.Data
{
    [JsonObject(MemberSerialization.Fields)]
    public sealed class EntityDefinition
    {
        [JsonObject(MemberSerialization.Fields)]
        public struct SpawnDefinition
        {
            [JsonProperty("radius")] public float Radius;
        }

        [JsonObject(MemberSerialization.Fields)]
        public struct MovementDefinition
        {
            [JsonProperty("speed")] public float Speed;
        }

        [JsonObject(MemberSerialization.Fields)]
        public struct VitalsDefinition
        {
            [JsonProperty("health")] public float Health;
            [JsonProperty("immortal")] public bool Immortal;
        }

        [JsonObject(MemberSerialization.Fields)]
        public struct AttackDefinition
        {
            [JsonProperty("damage")] public float Damage;
        }

        [JsonObject(MemberSerialization.Fields)]
        public struct AudioDefinition
        {
            public AudioDefinition()
            {

            }

            [JsonProperty("volume")] public float Volume = 1f;
            [JsonProperty("spatial")] public float Spatial = 0f;
            [JsonProperty("min_pitch")] public float MinPitch = 1f;
            [JsonProperty("max_pitch")] public float MaxPitch = 1f;
            [JsonProperty("min_distance")] public float MinDistance = 0f;
            [JsonProperty("max_distance")] public float MaxDistance = 500f;
            [JsonProperty("loop")] public bool Loop = false;
            [JsonProperty("clips")] public string[] Clips;
        }

        [JsonProperty("entity")] public string Entity;
        [JsonProperty("insanity")] public float Insanity;
        [JsonProperty("max_distance")] public float MaxDistance;

        [JsonProperty("spawn")] public SpawnDefinition Spawn;
        [JsonProperty("movement")] public MovementDefinition Movement;
        [JsonProperty("vitals")] public VitalsDefinition Vitals;
        [JsonProperty("attack")] public AttackDefinition Attack;
        [JsonProperty("audio")] public AudioDefinition Audio;
    }
}
