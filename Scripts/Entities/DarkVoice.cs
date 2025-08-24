using MelonLoader;
using NEP.Paranoia.Audio;
using UnityEngine;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class DarkVoice(IntPtr ptr) : Entity(ptr)
    {
        public override void EntityStart()
        {
            UseAudio();
            Emit(AudioBank.DarkVoice);
            SetPosition(AroundTarget(0.1f));
        }
    }
}
