using MelonLoader;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class StaringMan(IntPtr ptr) : Entity(ptr)
    {
        public override void EntityStart()
        {
            m_speed = 0.5f;
        }

        protected override void EntityUpdate()
        {
            FaceTarget();
            
            if (DistanceToTarget() <= 20f)
                Disappear();
        }
    }
}
