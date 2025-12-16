using MelonLoader;
using NEP.Paranoia.Audio;
using UnityEngine;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class DarkVoice(IntPtr ptr) : Entity(ptr)
    {
        protected override void Awake()
        {
            base.Awake();
            SetInsanity(1.5f);
        }

        public override void EntityStart()
        {
            base.EntityStart();

            Appear();
            UseAudio();
            Emit(AudioBank.DarkVoice);
            SetPosition(AroundTarget(0.35f));
        }
    }
}
