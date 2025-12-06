using Il2CppPuppetMasta;
using Il2CppSLZ.Marrow.AI;

using Random = UnityEngine.Random;

namespace NEP.Paranoia.Events.AI
{
    public class LaughAtPlayer : ParanoiaEvent
    {
        public override void Start()
        {
            SetInsanityLevel(2f);
            
            AIBrain[] brains = Utilities.Util.FindAIBrains();

            foreach(AIBrain brain in brains)
            {
                if(brain == null)
                {
                    continue;
                }

                BehaviourPowerLegs powerLegs = brain?.behaviour.TryCast<BehaviourPowerLegs>();

                if(!powerLegs)
                {
                    return;
                }

                powerLegs?.faceAnim?.Attack1(Random.Range(1, 3));
            }
        }
    }
}
