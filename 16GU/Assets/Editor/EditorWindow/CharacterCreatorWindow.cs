using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterCreatorWindow : EditorWindow
{
    private int _currentlySelectedEdit;
    private int _languageOption = 0;
    private CharacterDatabase _database;
    private Character _characterBeingEdited;
    private string[] _tabs = new string[5] { "Basic Info", "Behavior", "Abilities", "Relationships", "Timelines" };
    [MenuItem("Phase/Character/Create")]
    static void Initialize()
    {
        // if this window exists, it will focus on this window
        CharacterCreatorWindow myWindow = GetWindow<CharacterCreatorWindow>();

        // Add title to the window
        myWindow.titleContent = new GUIContent("Character Editor");
    }

    private void OnGUI()
    {
        
        // Create a field for the user to drag the database
        _database = (CharacterDatabase)EditorGUILayout.ObjectField("Database", _database, typeof(CharacterDatabase), true);

        if (_database != null)
        {
            if (_database.database == null)
            {
                _database.database = new System.Collections.Generic.List<Character>();
            }

            EditorGUILayout.HelpBox("Create your character here.", MessageType.Info);

            //_currentlySelected = GUI.Toolbar(new Rect(0, 0, toolbarPos.width, 50), _currentlySelected, _tabs);
            _currentlySelectedEdit = GUILayout.Toolbar(_currentlySelectedEdit, _tabs);

            if (_characterBeingEdited != null)
            {
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

                if (GUILayout.Button("Create A New Character."))
                {
                    if (!isIDTaken(_characterBeingEdited._id))
                    {
                        _database.database.Add(_characterBeingEdited);
                        _characterBeingEdited = null;
                        Debug.Log("This character has been added to the database.");
                    }
                    else
                    {
                        Debug.LogError("This ID has been taken.");
                    }
                }
            }
            else
            {
                Debug.Log("Creating a new tmp space for new character.");
                _characterBeingEdited = new Character(_database.database.Count);
            }

        }
        else
        {
            EditorGUILayout.HelpBox("Please assign a valid character database above.", MessageType.Error);
        }

        void DrawBasic(int languageOption)
        {
            _characterBeingEdited._id = EditorGUILayout.IntSlider("ID", _characterBeingEdited._id, 0, 500);
            _characterBeingEdited.firstName = EditorGUILayout.TextField("Name", _characterBeingEdited.firstName);
        }

        void DrawBehavior(int languageOption)
        {
            _characterBeingEdited._id = EditorGUILayout.IntSlider("ID", _characterBeingEdited._id, 0, 500);
            _characterBeingEdited.firstName = EditorGUILayout.TextField("Name", _characterBeingEdited.firstName);
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

        bool isIDTaken(int id)
        {
            foreach (Character c in _database.database)
            {
                if (c._id == id)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
