using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Essence))]
[CanEditMultipleObjects]
public class EssenceEditor : Editor
{
    private Essence _target;

    private bool _hasNote;
    private bool _hasPassive;
    private bool _hasActive;
    private bool _hasReplacements;

    private bool _showDescription;
    private bool _showEffect;
    private bool _showReplacements;

    private SerializedProperty _from;
    private SerializedProperty _to;
    
    // 0: English
    // 1: Chinese
    private string _languageHint = "使用中文";
    private bool _isChinese;
    private int _languageOption = 1; 
    
    private void OnEnable()
    {
        _languageOption = 0;
        _target = base.target as Essence;

        _from = serializedObject.FindProperty("from");
        _to = serializedObject.FindProperty("to");
    }
    
    public override void OnInspectorGUI()
    {
        _isChinese = EditorGUILayout.Toggle(_languageHint, _isChinese);
        
        if (_isChinese) 
        {
            _languageOption = 1;
        }
        else 
        {
            _languageOption = 0;
        }

        DrawContent();
        
        if (GUI.changed)
        {
            EditorUtility.SetDirty(_target);
        }
    }

    private void DrawBasicInformation()
    {
        if (_languageOption == 0)
        {
            _target.ID = EditorGUILayout.IntField("ID", _target.ID);
            _target.type = (Essence.Type)EditorGUILayout.EnumPopup("EF Type", _target.type);
            _target.englishName = EditorGUILayout.TextField("English Name", _target.englishName);
            _target.chineseName = EditorGUILayout.TextField("Chinese Name", _target.chineseName);
            EditorGUILayout.Space(20);
        }
        else
        {
            _target.ID = EditorGUILayout.IntField("ID", _target.ID);
            _target.type = (Essence.Type)EditorGUILayout.EnumPopup("编码类型", _target.type);
            _target.englishName = EditorGUILayout.TextField("英文名", _target.englishName);
            _target.chineseName = EditorGUILayout.TextField("中文名", _target.chineseName);
            EditorGUILayout.Space(20);
        }
    }

    private void DrawLevelInformation()
    {
        if (_languageOption == 0)
        {
            _target.maxLevel = EditorGUILayout.IntField("MaxLv.", _target.maxLevel);
            EditorGUILayout.Space(20);
        }
        else
        {
            _target.maxLevel = EditorGUILayout.IntField("最大等级", _target.maxLevel);
            EditorGUILayout.Space(20);
        }
    }

    private void DrawDescriptions()
    {
        if (_languageOption == 0)
        {
            _showDescription = EditorGUILayout.BeginFoldoutHeaderGroup(_showDescription, "Description");
            if (_showDescription)
            {
                // Draw Descriptions
                EditorGUILayout.LabelField("Basic Description");
                _target.description = EditorGUILayout.TextArea(_target.description, GUILayout.Height(60));
                EditorGUILayout.Space(20);
            
                
                if (!_target.hasNote)
                {
                    if (GUILayout.Button("Add Note"))
                    {
                        _target.hasNote = true;
                    }

                } else
                {
                    EditorGUILayout.LabelField("Note");
                    _target.note = EditorGUILayout.TextArea(_target.note, GUILayout.Height(60));
                    if (GUILayout.Button("Delete Note"))
                    {
                        _target.note = "";
                        _target.hasNote = false;
                    }
                }
                EditorGUILayout.Space(20);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        else
        {
            _showDescription = EditorGUILayout.BeginFoldoutHeaderGroup(_showDescription, "技能描述");
            if (_showDescription)
            {
                // Draw Descriptions
                EditorGUILayout.LabelField("简介");
                _target.description = EditorGUILayout.TextArea(_target.description, GUILayout.Height(60));
                EditorGUILayout.Space(20);

                if (!_target.hasNote)
                {
                    if (GUILayout.Button("添加备注"))
                    {
                        _target.hasNote = true;
                    }
                } else
                {
                    EditorGUILayout.LabelField("备注");
                    _target.note = EditorGUILayout.TextArea(_target.note, GUILayout.Height(60));
                    if (GUILayout.Button("删除备注"))
                    {
                        _target.note = "";
                        _target.hasNote = false;
                    }
                }
                EditorGUILayout.Space(20);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }

    private void DrawEffects()
    {
        if (_languageOption == 0)
        {
            _showEffect = EditorGUILayout.BeginFoldoutHeaderGroup(_showEffect, "Effects");
            if (_showEffect)
            { 
                // Draw Passive/Active Effects
                EditorGUILayout.BeginHorizontal();
                _target.hasPassiveEffect = EditorGUILayout.Toggle("Passive", _target.hasPassiveEffect);
                _target.hasActiveEffect = EditorGUILayout.Toggle("Active", _target.hasActiveEffect);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(20);
            
                if (_target.hasPassiveEffect)
                {
                    EditorGUILayout.LabelField("Passive Effect,");
                    _target.passiveEffect = EditorGUILayout.TextArea(_target.passiveEffect, GUILayout.Height(60));
                }
                else
                {
                    _target.passiveEffect = "";
                }

                if (_target.hasActiveEffect)
                {
                    EditorGUILayout.LabelField("Active Effect,");
                    _target.activeEffect = EditorGUILayout.TextArea(_target.activeEffect, GUILayout.Height(60));
                }else
                {
                    _target.activeEffect = "";
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        else
        {
            _showEffect = EditorGUILayout.BeginFoldoutHeaderGroup(_showEffect, "技能效果");
            if (_showEffect)
            { 
                // Draw Passive/Active Effects
                EditorGUILayout.BeginHorizontal();
                _target.hasPassiveEffect = EditorGUILayout.Toggle("具有被动效果", _target.hasPassiveEffect);
                _target.hasActiveEffect = EditorGUILayout.Toggle("具有主动效果", _target.hasActiveEffect);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(20);
            
                if (_target.hasPassiveEffect)
                {
                    EditorGUILayout.LabelField("被动，");
                    _target.passiveEffect = EditorGUILayout.TextArea(_target.passiveEffect, GUILayout.Height(60));
                }
                else
                {
                    _target.passiveEffect = "";
                }

                if (_target.hasActiveEffect)
                {
                    EditorGUILayout.LabelField("主动，");
                    _target.activeEffect = EditorGUILayout.TextArea(_target.activeEffect, GUILayout.Height(60));
                }else
                {
                    _target.activeEffect = "";
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }

    private void DrawReplacements()
    {
        if (_languageOption == 0)
        {
            _showReplacements = EditorGUILayout.BeginFoldoutHeaderGroup(_showReplacements, "Replacements");
            if (_showReplacements)
            { 
                // Draw Passive/Active Effects
                _target.hasReplacement = EditorGUILayout.Toggle("Replacements", _target.hasReplacement);
                if (_target.hasReplacement)
                {
                    EditorGUILayout.LabelField("The following Encoded Functions are replaced:");
                    serializedObject.Update();
                    _from.arraySize = EditorGUILayout.IntField("Replacement Size", _from.arraySize);
                    _to.arraySize = _from.arraySize;
                    EditorGUILayout.Space(20);
                    for (int i = 0; i < _from.arraySize; i++)
                    {
                        var from_ef = _from.GetArrayElementAtIndex(i);
                        EditorGUILayout.PropertyField(from_ef, new GUIContent("Replaced Encoded Function " + (i+1)), true);
                    }
                    EditorGUILayout.Space(20);
                    for (int i = 0; i < _to.arraySize; i++)
                    {
                        var to_ef = _to.GetArrayElementAtIndex(i);
                        EditorGUILayout.PropertyField(to_ef, new GUIContent("New Encoded Function " + (i+1)), true);
                    }

                    serializedObject.ApplyModifiedProperties();
                }
                else
                {
                    serializedObject.Update();
                    _from.arraySize = 0;
                    _to.arraySize = 0;
                    serializedObject.ApplyModifiedProperties();
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        else
        {
            _showReplacements = EditorGUILayout.BeginFoldoutHeaderGroup(_showReplacements, "能力置换");
            if (_showReplacements)
            { 
                // Draw Passive/Active Effects
                _target.hasReplacement = EditorGUILayout.Toggle("内核是否会置换能力", _target.hasReplacement);
                if (_target.hasReplacement)
                {
                    EditorGUILayout.LabelField("以下的编码能力将会被一一对应地置换");
                    serializedObject.Update();
                    _from.arraySize = EditorGUILayout.IntField("置换容量", _from.arraySize);
                    _to.arraySize = _from.arraySize;
                    EditorGUILayout.Space(20);
                    for (int i = 0; i < _from.arraySize; i++)
                    {
                        var from_ef = _from.GetArrayElementAtIndex(i);
                        EditorGUILayout.PropertyField(from_ef, new GUIContent("被置换编码能力 " + (i+1)), true);
                    }
                    EditorGUILayout.Space(20);
                    for (int i = 0; i < _to.arraySize; i++)
                    {
                        var to_ef = _to.GetArrayElementAtIndex(i);
                        EditorGUILayout.PropertyField(to_ef, new GUIContent("新装填编码能力 " + (i+1)), true);
                    }

                    serializedObject.ApplyModifiedProperties();
                }
                else
                {
                    serializedObject.Update();
                    _from.arraySize = 0;
                    _to.arraySize = 0;
                    serializedObject.ApplyModifiedProperties();
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
    
    public void DrawContent()
    {
        // Draw Basic Information
        DrawBasicInformation();
        
        // Draw Level Information
        DrawLevelInformation();
        
        // Draw Description
        DrawDescriptions();
        
        // Draw Effects
        DrawEffects();
        
        // Draw Replacements
        DrawReplacements();

    }
}
