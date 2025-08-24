using UnityEngine;

namespace NEP.Paranoia.Audio
{
    public static class AudioBank
    {
        public static Dictionary<string, AudioClip[]> Banks;
        
        public static AudioClip[] Ambience;
        public static AudioClip[] Terror;
        public static AudioClip[] Radio;

        public static AudioClip[] Crying;
        public static AudioClip[] Chaser;
        public static AudioClip[] CeilingMan;
        public static AudioClip[] DarkVoice;
        public static AudioClip[] GrayMan;

        public static AudioClip[] Ringer;
        public static AudioClip[] Paralyzer;

        public static AudioClip Sjas;

        internal static void Initialize()
        {
            Banks = new Dictionary<string, AudioClip[]>();
        }
    }
}