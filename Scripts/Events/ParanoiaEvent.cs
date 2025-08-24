using UnityEngine;

namespace NEP.Paranoia.Events
{
    public abstract class ParanoiaEvent
    {
        public virtual void Start() { }
        
        public virtual void Update() { }

        public virtual void Stop() { }
    }
}
