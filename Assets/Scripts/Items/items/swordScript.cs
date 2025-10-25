using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class swordScript : MonoBehaviour
{
    private GameObject swrd;
    [SerializeField] private UnityEvent startInter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created    
    void Start()
    {
        swrd = GameObject.Find("Sword");
        EquipmentItem swrd_item = swrd.AddComponent<EquipmentItem>();
        swrd_item.setdmgF(20);
        startInter.Invoke();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
