using MelonLoader;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class StaringMan(IntPtr ptr) : Entity(ptr)
    {
        public override void EntityStart()
        {
            base.EntityStart();
            
            m_speed = 0.5f;
            
            SetPosition(AroundTarget(100f));
        }

        protected override void EntityUpdate()
        {
            FaceTarget();
            Move();
            
            if (DistanceToTarget() <= 10f)
                Disappear();
        }
    }
}
