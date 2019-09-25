using UnityEngine;

namespace VRTK.CustomScripts
{
    public class VRTKCustom_UseHapticFeedback : VRTKCustom_HapticFeedback
    {
        protected void OnEnable()
        {
            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed += InteractableObjectUsed;
                linkedObject.InteractableObjectUnused += InteractableObjectUnused;
            }
        }

        protected void OnDisable()
        {
            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
                linkedObject.InteractableObjectUnused -= InteractableObjectUnused;
            }
        }

        protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
        {
            var isRightHand = e.interactingObject.tag == RIGHT_CONTROLLER_TAG;
            _asyncHapticPulse = HapticPulse(m_pulseSpan, m_spanBetweenpulse, m_duration, m_pulseIntensity, m_isContinuous, isRightHand);
            StartCoroutine(_asyncHapticPulse);

            Debug.Log($"Feedback {linkedObject.name} OnUsed.");
        }

        protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
        {
            CancelHapticPulse();

            Debug.Log($"Feedback {linkedObject.name} OnUnused.");
        }
    }
}