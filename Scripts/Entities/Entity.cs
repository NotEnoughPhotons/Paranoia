using BoneLib;

using Il2CppSLZ.Marrow;
using Il2CppSLZ.Marrow.Interaction;
using Il2CppSLZ.Marrow.Pool;
using UnityEngine;

using MelonLoader;
using NEP.Paranoia.Managers;
using Random = UnityEngine.Random;
using NEP.Paranoia.Audio;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public abstract class Entity(IntPtr ptr) : MonoBehaviour(ptr)
    {
        private struct AudioTimeStamp
        {
            public AudioTimeStamp(AudioClip clip, float time)
            {
                this.clip = clip;
                this.time = time;
            }

            public AudioClip clip;
            public float time;
        }

        public float Insanity => m_insanity;

        public bool Active => m_active;

        protected EntityFlags m_entityFlags;
        protected SpawnFlags m_spawnFlags;

        protected RigManager m_target;
        protected Transform m_targetTransform;

        protected Vector3 m_position;
        protected Vector3 m_scale;

        protected AudioSource m_source;

        protected bool m_active;

        protected float m_health;
        protected float m_lifetime;
        protected float m_attackDamage;

        protected float m_speed;
        protected float m_minDistance;
        protected float m_maxDistance;

        protected float m_fadePercent;

        protected float m_insanity;

        private Poolee m_poolee;
        
        private List<AudioTimeStamp> m_timestamps;
 
        public static Vector3 SpherePoint(Vector3 position, float radius = 1f, float angle = 0f)
        {
            float x = position.x + Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            float y = position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            float z = position.z + Mathf.Cos(angle * Mathf.Deg2Rad) * radius;

            return new Vector3(x, y, z);
        }

        public static Vector3 CirclePoint(Vector3 position, float radius = 1f, float angle = 0f, float height = 0f)
        {
            float x = position.x + Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            float y = position.y + height;
            float z = position.z + Mathf.Cos(angle * Mathf.Deg2Rad) * radius;

            return new Vector3(x, y, z);
        }

        public static Vector3 RandomCirclePoint(Vector3 position, float radius = 1f, float height = 0f)
        {
            return CirclePoint(position, radius, Random.Range(0f, 360f), height);
        }

        public static Vector3 RandomSpherePoint(Vector3 position, float radius = 1f)
        {
            return SpherePoint(position, radius, Random.Range(0f, 360f));
        }

        public Vector3 AroundTarget(float radius = 1f)
        {
            return RandomSpherePoint(m_targetTransform.position, radius);
        }

        public Vector3 AroundTarget(float radius = 1f, float height = 0f)
        {
            return RandomCirclePoint(m_targetTransform.position, radius, height);
        }

        private void Update()
        {
            m_position = transform.position;
            m_scale = transform.localScale;
            EntityUpdate();
        }
        
        private void OnEnable() => EntityStart();
        private void OnDisable() => EntityStop();
        
        protected virtual void Awake()
        {
            m_source = gameObject.AddComponent<AudioSource>();
            
            m_timestamps = new List<AudioTimeStamp>();

            m_poolee = GetComponent<Poolee>();
            
            ParanoiaDirector.RegisterEntity(this);
        }
        
        public virtual void EntityStart()
        {
            m_active = true;
            m_target = Player.RigManager;
            m_targetTransform = Player.Head;
        }

        public virtual void EntityStop()
        {
            m_active = false;
            m_target = null;
            m_targetTransform = null;
            m_poolee.Despawn();
        }

        protected virtual void EntityUpdate() { }

        protected void Appear() => gameObject.SetActive(true);
        
        protected void Disappear() => gameObject.SetActive(false);

        protected void FaceTarget()
        {
            if (!m_targetTransform)
                return;
            
            transform.LookAt(m_targetTransform);
        }

        protected float DistanceToTarget()
        {
            if (!m_target)
                return 0f;
            
            return Vector3.Distance(m_position, m_targetTransform.position);
        }

        protected bool BeingLookedAt()
        {
            return Vector3.Dot(m_targetTransform.forward, transform.forward) <= 0.8f;
        }
        
        protected void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
        
        protected void Move()
        {
            transform.position += transform.forward * (m_speed * Time.deltaTime);
        }

        protected void TakeDamage(float damage)
        {
            if (damage >= m_health)
            {
                m_health = 0f;
                return;
            }

            damage -= m_health;
        }

        protected void DamageTarget()
        {
            if (!m_target)
                return;
            
            Player_Health health = m_target.health.TryCast<Player_Health>();

            if (health.deathIsImminent)
                return;
            
            health.TAKEDAMAGE(m_attackDamage);
        }

        protected void UseAudio(float volume = 1f, float pitch = 1f, float spatial = 1f)
        {
            if (!m_source)
                m_source = gameObject.AddComponent<AudioSource>();
            
            m_source.volume = volume;
            m_source.pitch = pitch;
            m_source.spatialBlend = spatial;
            m_source.dopplerLevel = 0f;
        }

        protected void Emit(AudioClip clip)
        {
            if (!m_source)
                return;

            if (clip == null)
                return;

            if (m_source.isPlaying)
                m_source.Stop();
            
            m_source.clip = clip;
            m_source.Play();
        }
        
        protected void Emit(AudioClip[] clips)
        {
            if (!m_source)
                return;
         
            if (clips == null)
                return;
            
            AudioClip random = clips[UnityEngine.Random.Range(0, clips.Length)];
            
            m_source.clip = random;
            m_source.Play();
        }

        protected void Emit(string soundName)
        {
            if (AudioBank.Master.TryGetValue(soundName, out AudioClip clip))
                Emit(clip);
            else
                Paranoia.Logger.Warning($"Couldn't load non-existent bank sound {soundName}!");
        }

        protected void SetInsanity(float insanity)
        {
            if (insanity < 0f)
                insanity = 0f;

            m_insanity = insanity;
        }

        protected void SetVolume(float volume)
        {
            if (!m_source)
                return;
            
            m_source.volume = volume;
        }
        
        protected void SetPitch(float pitch)
        {
            if (!m_source)
                return;
            
            m_source.pitch = pitch;
        }
        
        protected void SetSpatial(float spatial)
        {
            if (!m_source)
                return;
            
            m_source.spatialBlend = spatial;
        }

        protected void SetLooping(bool loop)
        {
            if (!m_source)
                return;
            
            m_source.loop = loop;
        }

        protected void SetMinDistance(float distance)
        {
            if (!m_source)
                return;
            
            m_source.minDistance = distance;
        }
        
        protected void SetMaxDistance(float distance)
        {
            if (!m_source)
                return;
            
            m_source.maxDistance = distance;
        }
        
        private void AddAudioTimestamp(AudioTimeStamp stamp)
        {
            if (m_timestamps == null)
                m_timestamps = new List<AudioTimeStamp>();

            if (m_timestamps.Count >= 10)
                m_timestamps.Clear();

            m_timestamps.Add(stamp);
        }
    }

}