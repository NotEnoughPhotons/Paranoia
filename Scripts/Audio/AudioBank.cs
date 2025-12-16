using Il2CppSLZ.Marrow.Warehouse;
using UnityEngine;

namespace NEP.Paranoia.Audio
{
    public static class AudioBank
    {
        public static Dictionary<string, AudioClip> Master;
        
        public static AudioClip[] Ambience;
        public static AudioClip[] Terror;
        public static AudioClip[] Radio;

        public static AudioClip[] Crying;
        public static AudioClip[] Chaser;
        public static AudioClip CeilingMan;
        public static AudioClip[] DarkVoice;
        public static AudioClip[] GrayMan;

        public static AudioClip Paralyzer;

        public static AudioClip RingerRingExternal;
        public static AudioClip RingerCallRing;
        public static AudioClip RingerHangedUp;
        public static AudioClip[] RingerCalls;
        
        public static AudioClip Sjas;

        private static List<AudioClip> m_ambience;
        private static List<AudioClip> m_ringerCalls;
        private static List<AudioClip> m_radio;
        private static List<AudioClip> m_chaser;
        private static List<AudioClip> m_darkVoice;

        internal static void Initialize()
        {
            m_ambience = new List<AudioClip>();
            m_ringerCalls = new List<AudioClip>();
            m_radio = new List<AudioClip>();
            m_chaser = new List<AudioClip>();
            m_darkVoice = new List<AudioClip>();
            Master = new Dictionary<string, AudioClip>();

            if (!AssetWarehouse.Instance.TryGetPallet(new Barcode("NEP.Paranoia"), out Pallet pallet))
                throw new NullReferenceException("The Paranoia pallet is MISSING.");
            
            foreach (var datacard in pallet.DataCards)
            {
                MonoDisc md = datacard.TryCast<MonoDisc>();

                if (!md)
                    continue;
                
                md.AudioClip.LoadAsset(new Action<AudioClip>((clip) => OnClipLoaded(clip, md)));
            }
            
        }

        private static void OnClipLoaded(AudioClip clip, MonoDisc disc)
        {
            // TODO: find a better way to load assets 'cause this STINKS
            clip.hideFlags = HideFlags.DontUnloadUnusedAsset;

            if (disc.Description == "ambient" || disc.Description == "terror")
            {
                m_ambience.Add(clip);
                Ambience = m_ambience.ToArray();
            }

            if (disc.Description == "darkvoice")
            {
                m_darkVoice.Add(clip);
                DarkVoice = m_darkVoice.ToArray();
            }
            
            if (disc.Description == "phone_extern_ring")
                RingerRingExternal = clip;

            if (disc.Description == "phone_attempt_ring")
                RingerCallRing = clip;

            if (disc.Description == "phone_dials")
            {
                m_ringerCalls.Add(clip);
                RingerCalls = m_ringerCalls.ToArray();
            }

            if (disc.Description == "radio_tunes")
            {
                m_radio.Add(clip);
                Radio = m_radio.ToArray();
            }

            if (disc.Description == "ceiling_man")
                CeilingMan = clip;
            
            if (disc.Description == "paralyzer_cry")
                Paralyzer = clip;
            
            if (disc.Description == "phone_call_end")
                RingerHangedUp = clip;

            if (disc.Description == "chaser")
            {
                m_chaser.Add(clip);
                Chaser = m_chaser.ToArray();
            }
                        
            Master.Add(disc.Title, clip);
        }
    }
}