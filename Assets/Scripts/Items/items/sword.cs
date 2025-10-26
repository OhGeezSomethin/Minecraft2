using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class sword : MonoBehaviour, IInteractable
{
    private GameObject swrd;
    //[SerializeField] private UnityEvent startInter;

    protected EquipmentItem swrd_item;

    protected ButtonInteraction intAct;

    // Start is called once before the first execution of Update after the MonoBehaviour is created    
    void Start()
    {
        swrd = GameObject.Find("Sword");
        swrd_item = ScriptableObject.CreateInstance<EquipmentItem>();
        swrd_item.setdmgF(20);
        swrd_item.setButtonAudio(swrd.GetComponent<AudioSource>());
        intAct = swrd.AddComponent<ButtonInteraction>();
        intAct.setKey(KeyCode.E);
    }

    public void Interact()
    {

        //swrd_item.toggleHeld();

        swrd_item.playButtonAudio();
    }
}
