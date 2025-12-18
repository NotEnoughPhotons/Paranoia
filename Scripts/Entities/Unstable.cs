using MelonLoader;
using NEP.Paranoia.Audio;
using UnityEngine;

using NEP.Paranoia.Managers;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class Unstable(IntPtr ptr) : Entity(ptr)
    {
        private enum ArmState
        {
            Lowered,
            Raised
        }

        public enum UnstableState
        {
            Idle,
            Roaming,
            Angry
        }

        private Transform m_armsLowered;
        private Transform m_armsRaised;
        private Transform m_tempTarget;

        private UnstableState m_state;

        private Vector3 m_originalArmScale;
        private float m_armReach;

        protected override void Awake()
        {
            base.Awake();
            SetInsanity(3.5f);
            Disappear();
        }

        public override void EntityStart()
        {
            base.EntityStart();

            Appear();

            m_armsLowered = transform.Find("ArmsDown");
            m_armsRaised = transform.Find("ArmsUp");

            m_state = UnstableState.Idle;

            m_originalArmScale = m_armsRaised.transform.localScale;
        }

        protected override void EntityUpdate()
        {
            if (m_state == UnstableState.Idle)
                IdleState();
            else if (m_state == UnstableState.Roaming)
                RoamingState();
            else if (m_state == UnstableState.Angry)
                AngryState();
        }

        public override void EntityStop()
        {
            m_armReach = 0f;
            m_armsRaised?.transform.localScale = m_originalArmScale;
        }

        private bool ArmsCloseEnough()
        {
            print($"Distance: {DistanceToTarget()}");
            return m_armsRaised.transform.localScale.z >= DistanceToTarget();
        }

        private void RaiseArms()
        {
            m_armsLowered.gameObject.SetActive(false);
            m_armsRaised.gameObject.SetActive(true);
        }

        private void LowerArms()
        {
            m_armsLowered.gameObject.SetActive(true);
            m_armsRaised.gameObject.SetActive(false);
        }

        private void ReachOut()
        {
            m_armReach += Time.deltaTime;
            m_armsRaised.transform.localScale += Vector3.forward * m_armReach;
        }

        private void SetState(UnstableState state)
        {
            m_state = state;
        }

        private void IdleState()
        {
            LowerArms();

            if (BeingLookedAt())
                SetState(UnstableState.Angry);
        }

        private void RoamingState()
        {

        }

        private void AngryState()
        {
            FaceTarget();
            RaiseArms();
            ReachOut();

            if (ArmsCloseEnough())
                Disappear();
        }
    }
}