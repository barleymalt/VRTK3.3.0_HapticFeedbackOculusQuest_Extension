using System.Collections;
using UnityEngine;

namespace VRTK.CustomScripts
{
    public class VRTKCustom_HapticFeedback : MonoBehaviour
    {
        public VRTK_InteractableObject linkedObject;

        [Range(0, 1f)] [SerializeField] protected float m_pulseIntensity;
        [Range(0, 1f)] [SerializeField] protected float m_pulseSpan;
        [Range(0, 1f)] [SerializeField] protected float m_spanBetweenpulse;

        [Tooltip("If duration is 0, only one pulse will be executed.")]
        [Range(0, 1f)] [SerializeField] protected float m_duration;
        [SerializeField] protected bool m_isContinuous;

        protected IEnumerator _asyncHapticPulse;
        protected bool _keepAlive;

        protected const string RIGHT_CONTROLLER_TAG = "RHand";
        protected const string LEFT_CONTROLLER_TAG = "LHand";


        private void Start()
        {
            linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

            _keepAlive = m_isContinuous;
            m_duration = (m_duration == 0) ? m_pulseSpan : m_pulseSpan;
        }

        protected IEnumerator HapticPulse(float pulseDuration, float intervalDuration, float totalDuration, float intensity, bool isContinuous, bool isRHand)
        {
            _keepAlive = isContinuous;

            while (totalDuration > 0 || _keepAlive)
            {
                if (isRHand) OVRInput.SetControllerVibration(.1f, intensity, OVRInput.Controller.RTouch);
                else OVRInput.SetControllerVibration(.1f, intensity, OVRInput.Controller.LTouch);
                yield return new WaitForSeconds(pulseDuration);

                if (isRHand) OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
                else OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
                yield return new WaitForSeconds(intervalDuration);

                if (!_keepAlive) totalDuration -= pulseDuration + intervalDuration;
            }

            _asyncHapticPulse = null;
        }

        protected void CancelHapticPulse()
        {
            _keepAlive = false;
        }
    }
}
