using MelonLoader;
using NEP.Paranoia.Audio;
using UnityEngine;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class Chaser(IntPtr ptr) : Entity(ptr)
    {
        protected override void Awake()
        {
            base.Awake();
            Read("Chaser");
            Disappear();
        }

        public override void EntityStart()
        {
            base.EntityStart();

            Appear();
            
            FaceTarget();
            Emit(m_clips);
            
            SetPosition(AroundTarget(m_radius));
        }

        protected override void EntityUpdate()
        {
            FaceTarget();
            Move();
            
            if (DistanceToTarget() <= 1f)
                Disappear();
        }
    }
}
