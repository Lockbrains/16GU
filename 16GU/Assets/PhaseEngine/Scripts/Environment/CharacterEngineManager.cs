using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEngineManager : MonoBehaviour
{
    public static CharacterEngineManager instance;

    public List<CharacterData> database = new List<CharacterData>();


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
