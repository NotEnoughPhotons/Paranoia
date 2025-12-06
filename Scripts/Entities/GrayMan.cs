using NEP.Paranoia.Audio;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NEP.Paranoia.Entities
{
    public class GrayMan(IntPtr ptr) : Entity(ptr)
    {
        private float m_timer = 0f;
        private float m_duration = 0f;

        private AudioClip m_clip;
        
        public override void EntityStart()
        {
            Appear();
            UseAudio();
            SetLooping(true);

            m_clip = AudioBank.GrayMan[Random.Range(0, AudioBank.GrayMan.Length)];
            
            m_duration = m_clip.length;
            Emit(m_clip);
        }

        protected override void EntityUpdate()
        {
            m_timer += Time.deltaTime;

            if (m_timer >= m_duration)
            {
                m_timer = 0f;
                Move();
                Emit(m_clip);
            }
            
            if (DistanceToTarget() <= 5f)
                Disappear();
        }
    }
}
