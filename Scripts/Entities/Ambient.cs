using MelonLoader;
using NEP.Paranoia.Audio;
using UnityEngine;

using Random = UnityEngine.Random;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class Ambient(IntPtr ptr) : Entity(ptr)
    {
        private float m_audioTimer = 0f;
        private float m_audioDuration = 0f;

        protected override void Awake()
        {
            base.Awake();
            SetInsanity(1.0f);
        }

        public override void EntityStart()
        {
            base.EntityStart();

            Appear();
            UseAudio();
            SetSpatial(0f);

            AudioClip clip = AudioBank.Ambience[Random.Range(0, AudioBank.Ambience.Length - 1)];

            m_audioDuration = clip.length;

            Emit(clip);
        }

        protected override void EntityUpdate()
        {
            m_audioTimer += Time.unscaledDeltaTime;

            if (m_audioTimer >= m_audioDuration)
                Disappear();
        }

        public override void EntityStop()
        {
            m_audioTimer = 0f;
            m_audioDuration = 0f;
        }
    }
}
