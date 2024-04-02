using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System.ComponentModel;

[CreateAssetMenu(fileName = "Data", menuName = "Phase/Characters/Profile", order = 1)]
public class CharacterProfile : ScriptableObject
{
    public enum AugmentationType {
        Normal,
        Squared,
        Doubled
    }

    public enum MaterializedStatus
    {
        Non, Materialized, NegMaterialized
    }
    
    [Header("Basic Information")]
    public string firstName;
    public string lastName;
    public string nativeName;
    public string nicknames;

    [Header("Dates")]
    public int Year;
    public int Month;
    public int Day;

    [Header("Physical Data")]
    public float height;
    public float weight;

    [Header("Profile Photo")]
    public Sprite profilePhoto;
    public Sprite illustration;

    [Header("Ability - Basics")] 
    public EncodedFunction.Type bedrock;
    public float level;
    public AugmentationType aType;
    public MaterializedStatus mStatus;

    [Header("Ability - Physical Augmentation I")]
    public bool pa1Active;
    public Vector3Int pa1Levels;

    [Header("Ability - Spiritual Augmentation I")]
    public bool sa11Active, sa12Active, sa13Active;
    public EncodedFunction sa11, sa12, sa13;
    public Vector3Int sa1Levels;

    [Header("Ability - Physical Augmentation II")]
    public bool pa21Active, pa22Active, pa23Active;
    public EncodedFunction pa21, pa22, pa23;
    public Vector3Int pa2Levels;

    [Header("Ability - Spiritual Augmentation II")]
    public bool sa21Active, sa22Active, sa23Active;
    public EncodedFunction sa21, sa22, sa23;
    public Vector3Int sa2Levels;

    [Header("Ability - Essence I")] 
    public bool e1Active;
    public Essence essence1;
    public int e1Level;
    
    [Header("Ability - Essence II")] 
    public bool e2Active;
    public Essence essence2;
    public int e2Level;
    
    [Header("Ability - Essence III")] 
    public bool e3Active;
    public Essence essence3;
    public int e3Level;

    [Header("Relationships")] 
    public List<Relationship> relationships;


}
