using System;
using MelonLoader;
using NEP.Paranoia.Audio;
using NEP.Paranoia.Managers;

using UnityEngine;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class CeilingMan(IntPtr ptr) : Entity(ptr)
    {
        protected override void Awake()
        {
            base.Awake();
            Read("CeilingMan");
            Disappear();
        }

        public override void EntityStart()
        {
            base.EntityStart();

            Appear();
            
            // It's hard to tell what the "ceiling" is on a lot of levels.
            // For now just spawn him really high up (relative to the player) -
            // and allow him to be seen through walls.
            Vector3 randomPoint = AroundTarget(m_radius);
            Vector3 position = randomPoint + Vector3.up * 100f;
            
            SetPosition(position);
            
            // RenderFirst();
            
            Emit(m_clips);
        }

        protected override void EntityUpdate()
        {
            FaceTarget();
            
            if (BeingLookedAt())
                Disappear();
        }
    }
}
