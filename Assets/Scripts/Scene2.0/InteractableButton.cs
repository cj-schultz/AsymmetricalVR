using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

namespace MMVR
{
    [RequireComponent(typeof(Interactable))]
    public class InteractableButton : MonoBehaviour
    {
        [HideInInspector]
        public bool isActive;

        [SerializeField]
        private UnityEvent buttonPressedEvent;
        [SerializeField]
        private Vector3 pushToPosition;
        [SerializeField]
        private float pushSpeed;

        private Vector3 originalPosition;

        public virtual void Awake()
        {
            originalPosition = transform.localPosition;
        }

        private void OnHandHoverBegin(Hand hand)
        {
            if (isActive)
            {
                isActive = false;
                StartCoroutine(PushButton());
            }
        }
        
        private IEnumerator PushButton()
        {
            while (!Vector3.Equals(transform.localPosition, pushToPosition))
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, pushToPosition, Time.deltaTime * pushSpeed);
                yield return null;
            }

            ButtonPressedEvent();

            while (!Vector3.Equals(transform.localPosition, originalPosition))
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, originalPosition, Time.deltaTime * pushSpeed);
                yield return null;
            }

            isActive = true;
        }

        public virtual void ButtonPressedEvent()
        {
            if(buttonPressedEvent != null)
            {
                buttonPressedEvent.Invoke();
            }

            return;
        }
    }
}
