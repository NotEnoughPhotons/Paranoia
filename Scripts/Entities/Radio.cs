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
        
        public override void EntityStart()
        {
            base.EntityStart();
            
            UseAudio();
            SetPitch(Random.Range(0.85f, 1f));
            
            SetPosition(AroundTarget(2f));
            
            AudioClip track = AudioBank.Radio[Random.Range(0, AudioBank.Radio.Length)];
            
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
