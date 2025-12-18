using MelonLoader;
using NEP.Paranoia.Audio;
using UnityEngine;

using NEP.Paranoia.Managers;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class Terror(IntPtr ptr) : Entity(ptr)
    {
        protected override void Awake()
        {
            base.Awake();
            Read("Terror");
            Disappear();
        }

        public override void EntityStart()
        {
            Appear();
            UseAudio();
            SetSpatial(0f);
            Emit(AudioBank.Terror);
        }
    }
}