using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemBase", menuName = "Scriptable Objects/ItemBase")]
public class ItemBase : ScriptableObject
{
    protected string descript;
    protected string rarety;
    protected string type;
    protected bool isHeld;

    public ItemBase()
    {
        descript = string.Empty;
        rarety = string.Empty;
        type = string.Empty;
        isHeld = false;
    }

    public ItemBase(string desc, string rar, string tp)
    {
        descript = desc;
        rarety = rar;
        type = tp;
    }

    public void setDesc(string desc)
    {
        descript = string.Copy(desc);
    }

    public string getDesc()
    {
        return string.Copy(descript);
    }

    public void setRarety(string rar)
    {
        descript = string.Copy(rar);
    }

    public string getRarety()
    {
        return string.Copy(rarety);
    }

    public void setType(string tp)
    {
        type = string.Copy(tp);
    }

    public string getType()
    {
        return string.Copy(type);
    }

    public void toggleHeld()
    {
        isHeld = !isHeld;
        Debug.Log("Toggle Set.");
    }
}
