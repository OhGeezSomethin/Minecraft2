using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemBase", menuName = "Scriptable Objects/ItemBase")]
public class ItemBase : ScriptableObject
{
    string description;
    string rarety;

    public ItemBase()
    {
        description = string.Empty;
        rarety = string.Empty;
    }

    public ItemBase(string desc, string rar)
    {
        description = desc;
        rarety = rar;
    }
}
