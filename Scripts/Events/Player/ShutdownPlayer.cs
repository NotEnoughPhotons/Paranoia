using Il2CppSLZ.Marrow;
using NEP.Paranoia.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NEP.Paranoia.Events.Player
{
    public class ShutdownPlayer : ParanoiaEvent
    {
        private RigManager m_self;
        private RigManager m_rigTarget;
        private Rigidbody m_rightHand;

        private float m_timer = 0f;
        private float m_duration = 10f;
        
        public override void Start()
        {
            base.Start();
            
            // Get physics rig
            PhysicsRig rig = BoneLib.Player.PhysicsRig;
            
            rig.ShutdownRig();

            m_self = BoneLib.Player.RigManager;
            RigManager[] otherManagers = RigManager.Cache.ToArray();
            m_rigTarget = otherManagers[Random.Range(0, otherManagers.Length)];

            m_rightHand = m_self.physicsRig.leftHand.rb;
        }

        public override void Update()
        {
            if (m_rigTarget == null)
            {
                Stop();
                return;
            }
            
            if (m_timer < m_duration)
            {
                m_timer += Time.deltaTime;
                m_rightHand.AddForce((m_rightHand.transform.position - m_rigTarget.physicsRig.m_head.position) * 175f, ForceMode.Force);
                return;
            }

            Stop();
        }

        public override void Stop()
        {
            base.Stop();
            m_timer = 0f;
            m_rigTarget = null;
            m_self.physicsRig.TurnOnRig();
        }
    }
}