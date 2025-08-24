using MelonLoader;
using NEP.Paranoia.Managers;
using Random = UnityEngine.Random;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class ShadowPerson(IntPtr ptr) : Entity(ptr)
    {
        private bool m_becomeChaser = false;
        
        public override void EntityStart()
        {
            SetPosition(AroundTarget(50f));
            m_speed = 50f;

            // 50% chance to become a chaser
            m_becomeChaser = Random.Range(0f, 100f) % 2 == 0;
        }

        protected override void EntityUpdate()
        {
            FaceTarget();

            if (m_becomeChaser)
            {
                if (DistanceToTarget() <= 25f)
                    Disappear();

                Move();
                
                return;
            }
            
            if (DistanceToTarget() <= 25f)
                Disappear();
        }
    }
}
