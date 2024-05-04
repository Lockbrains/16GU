using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Character/Add A Character", order = 1)]
[Serializable]
public class CharacterData : ScriptableObject
{
    public enum GiftType
    {
        Stamina, Archive, Behavior, Constructive
    }

    [Header("Basic Information")]
    public string offcialName;
    public string nickName;
    public int age;
    public GiftType type;
    public int level;
    public string basicDescription;

    [Header("Ability Data")]
    public Ability[] physicalI;
    public Ability[] spritualI;
    public Ability[] physicalII;
    public Ability[] spritualII;
}
