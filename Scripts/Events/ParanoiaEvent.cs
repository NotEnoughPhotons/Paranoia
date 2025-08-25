using UnityEngine;

namespace NEP.Paranoia.Events
{
    public abstract class ParanoiaEvent
    {
        public bool Started => m_started;
        
        private bool m_started;
        private float m_insanityLevel;

        public virtual void Start()
        {
            m_started = true;
        }

        public virtual void Update()
        {
            
        }

        public virtual void Stop()
        {
            m_started = false;
        }

        public void SetInsanityLevel(float insanityLevel)
        {
            m_insanityLevel = insanityLevel;
        }
    }
}
