using MelonLoader;
using NEP.Paranoia.Audio;
using NEP.Paranoia.Entities;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class CryingEntity(IntPtr ptr) : Entity(ptr)
    {
        public override void EntityStart()
        {
            base.EntityStart();
            
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
