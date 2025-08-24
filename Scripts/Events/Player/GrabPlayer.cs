using Il2CppSLZ.Marrow;
using NEP.Paranoia.Managers;
using UnityEngine;

using Random = UnityEngine.Random;

namespace NEP.Paranoia.Events.Player
{
    public class GrabPlayer : ParanoiaEvent
    {
        private float m_initialDelayTimer = 0f;
        private float m_initialDelay = 0f;

        private float m_timer = 0f;
        private float m_duration = 7f;

        private float m_force;
        private Vector3 m_direction;

        private Rigidbody m_limb;
        
        public override void Start()
        {
            // Get physics rig
            PhysicsRig rig = BoneLib.Player.PhysicsRig;

            Rigidbody[] rbs = new Rigidbody[]
            {
                rig.leftHand.rb,
                rig.rightHand.rb,
                rig.legLf.rbEnd, // Left foot?
                rig.legRt.rbEnd // Right foot?
            };

            m_limb = rbs[Random.Range(0, rbs.Length)];

            m_initialDelay = 5f;
            
            m_direction = Vector3.up + (Random.onUnitSphere * 10f);
            m_force = Random.Range(50f, 100f);
        }

        public override void Update()
        {
            if (m_initialDelayTimer <= m_initialDelay)
            {
                m_initialDelayTimer += Time.deltaTime;
                return;
            }
            
            // Insert grab sound effect here.
            //AudioSource.PlayClipAtPoint(Paranoia.instance.grabSounds[Random.Range(0, Paranoia.instance.grabSounds.Count)], part.position);
            
            ParanoiaDirector.RagdollPlayer();
            
            m_limb.AddForce(m_direction * m_force, ForceMode.Acceleration);

            if (m_timer <= m_duration)
            {
                m_timer += Time.deltaTime;
                return;
            }
            
            ParanoiaDirector.UnRagdollPlayer();
        }
    }
}
