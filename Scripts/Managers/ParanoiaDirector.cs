using BoneLib;

using Il2CppSLZ.Marrow;
using Il2CppSLZ.Marrow.Interaction;
using Il2CppSLZ.Marrow.Warehouse;

using NEP.Paranoia.Entities;
using UnityEngine;

using NEP.Paranoia.Events;
using NEP.Paranoia.Events.AI;
using NEP.Paranoia.Events.Player;
using NEP.Paranoia.Events.Spawners;
using MelonLoader;

namespace NEP.Paranoia.Managers
{
    public static class ParanoiaDirector
    {
        public static float Insanity => m_insanity;
        public static IReadOnlyList<Entity> Entities { get; internal set; }

        private static float m_insanity;

        private static Pallet m_pallet;
        
        private static List<Entity> m_entities;
        
        private static List<ParanoiaEvent> m_events;
        private static Dictionary<string, ParanoiaEvent> m_registeredEvents;

        internal static void OnLevelLoaded(LevelInfo levelInfo) => Initialize();
        
        internal static void Initialize()
        {
            if (!AssetWarehouse.Instance.TryGetPallet(new Barcode("NEP.Paranoia"), out Pallet pallet))
                throw new NotSupportedException("Paranoia pallet is not installed!");
            
            m_pallet = pallet;
            
            m_entities = new List<Entity>();
            m_events = new List<ParanoiaEvent>();
            m_registeredEvents = new Dictionary<string, ParanoiaEvent>();
            
            WarmupEntities();
            Entities = m_entities;

            RegisterAllEvents();
        }

        public static void WarmupEntities()
        {
            foreach (var entityCrate in m_pallet.Crates)
            {
                SpawnableCrateReference crateReference = new SpawnableCrateReference()
                {
                    Barcode = entityCrate.Barcode
                };
                
                HelperMethods.SpawnCrate(crateReference, Vector3.zero, Quaternion.identity, Vector3.one);
            }
        }

        public static void RegisterEntity(Entity entity)
        {
            entity.EntityStop();
            m_entities.Add(entity);
        }
        
        public static void RegisterEvent<T>() where T : ParanoiaEvent
        {
            string typeName = typeof(T).Name;
            
            if (typeof(T).IsAbstract || !typeof(T).IsSubclassOf(typeof(ParanoiaEvent)))
                throw new NotSupportedException($"Event type {typeName} does not derive from ParanoiaEvent!");

            // Already registered
            if (m_registeredEvents.ContainsKey(typeName))
                return;
            
            var instance = Activator.CreateInstance<T>();
            
            m_events.Add(instance);
            m_registeredEvents.Add(typeName, instance);
        }

        public static void RegisterAllEvents()
        {
            Type[] types = typeof(ParanoiaEvent).GetNestedTypes();

            foreach (Type type in types)
            {
                if (m_registeredEvents.ContainsKey(type.Name))
                    continue;

                ParanoiaEvent instance = Activator.CreateInstance(type) as ParanoiaEvent;

                m_events.Add(instance);
                m_registeredEvents.Add(type.Name, instance);
            }
        }

        public static void Spawn<T>() where T : Entity
        {
            if (m_entities.Count == 0)
                return;
            
            if (typeof(T).IsAbstract || !typeof(T).IsSubclassOf(typeof(Entity)))
                throw new NotSupportedException($"Event type {typeof(T).Name} does not derive from Entity!");

            T entity = null;

            foreach (var entityObject in m_entities)
            {
                if (entityObject.GetType() == typeof(T))
                {
                    entity = entityObject as T;
                    break;
                }
            }
            
            if (entity == null)
                throw new NullReferenceException($"Could not find entity of type {typeof(T).Name} in list!");
            
            entity.EntityStart();
        }
        
        public static void Spawn(Entity entity)
        {
            if (entity == null)
                return;
            
            entity.EntityStart();
        }

        public static T GetEntity<T>() where T : Entity
        {
            foreach (var entity in Entities)
            {
                if (entity is T)
                    return (T)entity;
            }

            return null;
        }

        public static float RandomGaussian(float standardDeviation)
        {
            float r1 = UnityEngine.Random.value;
            float r2 = UnityEngine.Random.value;
            float rStdNorm = Mathf.Sqrt(-2f * Mathf.Log(r1, 10)) * Mathf.Sin(2 * Mathf.PI * r2);
            return rStdNorm * standardDeviation;
        }

        public static float RandomGaussianRange(float standardDeviation, float range)
        {
            float f = 0f;

            do
            {
                f = RandomGaussian(standardDeviation);
            } while (Mathf.Abs(f) > range);

            return f;
        }

        public static float RandomLogNormal(float mean, float standardDeviation)
        {
            return Mathf.Exp(RandomGaussian(standardDeviation)) * mean;
        }
        
        public static void Update()
        {
            if (m_events == null)
                return;

            m_insanity += Time.deltaTime * (1f / 60f);

            foreach (var paranoiaEvent in m_events)
            {
                try
                {
                    if (paranoiaEvent.CanStart())
                        paranoiaEvent.Start();
                    
                    if (paranoiaEvent.Started)
                        paranoiaEvent.Update();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static void FreezePlayer()
        {
            RigManager manager = Player.RigManager;
            
            if (!manager)
                return;

            PhysicsRig rig = manager.physicsRig;
            MarrowBody[] bodies = rig.marrowEntity.Bodies;

            foreach (var body in bodies)
            {
                if (!body.TryGetRigidbody(out Rigidbody rb))
                    continue;

                rb.isKinematic = true;
            }
        }
        
        public static void UnfreezePlayer()
        {
            RigManager manager = Player.RigManager;
            
            if (!manager)
                return;

            PhysicsRig rig = manager.physicsRig;
            MarrowBody[] bodies = rig.marrowEntity.Bodies;

            foreach (var body in bodies)
            {
                if (!body.TryGetRigidbody(out Rigidbody rb))
                    continue;

                rb.isKinematic = false;
            }
        }

        public static void RagdollPlayer()
        {
            RigManager manager = Player.RigManager;
            
            if (!manager)
                return;

            PhysicsRig rig = manager.physicsRig;
            
            rig.ShutdownRig();
            rig.RagdollRig();
        }
        
        public static void UnRagdollPlayer()
        {
            RigManager manager = Player.RigManager;
            
            if (!manager)
                return;
            
            PhysicsRig rig = manager.physicsRig;
            
            rig.TurnOnRig();
            rig.UnRagdollRig();
        }
    }
}