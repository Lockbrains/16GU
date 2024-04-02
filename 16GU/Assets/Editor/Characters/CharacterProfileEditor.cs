using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(CharacterProfile))]
[CanEditMultipleObjects]
public class CharacterProfileEditor : Editor
{

    private int _currentlySelected = 0;
    private CharacterProfile _target;


    // Basic Info Bools
    private bool _showNames = true;
    private bool _showDates = false;
    private bool showPhysicalData = false;
    private bool showProfilePhoto = false;
    
    // Skill Info Bools
    private bool _abilityBasics;
    private bool _showPA1;
    private bool _showSA1;
    private bool _showPA2;
    private bool _showSA2;
    private bool _showE1, _showE2, _showE3;
    
    // PA1
    private int _power = 1;
    private int _speed = 1;
    private int _responsiveness = 1;
    private Vector3Int _PSR;
    private string _helpMessage;
    
    // SA1
    private int _sa11, _sa12, _sa13;
    private Vector3Int _sa1;
    
    // PA2
    private int _pa21, _pa22, _pa23;
    private Vector3Int _pa2;
    
    // SA2
    private int _sa21, _sa22, _sa23;
    private Vector3Int _sa2;

    // Relationship
    private ReorderableList _relationships;
    private SerializedProperty m_Relationships;
    

    // 0: English
    // 1: Chinese
    private string _languageHint = "使用中文";
    private bool _isChinese;
    private int _languageOption = 1; 

    private void OnEnable()
    {
        _languageOption = 0;
        _target = base.target as CharacterProfile;
        m_Relationships = serializedObject.FindProperty("relationships");
    }

    private void OnDisable()
    {
        
    }

    // This is asking the inspector to use the override function defined here.
    public override void OnInspectorGUI()
    {
        // Adding tabs for the inspector
        _isChinese = EditorGUILayout.Toggle(_languageHint, _isChinese);
        if (_isChinese) 
        {
            _languageOption = 1;
            _languageHint = "Use English";
        }
        else 
        {
            _languageOption = 0;
            _languageHint = "使用中文";
        }

        string[] tabs = new string[5] { "Basic Info", "Behavior", "Abilities", "Relationships", "Timelines"};
        string[] tabs_chinese = new string[5] { "基本信息", "行为表现", "能力", "组织关系", "时间线" };
        
        if(!_isChinese) _currentlySelected = GUILayout.Toolbar(_currentlySelected, tabs);
        else _currentlySelected = GUILayout.Toolbar(_currentlySelected, tabs_chinese);
        // Create a help box
        switch(_currentlySelected)
        {
            case 0:
                DrawBasicInfoInspector(_languageOption);
                break;
            case 1:
                DrawBehaviorInspector(_languageOption);
                break;
            case 2:
                DrawSkillsInfoInspector(_languageOption);
                break;
            case 3:
                DrawRelationshipsInspector(_languageOption);
                break;
            case 4:
                DrawTimelinesInspector(_languageOption);
                break;
            default:
                break;
        }


        if (GUI.changed)
        {
            EditorUtility.SetDirty(_target);
        }
    }

    public void DrawBasicInfoInspector(int languageOption)
    {
        if(languageOption == 0)
        {
            EditorGUILayout.HelpBox("Some basic information of the character.", MessageType.Info);

            // Draw Names
            _showNames = EditorGUILayout.BeginFoldoutHeaderGroup(_showNames, "Names.");
            if(_showNames)
            {
                EditorGUILayout.Space();
                _target.firstName = EditorGUILayout.TextField("First Name", _target.firstName);
                _target.lastName = EditorGUILayout.TextField("Last Name", _target.lastName);
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox("Please write the native name in the native language.", MessageType.Info);
                _target.nativeName = EditorGUILayout.TextField("Native Name", _target.nativeName);
                EditorGUILayout.HelpBox("Please separate the nicknames with comma.", MessageType.Info);
                EditorGUILayout.LabelField("Nicknames");
                _target.nicknames = EditorGUILayout.TextArea(_target.nicknames, GUILayout.Height(60));
                EditorGUILayout.Space(30);

            }
            EditorGUILayout.EndFoldoutHeaderGroup();


            // Draw Dates
            _showDates = EditorGUILayout.BeginFoldoutHeaderGroup(_showDates, "Birthday.");
            if(_showDates)
            {
                _target.Month = EditorGUILayout.IntSlider("MM.", _target.Month, 1, 12);
                _target.Day = EditorGUILayout.IntSlider("DD.", _target.Day, 1, 31);
                _target.Year = EditorGUILayout.IntField("YY.", _target.Year);
                EditorGUILayout.Space(30);

            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            // Draw Physical Data
            showPhysicalData = EditorGUILayout.BeginFoldoutHeaderGroup(showPhysicalData, "Physical Data.");
            if (showPhysicalData)
            {
                _target.height = EditorGUILayout.Slider("Height(cm)", _target.height, 0f, 250f);
                _target.weight = EditorGUILayout.Slider("Weight(kg)", _target.weight, 0f, 300f);
                EditorGUILayout.Space(30);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            showProfilePhoto = EditorGUILayout.BeginFoldoutHeaderGroup(showProfilePhoto, "Photo.");
            if(showProfilePhoto)
            {
                EditorGUILayout.LabelField("Profile photo");
                _target.profilePhoto = EditorGUILayout.ObjectField(_target.profilePhoto, typeof(Sprite), true,
                                                       GUILayout.Height(100), GUILayout.Width(100)) as Sprite;

                EditorGUILayout.LabelField("Illustration");
                _target.illustration = EditorGUILayout.ObjectField(_target.illustration, typeof(Sprite), true,
                                                       GUILayout.Height(200), GUILayout.Width(100)) as Sprite;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();



            //base.OnInspectorGUI();
        } else if (languageOption == 1)
        {
            EditorGUILayout.HelpBox("一些有关该角色的基本情报。", MessageType.Info);

            // Draw Names
            _showNames = EditorGUILayout.BeginFoldoutHeaderGroup(_showNames, "姓名");
            if (_showNames)
            {
                EditorGUILayout.Space();
                _target.lastName = EditorGUILayout.TextField("姓", _target.lastName);
                _target.firstName = EditorGUILayout.TextField("名", _target.firstName);
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox("请使用原语言书写原名。", MessageType.Info);
                _target.nativeName = EditorGUILayout.TextField("原名", _target.nativeName);
                EditorGUILayout.HelpBox("请用逗号和一个空格隔开昵称。", MessageType.Info);
                EditorGUILayout.LabelField("昵称");
                _target.nicknames = EditorGUILayout.TextArea(_target.nicknames, GUILayout.Height(60));
                EditorGUILayout.Space(30);

            }
            EditorGUILayout.EndFoldoutHeaderGroup();


            // Draw Dates
            _showDates = EditorGUILayout.BeginFoldoutHeaderGroup(_showDates, "生日");
            if (_showDates)
            {
                _target.Year = EditorGUILayout.IntField("年", _target.Year);
                _target.Month = EditorGUILayout.IntSlider("月", _target.Month, 1, 12);
                _target.Day = EditorGUILayout.IntSlider("日", _target.Day, 1, 31);
                EditorGUILayout.Space(30);

            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            // Draw Physical Data
            showPhysicalData = EditorGUILayout.BeginFoldoutHeaderGroup(showPhysicalData, "身体数据");
            if (showPhysicalData)
            {
                _target.height = EditorGUILayout.Slider("身高(cm)", _target.height, 0f, 250f);
                _target.weight = EditorGUILayout.Slider("体重(kg)", _target.weight, 0f, 300f);
                EditorGUILayout.Space(30);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            showProfilePhoto = EditorGUILayout.BeginFoldoutHeaderGroup(showProfilePhoto, "外观");
            if (showProfilePhoto)
            {
                EditorGUILayout.LabelField("头像");
                _target.profilePhoto = EditorGUILayout.ObjectField(_target.profilePhoto, typeof(Sprite), true,
                                                       GUILayout.Height(100), GUILayout.Width(100)) as Sprite;

                EditorGUILayout.LabelField("立绘");
                _target.illustration = EditorGUILayout.ObjectField(_target.illustration, typeof(Sprite), true,
                                                       GUILayout.Height(200), GUILayout.Width(100)) as Sprite;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }

    public void DrawSkillsInfoInspector(int languageOption)
    {
        // Draw Physical Augmentation I
        DrawAbilityBasics();
        DrawPhysicalAugmentationI();
        DrawSpiritualAugmentationI();
        DrawPhysicalAugmentationII();
        DrawSpiritualAugmentationII();
        DrawEssenceI();
        DrawEssenceII();
        DrawEssenceIII();
    }

    private void DrawAbilityBasics()
    {
        if (_languageOption == 0)
        {
            _abilityBasics = EditorGUILayout.BeginFoldoutHeaderGroup(_abilityBasics, "Basic.");
            if (_abilityBasics)
            {
                switch (_target.bedrock)
                {
                    case EncodedFunction.Type.Archive:
                        EditorGUILayout.HelpBox("Archive:\nA-type EFs are boosted by 10%.", MessageType.Info);
                        break;
                    case EncodedFunction.Type.Behavioral:
                        EditorGUILayout.HelpBox("Behavioral:\nAll types of EFs are boosted by 5%.", MessageType.Info);
                        break;
                    case EncodedFunction.Type.Constructive:
                        EditorGUILayout.HelpBox("Constructive:\nC-type EFs are boosted by 15%.\nABS-type EFs are nurfed by 5%.", MessageType.Info);
                        break;
                    case EncodedFunction.Type.Stamina:
                        EditorGUILayout.HelpBox("Stamina:\nS-type EFs are boosted by 15%.\nABC-type EFs are nurfed by 5%.", MessageType.Info);
                        break;
                    default:
                        break;
                }
        
                _target.bedrock = (EncodedFunction.Type)EditorGUILayout.EnumPopup("Gift Bedrock", _target.bedrock);
                _target.level = EditorGUILayout.FloatField("Level", _target.level);
                EditorGUILayout.Space(10);
                _target.mStatus = (CharacterProfile.MaterializedStatus)EditorGUILayout.EnumPopup("Materialized Status", _target.mStatus);
                _target.aType = (CharacterProfile.AugmentationType)EditorGUILayout.EnumPopup("Augmentation Type", _target.aType);
                EditorGUILayout.Space(20);
            }
        
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        else
        {
            _abilityBasics = EditorGUILayout.BeginFoldoutHeaderGroup(_abilityBasics, "基本情报");
            if (_abilityBasics)
            {
                switch (_target.bedrock)
                {
                    case EncodedFunction.Type.Archive:
                        EditorGUILayout.HelpBox("Archive（记忆型）:\nA 类型的编码技能性能提高 10%。", MessageType.Info);
                        break;
                    case EncodedFunction.Type.Behavioral:
                        EditorGUILayout.HelpBox("Behavioral（感知型）:\n所有类型的编码技能性能提高 5%。", MessageType.Info);
                        break;
                    case EncodedFunction.Type.Constructive:
                        EditorGUILayout.HelpBox("Constructive（构造型）：\nC 类型的编码技能性能提高 15%.\nABS 类型的编码技能性能降低5%。", MessageType.Info);
                        break;
                    case EncodedFunction.Type.Stamina:
                        EditorGUILayout.HelpBox("Stamina（体力型）:\nS 类型的编码技能性能提高 15%.\nABC 类型的编码技能性能降低5%。", MessageType.Info);
                        break;
                    default:
                        break;
                }
        
                _target.bedrock = (EncodedFunction.Type)EditorGUILayout.EnumPopup("天赋基石", _target.bedrock);
                _target.level = EditorGUILayout.FloatField("等级", _target.level);
                EditorGUILayout.Space(10);
                _target.mStatus = (CharacterProfile.MaterializedStatus)EditorGUILayout.EnumPopup("物质状态", _target.mStatus);
                _target.aType = (CharacterProfile.AugmentationType)EditorGUILayout.EnumPopup("增幅类型", _target.aType);
                EditorGUILayout.Space(20);
            }
        
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        
    }

    private void DrawPhysicalAugmentationI()
    {
        if(_languageOption == 0) DrawPhysicalAugmentationIEnglish();
        else DrawPhysicalAugmentationIChinese();
    }
    private void DrawPhysicalAugmentationIEnglish()
    {
        _showPA1 = EditorGUILayout.BeginFoldoutHeaderGroup(_showPA1, "Physical Augmentation I.");
        if (_showPA1)
        {
            _target.pa1Active = EditorGUILayout.Toggle("Active", _target.pa1Active);
            if (_target.pa1Active)
            {
                if (_power + _speed + _responsiveness > 7)
                {
                    EditorGUILayout.HelpBox("The sum of PSR are exceeding 7. Please fix it.", MessageType.Error);
                }

                _helpMessage = "All the damages are increased by " + (_power * 10).ToString() + "%.\n";
                _helpMessage += "The speed is increased by " + (_speed * 10).ToString() + "%.\n";
                _helpMessage += "The dodge rate is increased by " + (_responsiveness * 10).ToString() + "%.";

                EditorGUILayout.HelpBox(_helpMessage, MessageType.Info);
                _power = EditorGUILayout.IntSlider("Power", _power, 1, 5);
                _speed = EditorGUILayout.IntSlider("Speed", _speed, 1, 5);
                _responsiveness = EditorGUILayout.IntSlider("Responsiveness", _responsiveness, 1, 5);
                _PSR.x = _power;
                _PSR.y = _speed;
                _PSR.z = _responsiveness;
                _target.pa1Levels = _PSR;

            }
            else
            {
                _target.pa1Levels = Vector3Int.zero;
            }
            EditorGUILayout.Space(20);
        }
        
        EditorGUILayout.EndFoldoutHeaderGroup();
        
    }
    private void DrawPhysicalAugmentationIChinese()
    {
        _showPA1 = EditorGUILayout.BeginFoldoutHeaderGroup(_showPA1, "一段物理强化");
        if (_showPA1)
        {
            _target.pa1Active = EditorGUILayout.Toggle("有效", _target.pa1Active);
            if (_target.pa1Active)
            {
                if (_power + _speed + _responsiveness > 7)
                {
                    EditorGUILayout.HelpBox("力量、速度、反应的总和超过了 7 。请检查并修改。", MessageType.Error);
                }

                _helpMessage = "造成的所有伤害提高 " + (_power * 10).ToString() + "%。\n";
                _helpMessage += "行动速度和出伤速度提高 " + (_speed * 10).ToString() + "%。\n";
                _helpMessage += "闪避率提高 " + (_responsiveness * 10).ToString() + "%。";

                EditorGUILayout.HelpBox(_helpMessage, MessageType.Info);
                _power = EditorGUILayout.IntSlider("力量", _power, 1, 5);
                _speed = EditorGUILayout.IntSlider("速度", _speed, 1, 5);
                _responsiveness = EditorGUILayout.IntSlider("反应", _responsiveness, 1, 5);
                _PSR.x = _power;
                _PSR.y = _speed;
                _PSR.z = _responsiveness;
                _target.pa1Levels = _PSR;

            }
            else
            {
                _target.pa1Levels = Vector3Int.zero;
            }
            EditorGUILayout.Space(20);
        }
        
        EditorGUILayout.EndFoldoutHeaderGroup();
        
    }

    private void DrawSpiritualAugmentationI()
    {
        if (_languageOption == 0) DrawSpiritualAugmentationIEnglish();
        else DrawSpiritualAugmentationIChinese();
    }
    private void DrawSpiritualAugmentationIEnglish()
    {
        _showSA1 = EditorGUILayout.BeginFoldoutHeaderGroup(_showSA1, "Spiritual Augmentation I.");
        if (_sa11 + _sa12+ _sa13 > 6)
        {
            EditorGUILayout.HelpBox("The sum of SA1 are exceeding 6. Please fix it.", MessageType.Error);
        }
        
        if (_showSA1)
        {
            _target.sa11Active = EditorGUILayout.Toggle("1st Active", _target.sa11Active);
            if (_target.sa11Active)
            {
                _sa11 = EditorGUILayout.IntSlider("Level", _sa11, 1, 4);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Encoded Function 1");
                _target.sa11 = EditorGUILayout.ObjectField(_target.sa11, typeof(EncodedFunction), true) as EncodedFunction;
                EditorGUILayout.EndHorizontal();
                // if there is some EF added
                if (_target.sa11 != null)
                {
                    string sa11_name = _target.sa11.chineseName + "(" + _target.sa11.englishName + ")\n";
                    string sa11_level = "Level: " + _sa11.ToString() + "/" + _target.sa11.maxLevel.ToString() + "\n\n";
                    string sa11_description = _target.sa11.description;
                    EditorGUILayout.HelpBox(sa11_name + sa11_level + sa11_description, MessageType.None);
                    if (_sa11 > _target.sa11.maxLevel)
                    {
                        EditorGUILayout.HelpBox("Your level is exceeding the max level.", MessageType.Error);
                    }
                }
                
                EditorGUILayout.Space(10);
                
                _target.sa12Active = EditorGUILayout.Toggle("2nd Active", _target.sa12Active);
                if (_target.sa12Active)
                {
                    _sa12 = EditorGUILayout.IntSlider("Level", _sa12, 1, 4);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Encoded Function 2");
                    _target.sa12 = EditorGUILayout.ObjectField(_target.sa12, typeof(EncodedFunction), true) as EncodedFunction;
                    EditorGUILayout.EndHorizontal();
                    if (_target.sa12 != null)
                    {
                        string sa12_name = _target.sa12.chineseName + "(" + _target.sa12.englishName + ")\n";
                        string sa12_level = "Level: " + _sa12.ToString() + "/" + _target.sa12.maxLevel.ToString() + "\n\n";
                        string sa12_description = _target.sa12.description;
                        EditorGUILayout.HelpBox(sa12_name + sa12_level + sa12_description, MessageType.None);
                        if (_sa12 > _target.sa12.maxLevel)
                        {
                            EditorGUILayout.HelpBox("Your level is exceeding the max level.", MessageType.Error);
                        }
                    }
                    EditorGUILayout.Space(10);
                
                    _target.sa13Active = EditorGUILayout.Toggle("3rd Active", _target.sa13Active);
                    if (_target.sa13Active)
                    {
                        _sa13 = EditorGUILayout.IntSlider("Level", _sa13, 1, 4);
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Encoded Function 3");
                        _target.sa13 = EditorGUILayout.ObjectField(_target.sa13, typeof(EncodedFunction), true) as EncodedFunction;
                        EditorGUILayout.EndHorizontal();
                        if (_target.sa13 != null)
                        {
                            string sa13_name = _target.sa13.chineseName + "(" + _target.sa13.englishName + ")\n";
                            string sa13_level = "Level: " + _sa13.ToString() + "/" + _target.sa13.maxLevel.ToString() + "\n\n";
                            string sa13_description = _target.sa13.description;
                            EditorGUILayout.HelpBox(sa13_name + sa13_level + sa13_description, MessageType.None);
                            if (_sa13 > _target.sa13.maxLevel)
                            {
                                EditorGUILayout.HelpBox("Your level is exceeding the max level.", MessageType.Error);
                            }
                        }
                        EditorGUILayout.Space(10);
                    }
                    else
                    {
                        _sa13 = 0;
                        _target.sa13 = null;
                    }
                
                }
                else
                {
                    _sa12 = 0;
                    _target.sa12 = null;
                }
                
            }
            else
            {
                _sa11 = 0;
                _target.sa11 = null;
            }

            _sa1.x = _sa11;
            _sa1.y = _sa12;
            _sa1.z = _sa13;
            _target.sa1Levels = _sa1;
            EditorGUILayout.Space(20);

        }
        EditorGUILayout.EndFoldoutHeaderGroup();

    }

    private void DrawSpiritualAugmentationIChinese()
    {
        _showSA1 = EditorGUILayout.BeginFoldoutHeaderGroup(_showSA1, "一段精神强化");
        if (_sa11 + _sa12+ _sa13 > 6)
        {
            EditorGUILayout.HelpBox("一段精神强化的各个编码技能等级总和超过了 6 。请检查并修改。", MessageType.Error);
        }
        
        if (_showSA1)
        {
            _target.sa11Active = EditorGUILayout.Toggle("1号编码技能有效位", _target.sa11Active);
            if (_target.sa11Active)
            {
                _sa11 = EditorGUILayout.IntSlider("等级", _sa11, 1, 4);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("编码技能#1");
                _target.sa11 = EditorGUILayout.ObjectField(_target.sa11, typeof(EncodedFunction), true) as EncodedFunction;
                EditorGUILayout.EndHorizontal();
                // if there is some EF added
                if (_target.sa11 != null)
                {
                    string sa11_name = _target.sa11.chineseName + "(" + _target.sa11.englishName + ")\n";
                    string sa11_level = "等级： " + _sa11.ToString() + "/" + _target.sa11.maxLevel.ToString() + "\n\n";
                    string sa11_description = _target.sa11.description;
                    EditorGUILayout.HelpBox(sa11_name + sa11_level + sa11_description, MessageType.None);
                    if (_sa11 > _target.sa11.maxLevel)
                    {
                        EditorGUILayout.HelpBox("该等级超过了该技能的最大等级。", MessageType.Error);
                    }
                }
                
                EditorGUILayout.Space(10);
                
                _target.sa12Active = EditorGUILayout.Toggle("2号编码技能有效位", _target.sa12Active);
                if (_target.sa12Active)
                {
                    _sa12 = EditorGUILayout.IntSlider("等级", _sa12, 1, 4);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("编码技能#2");
                    _target.sa12 = EditorGUILayout.ObjectField(_target.sa12, typeof(EncodedFunction), true) as EncodedFunction;
                    EditorGUILayout.EndHorizontal();
                    if (_target.sa12 != null)
                    {
                        string sa12_name = _target.sa12.chineseName + "(" + _target.sa12.englishName + ")\n";
                        string sa12_level = "等级： " + _sa12.ToString() + "/" + _target.sa12.maxLevel.ToString() + "\n\n";
                        string sa12_description = _target.sa12.description;
                        EditorGUILayout.HelpBox(sa12_name + sa12_level + sa12_description, MessageType.None);
                        if (_sa12 > _target.sa12.maxLevel)
                        {
                            EditorGUILayout.HelpBox("该等级超过了该技能的最大等级。", MessageType.Error);
                        }
                    }
                    EditorGUILayout.Space(10);
                
                    _target.sa13Active = EditorGUILayout.Toggle("3号编码技能有效位", _target.sa13Active);
                    if (_target.sa13Active)
                    {
                        _sa13 = EditorGUILayout.IntSlider("等级", _sa13, 1, 4);
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("编码技能#3");
                        _target.sa13 = EditorGUILayout.ObjectField(_target.sa13, typeof(EncodedFunction), true) as EncodedFunction;
                        EditorGUILayout.EndHorizontal();
                        if (_target.sa13 != null)
                        {
                            string sa13_name = _target.sa13.chineseName + "(" + _target.sa13.englishName + ")\n";
                            string sa13_level = "等级： " + _sa13.ToString() + "/" + _target.sa13.maxLevel.ToString() + "\n\n";
                            string sa13_description = _target.sa13.description;
                            EditorGUILayout.HelpBox(sa13_name + sa13_level + sa13_description, MessageType.None);
                            if (_sa13 > _target.sa13.maxLevel)
                            {
                                EditorGUILayout.HelpBox("该等级超过了该技能的最大等级。", MessageType.Error);
                            }
                        }
                        EditorGUILayout.Space(10);
                    }
                    else
                    {
                        _sa13 = 0;
                        _target.sa13 = null;
                    }
                
                }
                else
                {
                    _sa12 = 0;
                    _target.sa12 = null;
                }
                
            }
            else
            {
                _sa11 = 0;
                _target.sa11 = null;
            }

            _sa1.x = _sa11;
            _sa1.y = _sa12;
            _sa1.z = _sa13;
            _target.sa1Levels = _sa1;
            EditorGUILayout.Space(20);

        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
    private void DrawPhysicalAugmentationII()
    {
        _showPA2 = EditorGUILayout.BeginFoldoutHeaderGroup(_showPA2, "Physical Augmentation II.");
        if (_pa21 + _pa22+ _pa23 > 5)
        {
            EditorGUILayout.HelpBox("The sum of PA2 are exceeding 5. Please fix it.", MessageType.Error);
        }
        
        if (_showPA2)
        {
            _target.pa21Active = EditorGUILayout.Toggle("1st Active", _target.pa21Active);
            if (_target.pa21Active)
            {
                _pa21 = EditorGUILayout.IntSlider("Level", _pa21, 1, 4);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Encoded Function 1");
                _target.pa21 = EditorGUILayout.ObjectField(_target.pa21, typeof(EncodedFunction), true) as EncodedFunction;
                EditorGUILayout.EndHorizontal();
                if (_target.pa21 != null)
                {
                    string pa21_name = _target.pa21.chineseName + "(" + _target.pa21.englishName + ")\n";
                    string pa21_level = "Level: " + _pa21.ToString() + "/" + _target.pa21.maxLevel.ToString() + "\n\n";
                    string pa21_description = _target.pa21.description;
                    EditorGUILayout.HelpBox(pa21_name + pa21_level + pa21_description, MessageType.None);
                    if (_pa21 > _target.pa21.maxLevel)
                    {
                        EditorGUILayout.HelpBox("Your level is exceeding the max level.", MessageType.Error);
                    }
                }
                EditorGUILayout.Space(10);
                
                _target.pa22Active = EditorGUILayout.Toggle("2nd Active", _target.pa22Active);
                if (_target.pa22Active)
                {
                    _pa22 = EditorGUILayout.IntSlider("Level", _pa22, 1, 4);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Encoded Function 2");
                    _target.pa22 = EditorGUILayout.ObjectField(_target.pa22, typeof(EncodedFunction), true) as EncodedFunction;
                    EditorGUILayout.EndHorizontal();
                    if (_target.pa22 != null)
                    {
                        string pa22_name = _target.pa22.chineseName + "(" + _target.pa22.englishName + ")\n";
                        string pa22_level = "Level: " + _pa22.ToString() + "/" + _target.pa22.maxLevel.ToString() + "\n\n";
                        string pa22_description = _target.pa22.description;
                        EditorGUILayout.HelpBox(pa22_name + pa22_level + pa22_description, MessageType.None);
                        if (_pa22 > _target.pa22.maxLevel)
                        {
                            EditorGUILayout.HelpBox("Your level is exceeding the max level.", MessageType.Error);
                        }
                    }
                    EditorGUILayout.Space(10);
                
                    _target.pa23Active = EditorGUILayout.Toggle("3rd Active", _target.pa23Active);
                    if (_target.pa23Active)
                    {
                        _pa23 = EditorGUILayout.IntSlider("Level", _pa23, 1, 4);
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Encoded Function 3");
                        _target.pa23 = EditorGUILayout.ObjectField(_target.pa23, typeof(EncodedFunction), true) as EncodedFunction;
                        EditorGUILayout.EndHorizontal();
                        if (_target.pa23 != null)
                        {
                            string pa23_name = _target.pa23.chineseName + "(" + _target.pa23.englishName + ")\n";
                            string pa23_level = "Level: " + _pa23.ToString() + "/" + _target.pa23.maxLevel.ToString() + "\n\n";
                            string pa23_description = _target.pa23.description;
                            EditorGUILayout.HelpBox(pa23_name + pa23_level + pa23_description, MessageType.None);
                            if (_pa23 > _target.pa23.maxLevel)
                            {
                                EditorGUILayout.HelpBox("Your level is exceeding the max level.", MessageType.Error);
                            }
                        }
                        EditorGUILayout.Space(10);
                    }
                    else
                    {
                        _pa23 = 0;
                        _target.pa23 = null;
                    }
                
                }
                else
                {
                    _pa22 = 0;
                    _target.pa22 = null;
                }
                
            }
            else
            {
                _pa21 = 0;
                _target.pa21 = null;
            }

            _pa2.x = _pa21;
            _pa2.y = _pa22;
            _pa2.z = _pa23;
            _target.pa2Levels = _pa2;
            EditorGUILayout.Space(20);

        }
        EditorGUILayout.EndFoldoutHeaderGroup();

    }
    
    private void DrawSpiritualAugmentationII()
    {
        _showSA2 = EditorGUILayout.BeginFoldoutHeaderGroup(_showSA2, "Spiritual Augmentation II.");
        if (_sa21 + _sa22+ _sa23 > 4)
        {
            EditorGUILayout.HelpBox("The sum of SA2 are exceeding 4. Please fix it.", MessageType.Error);
        }
        
        if (_showSA2)
        {
            _target.sa21Active = EditorGUILayout.Toggle("1st Active", _target.sa21Active);
            if (_target.sa21Active)
            {
                _sa21 = EditorGUILayout.IntSlider("Level", _sa21, 1, 4);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Encoded Function 1");
                _target.sa21 = EditorGUILayout.ObjectField(_target.sa21, typeof(EncodedFunction), true) as EncodedFunction;
                EditorGUILayout.EndHorizontal();
                if (_target.sa21 != null)
                {
                    string sa21_name = _target.sa21.chineseName + "(" + _target.sa21.englishName + ")\n";
                    string sa21_level = "Level: " + _sa21.ToString() + "/" + _target.sa21.maxLevel.ToString() + "\n\n";
                    string sa21_description = _target.sa21.description;
                    EditorGUILayout.HelpBox(sa21_name + sa21_level + sa21_description, MessageType.None);
                    if (_sa21 > _target.sa21.maxLevel)
                    {
                        EditorGUILayout.HelpBox("Your level is exceeding the max level.", MessageType.Error);
                    }
                }
                EditorGUILayout.Space(10);
                
                _target.sa22Active = EditorGUILayout.Toggle("2nd Active", _target.sa22Active);
                if (_target.sa22Active)
                {
                    _sa22 = EditorGUILayout.IntSlider("Level", _sa22, 1, 4);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Encoded Function 2");
                    _target.sa22 = EditorGUILayout.ObjectField(_target.sa22, typeof(EncodedFunction), true) as EncodedFunction;
                    EditorGUILayout.EndHorizontal();
                    if (_target.sa22 != null)
                    {
                        string sa22_name = _target.sa22.chineseName + "(" + _target.sa22.englishName + ")\n";
                        string sa22_level = "Level: " + _sa22.ToString() + "/" + _target.sa22.maxLevel.ToString() + "\n\n";
                        string sa22_description = _target.sa22.description;
                        EditorGUILayout.HelpBox(sa22_name + sa22_level + sa22_description, MessageType.None);
                        if (_sa22 > _target.sa22.maxLevel)
                        {
                            EditorGUILayout.HelpBox("Your level is exceeding the max level.", MessageType.Error);
                        }
                    }
                    EditorGUILayout.Space(10);
                
                    _target.sa23Active = EditorGUILayout.Toggle("3rd Active", _target.sa23Active);
                    if (_target.sa23Active)
                    {
                        _sa23 = EditorGUILayout.IntSlider("Level", _sa23, 1, 4);
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Encoded Function 3");
                        _target.sa23 = EditorGUILayout.ObjectField(_target.sa23, typeof(EncodedFunction), true) as EncodedFunction;
                        EditorGUILayout.EndHorizontal();
                        if (_target.sa23 != null)
                        {
                            string sa23_name = _target.sa23.chineseName + "(" + _target.sa23.englishName + ")\n";
                            string sa23_level = "Level: " + _sa23.ToString() + "/" + _target.sa23.maxLevel.ToString() + "\n\n";
                            string sa23_description = _target.sa23.description;
                            EditorGUILayout.HelpBox(sa23_name + sa23_level + sa23_description, MessageType.None);
                            if (_sa23 > _target.sa23.maxLevel)
                            {
                                EditorGUILayout.HelpBox("Your level is exceeding the max level.", MessageType.Error);
                            }
                        }
                        EditorGUILayout.Space(10);
                    }
                    else
                    {
                        _sa23 = 0;
                        _target.sa23 = null;
                    }
                
                }
                else
                {
                    _sa22 = 0;
                    _target.sa22 = null;
                }
                
            }
            else
            {
                _sa21 = 0;
                _target.sa21 = null;
            }

            _sa2.x = _sa21;
            _sa2.y = _sa22;
            _sa2.z = _sa23;
            _target.sa2Levels = _sa2;
            EditorGUILayout.Space(20);

        }
        EditorGUILayout.EndFoldoutHeaderGroup();

    }

    private void DrawEssenceI()
    {
        if(_languageOption == 0) DrawEssenceIEnglish();
        else DrawEssenceIChinese();
    }
    private void DrawEssenceIEnglish()
    {
        _showE1 = EditorGUILayout.BeginFoldoutHeaderGroup(_showE1, "Essence I.");
        if (_showE1)
        {
            _target.e1Active = EditorGUILayout.Toggle("Active", _target.e1Active);
            if (_target.e1Active)
            {
                _target.e1Level = EditorGUILayout.IntSlider("Level", _target.e1Level, 1, 3);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Essence I");
                _target.essence1 = EditorGUILayout.ObjectField(_target.essence1, typeof(Essence), true) as Essence;
                EditorGUILayout.EndHorizontal();
                if (_target.essence1 != null)
                {
                    string e1_name = _target.essence1.chineseName + "(" + _target.essence1.englishName + ")\n";
                    string e1_level = "Level: " + _target.e1Level.ToString() + "/3" + "\n\n";
                    string e1_description = _target.essence1.description + "\n\n";
                    string e1_replacement = "";
                    for (int i = 0; i < _target.essence1.from.Count; i++)
                    {
                        string header = "The following Encoded Functions have been replaced:\n\n";
                        EncodedFunction old_ef = _target.essence1.from[i];
                        EncodedFunction new_ef = _target.essence1.to[i];
                        string ef_replacement = old_ef.chineseName + " " + old_ef.englishName + "→" +
                                                new_ef.chineseName + " " + new_ef.englishName + "\n";
                        e1_replacement += ef_replacement;
                    }
                    EditorGUILayout.HelpBox(e1_name + e1_level + e1_description + e1_replacement, MessageType.None);
                }
                EditorGUILayout.Space(10);

            }
            EditorGUILayout.Space(20);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
    
    private void DrawEssenceIChinese()
    {
        _showE1 = EditorGUILayout.BeginFoldoutHeaderGroup(_showE1, "一段内核强化");
        if (_showE1)
        {
            _target.e1Active = EditorGUILayout.Toggle("有效", _target.e1Active);
            if (_target.e1Active)
            {
                _target.e1Level = EditorGUILayout.IntSlider("等级", _target.e1Level, 1, 3);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("一段内核强化");
                _target.essence1 = EditorGUILayout.ObjectField(_target.essence1, typeof(Essence), true) as Essence;
                EditorGUILayout.EndHorizontal();
                if (_target.essence1 != null)
                {
                    string e1_name = _target.essence1.chineseName + "(" + _target.essence1.englishName + ")\n";
                    string e1_level = "等级：" + _target.e1Level.ToString() + "/3" + "\n\n";
                    string e1_description = _target.essence1.description + "\n\n";
                    string e1_replacement = "";
                    for (int i = 0; i < _target.essence1.from.Count; i++)
                    {
                        string header = "以下编码函数已被置换：\n\n";
                        EncodedFunction old_ef = _target.essence1.from[i];
                        EncodedFunction new_ef = _target.essence1.to[i];
                        string ef_replacement = old_ef.chineseName + " " + old_ef.englishName + " 将被置换为 " +
                                                new_ef.chineseName + " " + new_ef.englishName + "\n";
                        e1_replacement += ef_replacement;
                    }
                    EditorGUILayout.HelpBox(e1_name + e1_level + e1_description + e1_replacement, MessageType.None);
                }
                EditorGUILayout.Space(10);

            }
            EditorGUILayout.Space(20);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
    
    private void DrawEssenceII()
    {
        if(_languageOption == 0) DrawEssenceIIEnglish();
        else DrawEssenceIIChinese();
    }
    private void DrawEssenceIIEnglish()
    {
        _showE2 = EditorGUILayout.BeginFoldoutHeaderGroup(_showE2, "Essence II.");
        if (_showE2)
        {
            _target.e2Active = EditorGUILayout.Toggle("Active", _target.e2Active);
            if (_target.e2Active)
            {
                _target.e2Level = EditorGUILayout.IntSlider("Level", _target.e2Level, 1, 2);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Essence II");
                _target.essence2 = EditorGUILayout.ObjectField(_target.essence2, typeof(Essence), true) as Essence;
                EditorGUILayout.EndHorizontal();
                if (_target.essence2 != null)
                {
                    string e2_name = _target.essence2.chineseName + "(" + _target.essence2.englishName + ")\n";
                    string e2_level = "Level: " + _target.e2Level.ToString() + "/2" + "\n\n";
                    string e2_description = _target.essence2.description + "\n\n";
                    string e2_replacement = "";
                    for (int i = 0; i < _target.essence2.from.Count; i++)
                    {
                        string header = "The following Encoded Functions have been replaced:\n\n";
                        EncodedFunction old_ef = _target.essence2.from[i];
                        EncodedFunction new_ef = _target.essence2.to[i];
                        string ef_replacement = old_ef.chineseName + " " + old_ef.englishName + "→" +
                                                new_ef.chineseName + " " + new_ef.englishName + "\n";
                        e2_replacement += ef_replacement;
                    }
                    EditorGUILayout.HelpBox(e2_name + e2_level + e2_description + e2_replacement, MessageType.None);
                }
                EditorGUILayout.Space(10);

            }
            EditorGUILayout.Space(20);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
    
    private void DrawEssenceIIChinese()
    {
        _showE2 = EditorGUILayout.BeginFoldoutHeaderGroup(_showE2, "二段内核强化");
        if (_showE2)
        {
            _target.e2Active = EditorGUILayout.Toggle("有效", _target.e2Active);
            if (_target.e2Active)
            {
                _target.e2Level = EditorGUILayout.IntSlider("等级", _target.e2Level, 1, 2);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("二段内核强化");
                _target.essence2 = EditorGUILayout.ObjectField(_target.essence2, typeof(Essence), true) as Essence;
                EditorGUILayout.EndHorizontal();
                if (_target.essence2 != null)
                {
                    string e2_name = _target.essence2.chineseName + "(" + _target.essence2.englishName + ")\n";
                    string e2_level = "Level: " + _target.e2Level.ToString() + "/2" + "\n\n";
                    string e2_description = _target.essence2.description + "\n\n";
                    string e2_replacement = "";
                    for (int i = 0; i < _target.essence2.from.Count; i++)
                    {
                        string header = "以下编码函数已被置换\n\n";
                        EncodedFunction old_ef = _target.essence2.from[i];
                        EncodedFunction new_ef = _target.essence2.to[i];
                        string ef_replacement = old_ef.chineseName + " " + old_ef.englishName + " 将被置换为 " +
                                                new_ef.chineseName + " " + new_ef.englishName + "\n";
                        e2_replacement += ef_replacement;
                    }
                    EditorGUILayout.HelpBox(e2_name + e2_level + e2_description + e2_replacement, MessageType.None);
                }
                EditorGUILayout.Space(10);

            }
            EditorGUILayout.Space(20);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
    
    private void DrawEssenceIII()
    {
        _showE3 = EditorGUILayout.BeginFoldoutHeaderGroup(_showE3, "Essence III.");
        EditorGUILayout.EndFoldoutHeaderGroup();
    }

    public void DrawTimelinesInspector(int languageOption)
    {

    }

    public void DrawRelationshipsInspector(int languageOption)
    {
        if (languageOption == 0)
        {
            EditorGUILayout.HelpBox("Please drag a relationship to the field. Don't make 2 relationships for a single bond.", MessageType.Info);
            //_relationships = new ReorderableList(serializedObject, serializedObject.FindProperty("relationships"), true, true, true, true);
            EditorGUILayout.PropertyField(m_Relationships, new GUIContent("Relationships"));
            serializedObject.ApplyModifiedProperties();
            if (_target.relationships != null)
            {
                if (_target.relationships.Count > 0)
                {
                    foreach (Relationship r in _target.relationships)
                    {
                        if (r != null)
                        {
                            string bonders = r.characterA.firstName + " " + r.characterA.lastName + " & " + r.characterB.firstName +
                                             " " + r.characterB.lastName + "(" + r.relationship + ")\n\n";
                            string descriptions = r.description;
                            EditorGUILayout.HelpBox(bonders+descriptions, MessageType.None);
                        }
                    }
                }
            
            }
        }
        else
        {
            EditorGUILayout.HelpBox("请拖入一个已经建好的 Relationship 物件。请勿给同一个关系建立两个 Relationship 物件。", MessageType.Info);
            //_relationships = new ReorderableList(serializedObject, serializedObject.FindProperty("relationships"), true, true, true, true);
            EditorGUILayout.PropertyField(m_Relationships, new GUIContent("关系网"));
            serializedObject.ApplyModifiedProperties();
            if (_target.relationships != null)
            {
                if (_target.relationships.Count > 0)
                {
                    foreach (Relationship r in _target.relationships)
                    {
                        if (r != null)
                        {
                            string bonders = r.characterA.firstName + " " + r.characterA.lastName + " & " + r.characterB.firstName +
                                             " " + r.characterB.lastName + "(" + r.relationship + ")\n\n";
                            string descriptions = r.description;
                            EditorGUILayout.HelpBox(bonders+descriptions, MessageType.None);
                        }
                    }
                }
            
            }
        }
        
    }
    
    public void DrawBehaviorInspector(int languageOption)
    {

    }
}
