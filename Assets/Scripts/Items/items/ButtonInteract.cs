using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ButtonInteraction : MonoBehaviour
{
    //[SerializeField] private UnityEvent whenPressed;

    protected bool isPressed;
    protected bool isReleased;  

    protected KeyCode interactkey;

    IInteractable interactable;

    public ButtonInteraction(KeyCode ik)
    {
        interactkey = ik;
    }

    void Awake()
    {
        isPressed = false;
        isReleased = false;
        interactable = GetComponent<IInteractable>();
    }

    public void setPressFalse()
    {
        isPressed = false;
    }

    public void setPressTrue()
    {
        isPressed = true;
    }

    public void setKey(KeyCode key)
    {
        interactkey = key;
    }

    public KeyCode getKey()
    {
        return interactkey;
    }

    void Update()
    {
        if (isReleased)
        {
            interactable.Interact();
            Debug.Log("Script started.");
            isReleased = false;
            isPressed = false;
        }
        else if (Input.GetKeyDown(interactkey))
        {
            isPressed = true;
            Debug.Log("Button pressed and trap sprung.");
            //return;
        }
        else if(!Input.GetKeyDown(interactkey) && isPressed)
        {
            isReleased = true;
            Debug.Log("Button released.");
            //return;
        }

        ////Start interact on key release
        //if (!isPressed && isReleased)
        //{
        //    interactable.Interact();
        //    Debug.Log("Script started.");
        //    isReleased = false;
        //    return;
        //}

        //if (!Input.GetKeyDown(interactkey) && isPressed)
        //{
        //    isReleased = true;
        //    isPressed = false;
        //    return;

        //    //interactable.Interact();
        //    //Debug.Log("Script started.");

        //}

        //if (Input.GetKeyDown(interactkey) && !isPressed)
        //{
        //    isPressed = true;
        //    Debug.Log("Button pressed and toggle sprung.");
        //    return;
        //}

        //isPressed = false;
    }




}