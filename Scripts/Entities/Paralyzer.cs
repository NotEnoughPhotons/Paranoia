using System.Reflection.Metadata.Ecma335;
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

        protected override void Awake()
        {
            base.Awake();
            SetInsanity(4.0f);
        }

        public override void EntityStart()
        {
            base.EntityStart();

            Appear();
            UseAudio();
            SetSpatial(0.75f);
            
            Emit(AudioBank.Paralyzer);
            
            // TODO:
            // Account for other noises
            m_nextMove = AudioBank.Paralyzer.length;

            ParanoiaDirector.FreezePlayer();

            m_speed = 1500f;

            Vector3 pos = RandomCirclePoint(m_targetTransform.position, 50f);
            SetPosition(pos);
        }

        protected override void EntityUpdate()
        {
            base.EntityUpdate();
            
            FaceTarget();

            m_timer += Time.deltaTime;

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
