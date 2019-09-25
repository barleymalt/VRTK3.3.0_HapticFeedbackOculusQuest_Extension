using UnityEngine;

namespace VRTK.CustomScripts
{
    public class VRTKCustom_GrabHapticFeedback : VRTKCustom_HapticFeedback
    {
        protected void OnEnable()
        {
            if (linkedObject != null)
            {
                linkedObject.InteractableObjectGrabbed += InteractableObjectGrabbed;
                linkedObject.InteractableObjectUngrabbed += InteractableObjectUngrabbed;
            }
        }

        protected void OnDisable()
        {
            if (linkedObject != null)
            {
                linkedObject.InteractableObjectGrabbed -= InteractableObjectGrabbed;
                linkedObject.InteractableObjectUngrabbed -= InteractableObjectUngrabbed;
            }
        }

        protected virtual void InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
        {
            var isRightHand = e.interactingObject.tag == RIGHT_CONTROLLER_TAG;
            _asyncHapticPulse = HapticPulse(m_pulseSpan, m_spanBetweenpulse, m_duration, m_pulseIntensity, m_isContinuous, isRightHand);
            StartCoroutine(_asyncHapticPulse);

            Debug.Log($"Feedback {linkedObject.name} OnGrabbed.");
        }

        protected virtual void InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
        {
            CancelHapticPulse();

            Debug.Log($"Feedback {linkedObject.name} OnUngrabbed.");
        }
    }
}