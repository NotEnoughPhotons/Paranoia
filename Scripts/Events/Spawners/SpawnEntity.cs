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

            Entity entity = ParanoiaDirector.Entities[Random.Range(0, ParanoiaDirector.Entities.Count - 1)];
            ParanoiaDirector.Spawn(entity);
            
            base.Stop();
        }
    }
}
