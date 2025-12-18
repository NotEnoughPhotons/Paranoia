using MelonLoader;
using NEP.Paranoia.Audio;
using UnityEngine;

using Random = UnityEngine.Random;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class Radio(IntPtr ptr) : Entity(ptr)
    {
        private float m_timer = 0f;
        private float m_duration;

        protected override void Awake()
        {
            base.Awake();
            Read("Radio");
            Disappear();
        }

        public override void EntityStart()
        {
            base.EntityStart();
            
            Appear();
            
            SetPosition(AroundTarget(m_radius));

            AudioClip track = AudioBank.Master[m_clips[Random.RandomRange(0, m_clips.Length - 1)]];
            
            m_duration = track.length;
            
            Emit(track);
            
            FaceTarget();
        }

        protected override void EntityUpdate()
        {
            m_timer += Time.deltaTime;

            if (m_timer >= m_duration)
            {
                m_timer = 0f;
                Disappear();
            }
        }
    }
}
