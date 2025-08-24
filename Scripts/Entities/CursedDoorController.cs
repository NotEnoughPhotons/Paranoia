using MelonLoader;
using UnityEngine;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class CursedDoorController(IntPtr ptr) : Entity(ptr)
    {
        // TODO:
        // Redo this whole entity
        
        public AudioSource source;
        public Transform hingeTransform;
        public Material faceMaterial;

        private bool playedOnce = false;

        private float m_timer = 0f;
        private float m_delay = 4.75f;

        public override void EntityStart()
        {
            /*base.Awake();

            ReadValuesFromJSON(System.IO.File.ReadAllText(baseJsonPath + "CursedDoor.json"));

            hingeTransform = transform.Find("GameObject/scaler/door_Boneworks/hinge");
            source = transform.Find("GameObject/scaler/source").GetComponent<AudioSource>();

            trigger = GetComponentInChildren<PlayerTrigger>();

            trigger.TriggerEnterEvent.AddListener(new System.Action(() =>
            {
                GameManager.endRoom.SetActive(true);
                Utilities.GetRigManager().GetComponent<RigManager>().Teleport(MapUtilities.endRoomPlayerSpawn.position, true);
                GameManager.hStaringMan.gameObject.SetActive(true);
                GameManager.hStaringMan.transform.position = MapUtilities.endRoomEyesSpawn.position;
                GameManager.hStaringMan.moveSpeed = 0.15f;
                GameManager.hStaringMan.disableDistance = 0.25f;

                MelonLoader.MelonCoroutines.Start(CoEndRoutine());
            }));

            MelonLoader.MelonCoroutines.Start(CoHideRoutine());*/
        }

        protected override void EntityUpdate()
        {
            /*base.Update();

            if(Vector3.Distance(hingeTransform.position, ParanoiaUtilities.Utilities.FindPlayer().transform.position) < 5f)
            {
                Vector3 target = -Vector3.up * 125f;
                hingeTransform.localRotation = Quaternion.Lerp(hingeTransform.localRotation, Quaternion.Euler(target), 0.15f * Time.deltaTime);

                if (!playedOnce)
                {
                    m_timer += Time.deltaTime;

                    if(m_timer >= m_delay)
                    {
                        source.PlayOneShot(Paranoia.instance.doorOpenSounds[0]);
                        playedOnce = true;
                        m_timer = 0f;
                    }
                }
            }*/
        }
    }
}
