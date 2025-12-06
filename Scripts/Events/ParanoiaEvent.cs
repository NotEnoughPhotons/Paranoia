using UnityEngine;
using Random = UnityEngine.Random;

namespace NEP.Paranoia.Events
{
    public abstract class ParanoiaEvent
    {
        public bool Started => m_started;
        
        private bool m_started;
        
        private float m_insanityLevel;

        private float m_timer = 0f;
        private float m_nextTimeToRun = 0f;
        
        public bool CanStart()
        {
            if (m_started)
                return false;
            
            m_timer += Time.deltaTime;

            if (m_timer < m_nextTimeToRun)
                return false;
            
            m_timer = 0f;
            return true;
        }
        
        public virtual void Start()
        {
            m_started = true;
            m_nextTimeToRun = Random.Range(0f, 15f);
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
