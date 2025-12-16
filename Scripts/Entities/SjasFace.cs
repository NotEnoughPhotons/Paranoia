using MelonLoader;
using NEP.Paranoia.Audio;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class SjasFace(IntPtr ptr) : Chaser(ptr)
    {
        public override void EntityStart()
        {
            base.EntityStart();
            Emit("Chaser 02");
        }
    }
}
