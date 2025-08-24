using UnityEngine;

namespace NEP.Paranoia.Events.World
{
    public class CloseGame : ParanoiaEvent
    {
        public override void Start()
        {
            Application.Quit();
        }
    }
}
