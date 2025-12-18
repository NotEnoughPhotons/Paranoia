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
            Read("DarkVoice");
            Disappear();
        }

        public override void EntityStart()
        {
            base.EntityStart();

            Appear();
            Emit(m_clips);
            SetPosition(AroundTarget(m_radius));
        }
    }
}
