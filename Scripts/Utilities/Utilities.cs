using System.Reflection;

using Il2CppInterop.Runtime.InteropTypes.Arrays;

using UnityEngine;

using Il2CppSLZ.Marrow;
using Il2CppSLZ.Marrow.AI;
using Il2CppSLZ.Marrow.Interaction;
using Il2CppSLZ.Marrow.PuppetMasta;

using BoneLib;

using Object = UnityEngine.Object;

namespace NEP.Paranoia.Utilities
{
    public static class Utilities
    {
        public static Assembly GetAssembly(string assemblyName)
        {
            return null;
        }

        public static Il2CppArrayBase<AIBrain> FindAIBrains()
        {
            return Object.FindObjectsOfType<AIBrain>();
        }

        public static AIBrain[] FindAIBrains(out BehaviourBaseNav[] navs)
        {
            AIBrain[] result = Object.FindObjectsOfType<AIBrain>();
            navs = FindBaseNavs(result);

            return result;
        }

        public static BehaviourBaseNav[] FindBaseNavs(AIBrain[] brains)
        {
            List<BehaviourBaseNav> baseNavs = new List<BehaviourBaseNav>();

            brains.ToList().ForEach((brain) =>
            {
                baseNavs.Add(brain?.behaviour);
            });

            return baseNavs.ToArray();
        }

        public static Gun GetGunInHand(Handedness hand)
        {
            PhysicsRig physRig = Player.PhysicsRig;

            Hand target = hand == Handedness.RIGHT ? physRig.rightHand : physRig.leftHand;

            if(target == null) { return null; }

            Gun gun = Player.GetComponentInHand<Gun>(target);

            if(gun == null) { return null; }

            return gun;
        }
    }
}
