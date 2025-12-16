using Il2CppSLZ.Marrow;
using MelonLoader;
using UnityEngine;

using Random = UnityEngine.Random;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class Bridge(IntPtr ptr) : Entity(ptr)
    {
        private GameObject m_blankbox;
        private GameObject m_mesh;
        private GameObject m_indoorTrigger;
        private GameObject m_outdoorTrigger;

        protected override void Awake()
        {
            base.Awake();

            m_blankbox = transform.Find("Blankbox").gameObject;
            m_mesh = transform.Find("Mesh").gameObject;
            m_indoorTrigger = transform.Find("IndoorTrigger").gameObject;
            m_outdoorTrigger = transform.Find("OutdoorTrigger").gameObject;

            m_blankbox.SetActive(false);
        }

        public override void EntityStart()
        {
            base.EntityStart();

            SetPosition(AroundTarget(4f, 0f));
        }

        protected override void EntityUpdate()
        {
            base.EntityUpdate();
        }

        public override void EntityStop()
        {
            base.EntityStop();
        }
        
        private void TeleportToBlankbox()
        {
            RigManager rig = BoneLib.Player.RigManager;

            SetPosition(AroundTarget(1000f, Random.Range(100f, 1000f)));

            rig.Teleport(transform.position, zeroVelocity: true);

            m_mesh.SetActive(false);
            m_indoorTrigger.SetActive(false);
            m_outdoorTrigger.SetActive(false);
            m_blankbox.SetActive(true);
        }
    }
}
