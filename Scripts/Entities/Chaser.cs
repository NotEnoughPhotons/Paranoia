using MelonLoader;
using NEP.Paranoia.Audio;
using UnityEngine;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class Chaser(IntPtr ptr) : Entity(ptr)
    {
        public override void EntityStart()
        {
            UseAudio();
            
            SetVolume(0.75f);
            SetLooping(true);
            SetSpatial(1f);
            
            FaceTarget();
            Emit(AudioBank.Chaser);

            m_speed = 50f;
            
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
