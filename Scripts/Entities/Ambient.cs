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
            Read("Ambient");
            Disappear();
        }

        public override void EntityStart()
        {
            base.EntityStart();

            Appear();

            string clipName = m_clips[Random.Range(0, m_clips.Length - 1)];
            AudioClip clip = AudioBank.Master[clipName];

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
