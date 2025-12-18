using MelonLoader;

using Random = UnityEngine.Random;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class StaringMan(IntPtr ptr) : Entity(ptr)
    {
        protected override void Awake()
        {
            base.Awake();
            Read("StaringMan");
            Disappear();
        }

        public override void EntityStart()
        {
            base.EntityStart();
            
            Appear();
            SetPosition(AroundTarget(m_radius));
        }

        protected override void EntityUpdate()
        {
            FaceTarget();
            Move();
            
            if (DistanceToTarget() <= m_maxDistance)
                Disappear();
        }
    }
}
