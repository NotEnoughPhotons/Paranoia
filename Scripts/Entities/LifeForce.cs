using MelonLoader;
using UnityEngine;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class LifeForce(IntPtr ptr) : Entity(ptr)
    {
        private SphereCollider m_collider;
        private float m_lifeForceRadius = 2f;

        protected override void Awake()
        {
            base.Awake();
            Read("LifeForce");
            Disappear();
        }

        public override void EntityStart()
        {
            Appear();
            m_collider = GetComponent<SphereCollider>();
            m_collider.radius = m_lifeForceRadius;
        }
        
        protected override void EntityUpdate()
        {
            FaceTarget();
            Move();
            
            if (DistanceToTarget() < 1f)
                Disappear();
        }
    }
}
