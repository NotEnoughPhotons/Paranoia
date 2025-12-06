using Il2CppSLZ.Marrow.PuppetMasta;

using UnityEngine;

using NEP.Paranoia.Utilities;

namespace NEP.Paranoia.Events.AI
{
    public class MoveAIToPlayer : ParanoiaEvent
    {
        public override void Start()
        {
            BehaviourBaseNav[] navs;
            Util.FindAIBrains(out navs);
            
            if(navs == null)
                return;

            foreach (BehaviourBaseNav nav in navs)
                nav.SetPath(BoneLib.Player.Head.position);
        }
    }
}
