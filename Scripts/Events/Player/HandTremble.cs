using Il2CppSLZ.Marrow;

using UnityEngine;

using Random = UnityEngine.Random;

namespace NEP.Paranoia.Events.Player
{
    public class HandTremble : ParanoiaEvent
    {
        private Rigidbody m_leftHand;
        private Rigidbody m_rightHand;

        private float m_timer = 0f;
        private float m_duration = 0f;
        
        public override void Start()
        {
            base.Start();

            m_timer = 0f;
            m_duration = 0f;
            
            PhysicsRig physicsRig = BoneLib.Player.PhysicsRig;

            m_leftHand = physicsRig.leftHand.rb;
            m_rightHand = physicsRig.rightHand.rb;

            if(m_leftHand == null || m_rightHand == null) { return; }

            m_duration = Random.Range(10, 20f);
        }

        public override void Update()
        {
            m_timer += Time.deltaTime;

            if (m_timer >= m_duration)
            {
                m_timer = 0f;
                Stop();
                return;
            }
            
            float rand = Random.Range(0.25f, 0.50f);
            
            m_leftHand.AddForce(Random.rotation.eulerAngles * rand);
            m_rightHand.AddForce(Random.rotation.eulerAngles * rand);

            m_leftHand.AddTorque(Random.rotation.eulerAngles * rand);
            m_rightHand.AddTorque(Random.rotation.eulerAngles * rand);
        }
    }
}
