using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ButtonInteraction : MonoBehaviour
{
    [SerializeField] private UnityEvent whenPressed;
    //[SerializeField] private Animator buttonAnimator;

    public AudioSource buttonPressSound;

    public bool isPressed;

    public EquipmentItem itemCurrent;

    public InputAction interactAction;

    //public InputAction chimeAction;

    void Awake()
    {
        buttonPressSound = GetComponent<AudioSource>();
        itemCurrent = GetComponent<EquipmentItem>();
        isPressed = false;
    }

    public void OnEnable()
    {
        interactAction.Enable();
    }

    public void OnDisable()
    {
        interactAction.Disable();
    }

    private void Update()
    {
        if (!isPressed)
        {
            isPressed = interactAction.IsPressed();
        }
        else
        {
            Debug.Log("Button pressed.");

            whenPressed.Invoke();
            Debug.Log("Script ran.");
            isPressed = false;
        }
    }


}