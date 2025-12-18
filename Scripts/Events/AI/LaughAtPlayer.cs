using Il2CppPuppetMasta;
using Il2CppSLZ.Marrow.AI;

using System.Collections;
using UnityEngine;
using MelonLoader;
using Random = UnityEngine.Random;

namespace NEP.Paranoia.Events.AI
{
    public class LaughAtPlayer : ParanoiaEvent
    {
        private List<SubBehaviourFaceanim> m_speakers;

        public override void Start()
        {
            m_speakers = new List<SubBehaviourFaceanim>();

            base.Start();
            Read("AILaughAtPlayer");

            // SetInsanityLevel(2f);
            
            AIBrain[] brains = Utilities.Util.FindAIBrains();

            foreach (AIBrain brain in brains)
            {
                BehaviourPowerLegs powerLegs = brain?.behaviour.TryCast<BehaviourPowerLegs>();

                if (!powerLegs || powerLegs.faceAnim == null)
                    return;

                m_speakers.Add(powerLegs.faceAnim);
            }

            MelonCoroutines.Start(LaughRoutine());
        }

        private IEnumerator LaughRoutine()
        {
            int count = Random.Range(0, 15);

            for (int i = 0; i < count; i++)
            {
                foreach (var face in m_speakers)
                    face.Attack1(Random.Range(0, 2));

                yield return new WaitForSeconds(1.75f);
            }

            base.Stop();
            yield return null;
        }
    }
}
