using NEP.Paranoia.Data;
using NEP.Paranoia.Managers;
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

        private float m_minTime;
        private float m_maxTime;
        private float m_minRng;
        private float m_maxRng;

        public bool CanStart()
        {
            if (m_started)
                return false;

            if (ParanoiaDirector.Insanity < m_insanityLevel)
                return false;
            
            m_timer += Time.deltaTime;

            if (m_timer < m_nextTimeToRun)
                return false;

            m_timer = 0f;

            int random = Random.Range(0, 100);

            if (random < m_minRng || random > m_maxRng)
                return false;

            return true;
        }

        public virtual void Read(string name)
        {
            var definition = DataReader.EventDefinitions[name];

            m_insanityLevel = definition.Insanity;
            m_minTime = definition.Time.MinTime;
            m_maxTime = definition.Time.MaxTime;
            m_minRng = definition.Random.MinRng;
            m_maxRng = definition.Random.MaxRng;
        }
        
        public virtual void Start()
        {
            m_nextTimeToRun = Random.Range(m_minTime, m_maxTime);
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
