using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private int baseValue;

    public List<int> modifiers;
    public System.Action onValueChanged;
    public Stat()
    {
        modifiers = new List<int>();
    }

    public int GetValue()
    {
        int finalValue = baseValue;

       // return baseValue;

        foreach(int modifier in modifiers)
        {
            finalValue += modifier;
        }
        return finalValue;
    }

    public void SetDefaultValue(int _value)
    {
        baseValue = _value;
        onValueChanged?.Invoke();
    }
    public void AddModifier(int _modifier)
    {
        modifiers.Add(_modifier);
        onValueChanged?.Invoke();
    }

    public void RemoveModifier(int _modifier)
    {
        modifiers.Remove(_modifier);
        onValueChanged?.Invoke();
    }
}
