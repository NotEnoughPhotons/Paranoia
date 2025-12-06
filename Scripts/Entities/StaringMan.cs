using MelonLoader;

using Random = UnityEngine.Random;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class StaringMan(IntPtr ptr) : Entity(ptr)
    {
        public override void EntityStart()
        {
            base.EntityStart();

            m_speed = Random.Range(1f, 5f);
            
            Appear();
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
