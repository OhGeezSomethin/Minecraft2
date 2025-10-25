using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class PlayerInteraction : MonoBehaviour, IInteractable
{
    public InputAction interactAction;
    private AudioSource buttonPressSound;
    private bool isPressed;
    private EquipmentItem itemCurrent;
    private ButtonInteraction bInt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bInt = GetComponent<ButtonInteraction>(); ;
        buttonPressSound = bInt.buttonPressSound;
        itemCurrent = bInt.itemCurrent;
        isPressed = bInt.isPressed;
        bInt.OnEnable();
    }

    public void OnEnable()
    {
        interactAction.Enable();
    }

    public void OnDisable()
    {
        interactAction.Disable();
    }

    public void Interact()
    {
        if (isPressed) return; // Prevents from being pressed again

        isPressed = true;
        //Debug.Log("Button pressed.");

        itemCurrent.toggleHeld();

        buttonPressSound.Play();
    }
}
