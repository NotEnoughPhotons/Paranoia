using System.Collections;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;
using UnityEngine.Video;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class Monitor(IntPtr ptr) : Entity(ptr)
    {
        public List<VideoClip> clips;

        private VideoPlayer player;

        private void Awake()
        {
            if(GetComponent<VideoPlayer>() != null)
            {
                player = GetComponent<VideoPlayer>();
            }
        }

        private void OnEnable()
        {
            if(clips == null) { return; }
            if(player == null) { return; }

            //player.clip = clips[ParanoiaGameManager.instance.insanity];
            MelonLoader.MelonCoroutines.Start(CoHideMonitor());
        }

        private IEnumerator CoHideMonitor()
        {
            yield return new WaitForSeconds((float)player.clip.length + 0.5f);

            gameObject.SetActive(false);
        }
    }

}