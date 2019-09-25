using UnityEngine;

namespace VRTK.CustomScripts
{
    public class VRTKCustom_NearTouchHapticFeedback : VRTKCustom_HapticFeedback
    {
        protected void OnEnable()
        {
            if (linkedObject != null)
            {
                linkedObject.InteractableObjectNearTouched += InteractableObjectNearTouched;
                linkedObject.InteractableObjectNearUntouched += InteractableObjectNearUntouched;
            }
        }

        protected void OnDisable()
        {
            if (linkedObject != null)
            {
                linkedObject.InteractableObjectNearTouched -= InteractableObjectNearTouched;
                linkedObject.InteractableObjectNearUntouched -= InteractableObjectNearUntouched;
            }
        }

        protected virtual void InteractableObjectNearTouched(object sender, InteractableObjectEventArgs e)
        {
            var isRightHand = e.interactingObject.tag == RIGHT_CONTROLLER_TAG;
            _asyncHapticPulse = HapticPulse(m_pulseSpan, m_spanBetweenpulse, m_duration, m_pulseIntensity, m_isContinuous, isRightHand);
            StartCoroutine(_asyncHapticPulse);

            Debug.Log($"Feedback {linkedObject.name} OnNearTouched.");
        }

        protected virtual void InteractableObjectNearUntouched(object sender, InteractableObjectEventArgs e)
        {
            CancelHapticPulse();

            Debug.Log($"Feedback {linkedObject.name} OnNearUntouched.");
        }
    }
}