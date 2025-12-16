using MelonLoader;

using UnityEngine;
using UnityEngine.Video;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class Monitor(IntPtr ptr) : Entity(ptr)
    {
        private VideoPlayer m_videoPlayer;
        private List<VideoClip> m_clips;
        private RenderTexture m_renderTexture;

        protected override void Awake()
        {
            m_renderTexture = new RenderTexture(512, 512, 0);
            m_renderTexture.format = RenderTextureFormat.ARGBFloat;
            m_renderTexture.Create();

            m_videoPlayer = GetComponent<VideoPlayer>();
        }
    }
}