using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentItem", menuName = "Scriptable Objects/EquipmentItem")]
public class EquipmentItem : ItemBase
{
    float dmg_flat;
    float dmg_multi;
    float health_flat;
    float regen;

    public EquipmentItem()
    {
        dmg_flat = 0;
        dmg_multi = 0;
        health_flat = 0;
        regen = 0;  
    }

    public EquipmentItem(float df, float dm, float hf, float regn)
    {
        dmg_flat = df;
        dmg_multi = dm;
        health_flat = hf;
        regen = regn;
    }
}