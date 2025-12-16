using Il2CppRealisticEyeMovements;
using UnityEngine;

using Random = UnityEngine.Random;

namespace NEP.Paranoia.Events.AI
{
    public class CorruptNPC : ParanoiaEvent
    {
        public sealed class Target
        {
            public Target(GameObject npcRoot)
            {
                if (npcRoot == null)
                {
                    return;
                }

                m_physicsControl = npcRoot.transform.GetChild(1).gameObject;
                m_head = m_physicsControl.transform.Find("Root_M/Spine_M/Chest_M/Head_M");
                m_geoGroup = npcRoot.transform.Find("brettEnemy@neutral/geoGrp");
                m_jaw = m_head.Find("Jaw_M");
                m_headTop = m_head.Find("skn_headTop");
            }

            private GameObject m_physicsControl;
            private Transform m_jaw;
            private Transform m_head;
            private Transform m_headTop;
            private Transform m_geoGroup;

            public void Corrupt()
            {
                // Disable physics controller to freeze the NPC
                m_physicsControl.SetActive(false);

                // Distort head
                m_head.localPosition = new Vector3(m_head.localPosition.x, -0.1125f, m_head.localPosition.z);
                m_head.localScale = new Vector3(1.4f, 0.1f, 1f);
                m_head.localScale += Vector3.one;

                // Disable eyes
                m_headTop.GetChild(0).localScale = Vector3.zero;
                m_headTop.GetChild(1).localScale = Vector3.zero;

                // Distort the eyelids
                m_headTop.localScale = new Vector3(-0.5f, 1f, 1f);
                m_headTop.localPosition = Vector3.zero;

                // Extend jaw
                m_jaw.localPosition = new Vector3(0.1011f, m_jaw.localPosition.y, m_jaw.localPosition.z);

                // Disable everything except the head mesh
                m_geoGroup.GetChild(0).gameObject.SetActive(false);
                m_geoGroup.GetChild(1).gameObject.SetActive(false);
                m_geoGroup.GetChild(2).gameObject.SetActive(false);
                m_geoGroup.GetChild(3).gameObject.SetActive(false);
                m_geoGroup.GetChild(4).gameObject.SetActive(false);
            }

            public void FacePlayer()
            {
                Transform player = BoneLib.Player.Chest;
                m_head.LookAt(player);
            }
        }

        private Target m_target;

        public override void Setup()
        {
            base.Setup();
        }

        public override void Start()
        {
            base.Start();

            if (m_target != null)
            {
                base.Stop();
                return;
            }

            // Pick a random NPC
            // NOTE: Doesn't factor in distance from player
            var allNPCs = Utilities.Util.FindAIBrains();

            if (allNPCs.Length == 0)
            {
                base.Stop();
                return;
            }

            var npc = allNPCs[Random.Range(0, allNPCs.Length)];

            m_target = new Target(npc.gameObject);
            m_target.Corrupt();

            base.Stop();
        }

        public override void Update()
        {
            if (m_target == null)
            {
                base.Stop();
                return;
            }

            m_target.FacePlayer();
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}