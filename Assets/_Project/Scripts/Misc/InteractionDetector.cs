using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    public Key interactionKey = Key.E;

    private IInteractable currentInteractable;

    void Update()
    {
        if (currentInteractable != null &&
            Keyboard.current[interactionKey].wasPressedThisFrame)
        {
            currentInteractable.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable != null)
        {
            currentInteractable = interactable;
            Debug.Log("Can interact with " + other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable != null && interactable == currentInteractable)
        {
            currentInteractable = null;
            Debug.Log("No longer in range");
        }
    }
}