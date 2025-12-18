using MelonLoader;
using NEP.Paranoia.Audio;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class SjasFace(IntPtr ptr) : Chaser(ptr)
    {
        protected override void Awake()
        {
            base.Awake();
            Read("SjasFace");
            Disappear();
        }

        public override void EntityStart()
        {
            base.EntityStart();
            Emit("Chaser 02");
        }
    }
}
