using Il2CppSLZ.Marrow.AI;
using Il2CppSLZ.Marrow.PuppetMasta;

using UnityEngine;

namespace NEP.Paranoia.Events.AI
{
    public class KillAI : ParanoiaEvent
    {
        public override void Start()
        {
            AIBrain[] brains = Utilities.Utilities.FindAIBrains();

            if(brains == null) { return; }

            foreach(AIBrain brain in brains)
            {
                PuppetMaster puppetMaster = brain.puppetMaster;
                SubBehaviourHealth health = brain.behaviour.health;

                health.Kill();
                puppetMaster.Kill();
            }
        }
    }
}
