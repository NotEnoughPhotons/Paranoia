using MelonLoader;
using NEP.Paranoia.Audio;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NEP.Paranoia.Entities
{
    [RegisterTypeInIl2Cpp]
    public class Ringer(IntPtr ptr) : Entity(ptr)
    {
        private enum RingerState
        {
            Waiting,
            Calling,
            InCall,
            HangedUp
        }
        
        private RingerState m_state;

        private float m_incomingCallTimer = 0f;
        private float m_incomingCallDelay = 5f;
        
        private float m_preCallTimer = 0f;
        private float m_ringDelay = 0f;

        private float m_callTimer = 0f;
        private float m_callDuration = 0f;

        protected override void Awake()
        {
            base.Awake();
            SetInsanity(2.25f);
        }

        public override void EntityStart()
        {
            base.EntityStart();

            Appear();
            
            Emit(AudioBank.RingerRingExternal);
            m_state = RingerState.Waiting;
        }

        protected override void EntityUpdate()
        {
            if (m_state == RingerState.InCall)
            {
                m_callTimer += Time.deltaTime;
                
                if (m_callTimer >= m_callDuration)
                {
                    m_callTimer = 0f;
                    m_state = RingerState.HangedUp;
                    SetLooping(false);
                    Emit(AudioBank.RingerHangedUp);
                }
                
                return;
            }
            
            if (m_state == RingerState.Waiting)
            {
                m_incomingCallTimer += Time.deltaTime;
                
                if (m_incomingCallTimer >= m_incomingCallDelay)
                {
                    m_incomingCallTimer = 0f;
                    Emit(AudioBank.RingerRingExternal);
                }

                if (DistanceToTarget() <= 0.275f)
                {
                    SetLooping(true);
                    Emit(AudioBank.RingerCallRing);
                    m_state = RingerState.Calling;
                    m_ringDelay = Random.Range(AudioBank.RingerCallRing.length * 2f, AudioBank.RingerCallRing.length * 2f);
                }
            }

            if (m_state == RingerState.Calling)
            {
                m_preCallTimer += Time.deltaTime;
                
                if (m_preCallTimer >= m_ringDelay)
                {
                    m_state = RingerState.InCall;
                    m_preCallTimer = 0f;
                    AudioClip call = AudioBank.RingerCalls[Random.Range(0, AudioBank.RingerCalls.Length)];
                    m_callDuration = call.length;
                    Paranoia.Logger.Msg(m_callDuration);
                    SetLooping(false);
                    Emit(call);
                }
            }
        }
    }
}