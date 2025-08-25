using BoneLib;
using Il2CppSLZ.Marrow.Warehouse;
using NEP.Paranoia.Entities;

using UnityEngine;

using Random = UnityEngine.Random;

namespace NEP.Paranoia.Events.Spawners
{
    public class SpawnEntity : ParanoiaEvent
    {
        public override void Start()
        {
            AssetWarehouse.Instance.TryGetPallet(new Barcode("NEP.Paranoia"), out Pallet pallet);

            if (pallet)
            {
                List<SpawnableCrateReference> entities = new List<SpawnableCrateReference>();

                foreach (var crate in pallet.Crates)
                {
                    foreach (var tag in crate.Tags)
                    {
                        if (tag == "Entity")
                            entities.Add(new SpawnableCrateReference() { Barcode = crate.Barcode });
                    }
                }
                
                var randEnt = entities[Random.Range(0, entities.Count)];
                HelperMethods.SpawnCrate(randEnt, Vector3.zero, Quaternion.identity, Vector3.one, false, (obj) =>
                {
                    Entity entity = obj.GetComponent<Entity>();
                    entity.EntityStart();
                });
            }
        }
    }
}
