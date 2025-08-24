using Il2CppSLZ.Marrow.Interaction;

using NEP.Paranoia.Extensions;

using UnityEngine;

using Random = UnityEngine.Random;

namespace NEP.Paranoia.Events.World
{
    public class FlingRandomObject : ParanoiaEvent
    {
        public override void Start()
        {
            MarrowBody[] bodies = MarrowBody.Cache.ToArray();

            Transform player = BoneLib.Player.Head;

            Rigidbody randomRB = bodies[Random.Range(0, bodies.Length)]._rigidbody;

            randomRB.AddForce((player.position - randomRB.transform.position) * Random.Range(100f, 200f), ForceMode.Impulse);
        }
    }
}
