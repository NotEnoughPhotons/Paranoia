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

            SetInsanity(1.75f);
        }

        public override void EntityStart()
        {
            base.EntityStart();

            Appear();
            UseAudio();
            
            SetVolume(0.75f);
            SetLooping(true);
            SetSpatial(0.925f);
            
            FaceTarget();
            Emit(AudioBank.Chaser);

            m_speed = 35f;
            
            SetPosition(AroundTarget(250f));
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
