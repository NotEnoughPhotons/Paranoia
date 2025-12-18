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
            Read("Paralyzer");
            Disappear();
        }

        public override void EntityStart()
        {
            base.EntityStart();

            Appear();
            
            Emit(m_clips);

            // TODO:
            // Account for other noises
            string clipName = m_clips[Random.Range(0, m_clips.Length - 1)];
            m_nextMove = AudioBank.Master[clipName].length;

            ParanoiaDirector.FreezePlayer();

            Vector3 pos = RandomSpherePoint(m_targetTransform.position, m_radius);
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
                Emit(m_clips);
            }
            
            if (DistanceToTarget() <= 5f)
            {
                Disappear();
                ParanoiaDirector.UnfreezePlayer();
            }
        }
    }
}
