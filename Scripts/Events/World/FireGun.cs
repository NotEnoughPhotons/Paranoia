using Il2CppSLZ.Marrow;
using NEP.Paranoia.Extensions;

namespace NEP.Paranoia.Events.World
{
    public class FireGun : ParanoiaEvent
    {
        public override void Start()
        {
            // Find a random gun in the world to fire
            Gun[] guns = Gun.Cache.ToArray();

            if(guns == null) { return; }

            guns[UnityEngine.Random.Range(0, guns.Length)]?.Fire();
        }
    }
}
