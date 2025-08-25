using System.Reflection;
using BoneLib;

using Il2CppSLZ.Marrow;
using Il2CppSLZ.Marrow.Interaction;

using UnityEngine;

using NEP.Paranoia.Events;
using NEP.Paranoia.Events.Player;
using NEP.Paranoia.Events.Spawners;
using NEP.Paranoia.Events.World;
using Random = UnityEngine.Random;

namespace NEP.Paranoia.Managers
{
    public static class ParanoiaDirector
    {
        private static List<ParanoiaEvent> m_events;

        private static float m_timer;
        private static float m_nextEventTime;
        
        internal static void Initialize()
        {
            m_events = new List<ParanoiaEvent>();
            
            //m_events.Add(new HandTremble());
            //m_events.Add(new GrabPlayer());
            m_events.Add(new SpawnEntity());
            //m_events.Add(new FireGunInHand());
            m_events.Add(new ShutdownPlayer());

            m_nextEventTime = Random.Range(10f, 20f);
        }

        public static void Update()
        {
            m_timer += Time.deltaTime;

            if (m_timer >= m_nextEventTime)
            {
                ParanoiaEvent randomEvent = m_events[Random.Range(0, m_events.Count)];
                m_timer = 0f;
                m_nextEventTime = Random.Range(10f, 20f);
                randomEvent.Start();
            }

            foreach (var paranoiaEvent in m_events)
            {
                try
                {
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