using BoneLib;

using Il2CppSLZ.Marrow;
using Il2CppSLZ.Marrow.Interaction;

using UnityEngine;

using NEP.Paranoia.Events;

namespace NEP.Paranoia.Managers
{
    public static class ParanoiaDirector
    {
        private static List<ParanoiaEvent> m_events;

        internal static void Initialize()
        {
            m_events = new List<ParanoiaEvent>();
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