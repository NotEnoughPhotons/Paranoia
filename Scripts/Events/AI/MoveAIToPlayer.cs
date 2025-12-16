using Il2CppPuppetMasta;
using Il2CppSLZ.Marrow.AI;
using Il2CppSLZ.Marrow.PuppetMasta;

using System.Collections;

using NEP.Paranoia.Utilities;
using UnityEngine;

using Random = UnityEngine.Random;

namespace NEP.Paranoia.Events.AI
{
    public class MoveAIToPlayer : ParanoiaEvent
    {
        private List<BehaviourBaseNav> m_navs;

        private float m_timer;
        private float m_maxTime;

        public override void Start()
        {
            m_navs = new List<BehaviourBaseNav>();

            base.Start();

            // SetInsanityLevel(2f);

            AIBrain[] brains = Util.FindAIBrains();

            foreach (AIBrain brain in brains)
            {
                BehaviourPowerLegs powerLegs = brain?.behaviour.TryCast<BehaviourPowerLegs>();

                if (!powerLegs)
                    return;

                m_navs.Add(powerLegs);
            }

            m_timer = 0f;
            m_maxTime = Random.Range(5f, 20f);
        }

        public override void Update()
        {
            m_timer += Time.unscaledTime;

            foreach (var nav in m_navs)
            {
                nav.mentalState = BehaviourBaseNav.MentalState.MindControlled;
                nav.SetPath(BoneLib.Player.Head.position);
            }

            if (m_timer > m_maxTime)
            {
                base.Stop();
            }
        }
    }
}
