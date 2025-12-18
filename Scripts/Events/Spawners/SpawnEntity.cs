using BoneLib;
using Il2CppSLZ.Marrow.Warehouse;
using NEP.Paranoia.Entities;
using NEP.Paranoia.Managers;
using UnityEngine;

using Random = UnityEngine.Random;

namespace NEP.Paranoia.Events.Spawners
{
    public class SpawnEntity : ParanoiaEvent
    {
        public override void Start()
        {
            base.Start();
            Read("SpawnEntity");

            if (ParanoiaDirector.Entities.Count == 0)
            {
                base.Stop();
                return;
            }

            Entity entity = ParanoiaDirector.Entities[Random.Range(0, ParanoiaDirector.Entities.Count - 1)];

            if (ParanoiaDirector.Insanity >= entity.Insanity && !entity.Active)
            {
                ParanoiaDirector.Spawn(entity);
            } 

            base.Stop();
        }
    }
}
