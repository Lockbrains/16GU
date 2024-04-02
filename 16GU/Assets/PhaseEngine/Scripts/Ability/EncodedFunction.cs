using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Phase/Abilities/Encoded Function", order = 2)]
[System.Serializable]
public class EncodedFunction : ScriptableObject
{
    public enum Type
    {
        Stamina, 
        Archive,
        Behavioral,
        Constructive
    }

    public int ID;
    public Type type;

    public string englishName;
    public string chineseName;
    public string description;

    //public int currentLevel;
    public int maxLevel;

    public bool hasNote;
    public string note;

    public bool hasPassiveEffect;
    public bool hasActiveEffect;

    public string passiveEffect;
    public string activeEffect;


}
