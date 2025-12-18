using MelonLoader;
using NEP.Paranoia.Managers;
using Random = UnityEngine.Random;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class ShadowPerson(IntPtr ptr) : Entity(ptr)
    {
        private bool m_becomeChaser = false;

        protected override void Awake()
        {
            base.Awake();
            Read("ShadowPerson");
            Disappear();
        }

        public override void EntityStart()
        {
            base.EntityStart();

            Appear();
            SetPosition(AroundTarget(m_radius));

            // 50% chance to become a chaser
            m_becomeChaser = Random.Range(0f, 100f) % 2 == 0;
        }

        protected override void EntityUpdate()
        {
            FaceTarget();

            if (m_becomeChaser)
            {
                if (DistanceToTarget() <= m_maxDistance)
                    Disappear();

                Move();
                
                return;
            }
            
            if (DistanceToTarget() <= m_maxDistance)
                Disappear();
        }
    }
}
