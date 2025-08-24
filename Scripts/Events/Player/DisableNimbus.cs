using Il2CppSLZ.Marrow;

namespace NEP.Paranoia.Events.Player
{
    public class DisableNimbus : ParanoiaEvent
    {
        public override void Start()
        {
            // Try to get the Nimbus gun in either/both hand(s)
            FlyingGun leftHandNimbus = BoneLib.Player.GetComponentInHand<FlyingGun>(BoneLib.Player.LeftHand);
            FlyingGun rightHandNimbus = BoneLib.Player.GetComponentInHand<FlyingGun>(BoneLib.Player.RightHand);

            if (leftHandNimbus)
            {
                leftHandNimbus.DisableNoClip(BoneLib.Player.LeftHand);
                leftHandNimbus.triggerGrip.ForceDetach();
            }

            if (rightHandNimbus)
            {
                rightHandNimbus.DisableNoClip(BoneLib.Player.RightHand);
                rightHandNimbus.triggerGrip.ForceDetach();
            }
        }
    }
}
