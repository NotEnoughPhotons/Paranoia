using UnityEngine;

using MelonLoader;

using NEP.Paranoia.Audio;
using NEP.Paranoia.Managers;

using Random = UnityEngine.Random;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class Paralyzer(IntPtr ptr) : Entity(ptr)
    {
        private float m_timer = 0f;
        private float m_nextMove;
        
        public override void EntityStart()
        {
            UseAudio();
            SetSpatial(0.75f);
            SetPitch(Random.Range(0.85f, 1f));
            
            Emit(AudioBank.Paralyzer);
            
            // TODO:
            // Account for other noises
            m_nextMove = AudioBank.Paralyzer[0].length;

            ParanoiaDirector.FreezePlayer();

            m_speed = 15f;

            Vector3 pos = RandomCirclePoint(m_targetTransform.position, 50f);
            SetPosition(pos);
        }

        protected override void EntityUpdate()
        {
            FaceTarget();

            if (m_timer >= m_nextMove)
            {
                m_timer = 0f;
                Move();
                Emit(AudioBank.Paralyzer);
            }
            
            if (DistanceToTarget() <= 5f)
            {
                Disappear();
                ParanoiaDirector.UnfreezePlayer();
            }
        }
    }
}
