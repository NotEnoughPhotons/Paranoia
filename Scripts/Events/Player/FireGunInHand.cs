using Il2CppSLZ.Marrow;

namespace NEP.Paranoia.Events.Player
{
    public class FireGunInHand : ParanoiaEvent
    {
        public override void Start()
        {
            base.Start();

            Gun leftGun = BoneLib.Player.GetComponentInHand<Gun>(BoneLib.Player.LeftHand);
            Gun rightGun = BoneLib.Player.GetComponentInHand<Gun>(BoneLib.Player.RightHand);

            if (leftGun)
                leftGun.Fire();

            if (rightGun)
                rightGun.Fire();

            base.Stop();
        }
    }
}
