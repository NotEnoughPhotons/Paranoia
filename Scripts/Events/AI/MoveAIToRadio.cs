using UnityEngine;
using NEP.Paranoia.Managers;
using NEP.Paranoia.Utilities;
using Il2CppSLZ.Marrow.PuppetMasta;
using NEP.Paranoia.Entities;

namespace NEP.Paranoia.Events.AI
{
    public class MoveAIToRadio : ParanoiaEvent
    {
        public override void Start()
        {
            base.Start();

            Radio radio = ParanoiaDirector.GetEntity<Radio>();

            if (!radio)
                return;

            if (!radio.Active)
                return;

            var navs = Util.FindBaseNavs(Util.FindAIBrains());

            if (navs.Length == 0)
                return;

            foreach (var nav in navs)
            {
                nav.mentalState = BehaviourBaseNav.MentalState.MindControlled;
                nav.SetPath(radio.transform.position);
            }
        }
    }
}
