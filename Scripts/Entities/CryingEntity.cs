using MelonLoader;
using NEP.Paranoia.Audio;
using NEP.Paranoia.Entities;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class CryingEntity(IntPtr ptr) : Entity(ptr)
    {
        protected override void Awake()
        {
            base.Awake();
            SetInsanity(3.0f);
        }

        public override void EntityStart()
        {
            base.EntityStart();


            Appear();
            UseAudio();
            SetLooping(true);
            Emit(AudioBank.Crying);
            
            SetPosition(AroundTarget(50f));
        }

        protected override void EntityUpdate()
        {
            if (DistanceToTarget() <= 10f)
                Disappear();
        }
    }
}
