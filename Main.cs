using Il2CppSLZ.Marrow.Warehouse;
using MelonLoader;
using NEP.Paranoia.Audio;
using NEP.Paranoia.Managers;

namespace NEP.Paranoia
{
    public static class BuildInfo
    {
        public const string Name = "paranoia";
        public const string Description = "I told you not to.";
        public const string Author = "Not Enough Photons, adamdev, Mabel";
        public const string Company = "Not Enough Photons";
        public const string Version = "4.0.0";
        public const string DownloadLink = null;
    }

    public class Paranoia : MelonMod
    {
        public static Paranoia Instance { get; private set; }

        public static MelonLogger.Instance Logger { get; private set; }

        public override void OnInitializeMelon()
        {
            Instance = this;
            Logger = new MelonLogger.Instance("paranoia");
            
            ParanoiaDirector.Initialize();

            AssetWarehouse._onReady += new Action(() =>
            {
                AudioBank.Initialize();
            });
        }

        public override void OnUpdate()
        {
            ParanoiaDirector.Update();
        }
    }
}