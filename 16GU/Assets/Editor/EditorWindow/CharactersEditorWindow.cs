using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class CharactersEditorWindow : EditorWindow
{

    private int _currentlySelectedEdit;
    private int _languageOption = 0;
    private CharacterDatabase _database;
    private Character _characterBeingEdited;
    private string[] _tabs = new string[5] { "Basic Info", "Behavior", "Abilities", "Relationships", "Timelines" };
    [MenuItem("Phase/Character/Edit")]
    static void Initialize()
    {
        // if this window exists, it will focus on this window
        CharactersEditorWindow myWindow = GetWindow<CharactersEditorWindow>();

        // Add title to the window
        myWindow.titleContent = new GUIContent("Character Editor");
    }

    private void OnGUI()
    {
        // Create a field for the user to drag the database
        _database = (CharacterDatabase) EditorGUILayout.ObjectField("Database", _database, typeof(CharacterDatabase), true);

        if (_database != null)
        {
            if(_database.database == null)
            {
                _database.database = new System.Collections.Generic.List<Character>();
            }
            // In Edit Mode
            EditorGUILayout.HelpBox("Edit your character here.", MessageType.Info);
            //Rect toolbarPos = EditorGUILayout.GetControlRect();
            //_currentlySelected = GUI.Toolbar(new Rect(0, 0, toolbarPos.width, 50), _currentlySelected, _tabs);
            _currentlySelectedEdit = GUILayout.Toolbar(_currentlySelectedEdit, _tabs);

            switch (_currentlySelectedEdit)
            {
                case 0:
                    DrawBasic(_languageOption);
                    break;
                case 1:
                    DrawBehavior(_languageOption);
                    break;
                case 2:
                    DrawAbilities(_languageOption);
                    break;
                case 3:
                    DrawRelationships(_languageOption);
                    break;
                case 4:
                    DrawTimelines(_languageOption);
                    break;
                default:
                    break;
            }
            
            
        }
        else
        {
            EditorGUILayout.HelpBox("Please assign a valid character database above.", MessageType.Error);
        }


    }



    void DrawBasic(int languageOption)
    {

    }

    void DrawBehavior(int languageOption)
    {

    }

    void DrawAbilities(int languageOption)
    {

    }

    void DrawRelationships(int languageOption)
    {

    }

    void DrawTimelines(int languageOption)
    {

    }

    


    
}
