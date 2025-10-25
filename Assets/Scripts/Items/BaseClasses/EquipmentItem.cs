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
        dmg_multi = 1;
        health_flat = 0;
        regen = 0;  
    }

    public EquipmentItem(float df, float dm, float hf, float regn, string desc, string rar, string tp) : base(desc, rar, tp)
    {
        dmg_flat = df;
        dmg_multi = dm;
        health_flat = hf;
        regen = regn;
    }

    public void setdmgF(float val)
    {
        dmg_flat += val;
    }

    public float getdmgF()
    {
        return dmg_flat;
    }

    public void setdmgM(float val)
    {
        dmg_multi += val;
    }

    public float getdmgM()
    {
        return dmg_multi;
    }

    public void sethealth(float val)
    {
        health_flat = val;
    }

    public float gethealth() {  
        return health_flat;
    }

    public void setRegen(float val)
    {
        regen = val;
    }

    public float getRegen()
    {
        return regen;
    }
}