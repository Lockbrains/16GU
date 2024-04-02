using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System.ComponentModel;

[CreateAssetMenu(fileName = "Data", menuName = "Phase/Characters/Relationship", order = 1)]
[System.Serializable]
public class Relationship : ScriptableObject
{
    [Header("Relationship")] 
    public string relationship;

    [Header("Characters")] 
    public CharacterProfile characterA;
    public CharacterProfile characterB;

    [Header("Description")]
    [TextArea(1,50)]
    public string description;

}
