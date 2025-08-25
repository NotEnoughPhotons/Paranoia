using MelonLoader;
using NEP.Paranoia.Audio;
using UnityEngine;

using NEP.Paranoia.Managers;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class Ambient(IntPtr ptr) : Entity(ptr)
    {
        public override void EntityStart()
        {
            base.EntityStart();
            
            UseAudio();
            SetSpatial(0f);
            Emit(AudioBank.Ambience);
        }
    }
}
