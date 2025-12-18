using Il2CppSLZ.Marrow.Audio;
using MelonLoader;
using NEP.Paranoia.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine;

using Random = UnityEngine.Random;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class Deafener(IntPtr ptr) : Entity(ptr)
    {
        private Audio3dManager m_audioManager;

        private float m_lpf;
        private float m_lpfBase;
        private float m_lpfFinal;
        private float m_lerp;

        private float m_minTimeDeaf;
        private float m_maxTimeDeaf;

        private IEnumerator m_routine;

        protected override void Awake()
        {
            base.Awake();
            Read("Deafen");
            Emit(m_clips);

            m_audioManager = FindObjectOfType<Audio3dManager>();
        }

        protected override void Read(string entity)
        {
            base.Read(entity);

            JToken token = ReadJToken(entity);
            JToken deafen = token["deafen"];

            m_lpfBase = deafen["lpf_base"].Value<float>();
            m_lpfFinal = deafen["lpf_final"].Value<float>();
            m_lerp = deafen["lerp"].Value<float>();

            m_minTimeDeaf = deafen["min_time_deaf"].Value<float>();
            m_maxTimeDeaf = deafen["max_time_deaf"].Value<float>();
        }

        public override void EntityStart()
        {
            base.EntityStart();
            m_source.volume = 0f;
            m_lpf = m_lpfBase;

            if (m_routine != null)
            {
                MelonCoroutines.Stop(m_routine);
                m_routine = null;
            }
            else
                m_routine = MelonCoroutines.Start(VolumeRoutine()) as IEnumerator;
        }

        private IEnumerator VolumeRoutine()
        {
            float lerp = Time.deltaTime * m_lerp;

            while (m_source.volume < 1f)
            {
                m_source.volume = Mathf.Lerp(m_source.volume, 1f, lerp);
                m_lpf = Mathf.Lerp(m_lpf, m_lpfFinal, lerp);
                m_audioManager.SetLowPassFilter(m_lpf);
                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(m_minTimeDeaf, m_maxTimeDeaf));

            while (m_source.volume > 0f)
            {
                m_source.volume = Mathf.Lerp(m_source.volume, 0f, lerp);
                m_lpf = Mathf.Lerp(m_lpf, m_lpfBase, lerp);
                m_audioManager.SetLowPassFilter(m_lpf);
                yield return null;
            }

            yield return null;
        }
    }
}
