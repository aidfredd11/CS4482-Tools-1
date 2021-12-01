using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Types;
using System;

public class EnemyDesignerWindow : EditorWindow
{
    Texture2D headerSectionTexture;
    Texture2D mageSectionTexture;
    Texture2D warriorSectionTexture;
    Texture2D rogueSectionTexture;

    Color headerSectionColor = new Color(13f / 255f, 32f / 255f, 44f / 255f);

    Rect headerSection;
    Rect mageSection;
    Rect warriorSection;
    Rect rogueSection;

    static MageData mageData;
    static WarriorData warriorData;
    static RogueData rogueData;

    public static MageData mageInfo { get { return mageData; } }
    public static WarriorData warriorInfo { get { return warriorData; } }
    public static RogueData rogueInfo { get { return rogueData; } }

    GUIStyle style;   

    [MenuItem("Window/Enemy Designer")]
    static void OpenWindow()
    {
        EnemyDesignerWindow window = (EnemyDesignerWindow)GetWindow(typeof(EnemyDesignerWindow));
        window.minSize = new Vector2(300, 150);
        window.Show();
    }

    /// <summary>
    /// Similar to Start() or Awake()
    /// </summary>
    private void OnEnable()
    {
        InitTextures();
        InitData();

        style = new GUIStyle();
        style.normal.textColor = Color.black;
    }

    public static void InitData()
    {
        mageData = (MageData)CreateInstance(typeof(MageData));
        warriorData = (WarriorData)CreateInstance(typeof(WarriorData));
        rogueData = (RogueData)CreateInstance(typeof(RogueData));
    }

    /// <summary>
    /// Initialize Texture2d values 
    /// </summary>
    private void InitTextures()
    {
        headerSectionTexture = new Texture2D(1, 1);
        headerSectionTexture.SetPixel(0, 0, headerSectionColor);
        headerSectionTexture.Apply();

        mageSectionTexture = Resources.Load<Texture2D>("icons/editor_mage_texture");
        warriorSectionTexture = Resources.Load<Texture2D>("icons/editor_warrior_texture");
        rogueSectionTexture = Resources.Load<Texture2D>("icons/editor_rogue_texture");
    }

    /// <summary>
    /// Similar to Update(), called 1 or more times per interaction
    /// </summary>
    private void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawMageSettings();
        DrawWarriorSettings();
        DrawRogueSettings();
    }

    /// <summary>
    /// Define Rect values and points textures based on Rects
    /// </summary>
    private void DrawLayouts()
    {
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = position.width;
        headerSection.height = 50;

        mageSection.x = 0;
        mageSection.y = 50;
        mageSection.width = position.width / 3f;
        mageSection.height = position.height - 50;

        warriorSection.x = position.width / 3f;
        warriorSection.y = 50;
        warriorSection.width = position.width / 3f;
        warriorSection.height = position.height - 50;

        rogueSection.x = (position.width / 3f) * 2;
        rogueSection.y = 50;
        rogueSection.width = position.width / 3f;
        rogueSection.height = position.height - 50;

        GUI.DrawTexture(headerSection, headerSectionTexture);
        GUI.DrawTexture(mageSection, mageSectionTexture);
        GUI.DrawTexture(warriorSection, warriorSectionTexture);
        GUI.DrawTexture(rogueSection, rogueSectionTexture);
    }

    /// <summary>
    /// Draw contents of header
    /// </summary>
    private void DrawHeader()
    {
        GUILayout.BeginArea(headerSection);

        GUILayout.Label("Enemy Designer");

        GUILayout.EndArea();
    }

    /// <summary>
    /// Draw contents of mage section
    /// </summary>
    private void DrawMageSettings()
    {
        GUILayout.BeginArea(mageSection);

        GUILayout.Label("Mage", style);

        // Damage
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Damage", style);
        mageData.damageType = (MageDamageType)EditorGUILayout.EnumPopup(mageData.damageType);
        EditorGUILayout.EndHorizontal();

        // Weapon
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon", style);
        mageData.weaponType = (MageWeaponType)EditorGUILayout.EnumPopup(mageData.weaponType);
        EditorGUILayout.EndHorizontal();

        // Generate Button
        if (GUILayout.Button("Generate", GUILayout.Height(25)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.MAGE);
        }

        GUILayout.EndArea();
    }

    /// <summary>
    /// Draw contents of warrior section
    /// </summary>
    private void DrawWarriorSettings()
    {
        GUILayout.BeginArea(warriorSection);

        GUILayout.Label("Warrior", style);

        // Class
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Class", style);
        warriorData.classType = (WarriorClassType)EditorGUILayout.EnumPopup(warriorData.classType);
        EditorGUILayout.EndHorizontal();

        // Weapon
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon", style);
        warriorData.weaponType = (WarriorWeaponType)EditorGUILayout.EnumPopup(warriorData.weaponType);
        EditorGUILayout.EndHorizontal();

        // Generate Button
        if (GUILayout.Button("Generate", GUILayout.Height(25)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.WARRIOR);
        }

        GUILayout.EndArea();
    }

    /// <summary>
    /// Draw contents of rogue section
    /// </summary>
    private void DrawRogueSettings()
    {
        GUILayout.BeginArea(rogueSection);

        GUILayout.Label("Rogue", style);

        // Strategy
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Class", style);
        rogueData.strategyType = (RogueStrategyType)EditorGUILayout.EnumPopup(rogueData.strategyType);
        EditorGUILayout.EndHorizontal();

        // Weapon
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon", style);
        rogueData.weaponType = (RogueWeaponType)EditorGUILayout.EnumPopup(rogueData.weaponType);
        EditorGUILayout.EndHorizontal();

        // Generate Button
        if (GUILayout.Button("Generate", GUILayout.Height(25)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.ROGUE);
        }

        GUILayout.EndArea();
    }
}

public class GeneralSettings : EditorWindow
{
    public enum SettingsType
    {
        MAGE,
        WARRIOR,
        ROGUE
    }
    static SettingsType dataSetting;
    static GeneralSettings window;

    public static void OpenWindow(SettingsType setting)
    {
        dataSetting = setting;
        window = (GeneralSettings)GetWindow(typeof(GeneralSettings));
        window.minSize = new Vector2(250, 200);
        window.Show();
    }

    private void OnGUI()
    {
        switch (dataSetting)
        {
            case SettingsType.MAGE:
                DrawSettings(EnemyDesignerWindow.mageInfo);
                break;
            case SettingsType.WARRIOR:
                DrawSettings(EnemyDesignerWindow.warriorInfo);
                break;
            case SettingsType.ROGUE:
                DrawSettings(EnemyDesignerWindow.rogueInfo);
                break;
        }
    }

    private void DrawSettings(CharacterData characterData)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Prefab");
        characterData.prefab = (GameObject)EditorGUILayout.ObjectField(characterData.prefab, typeof(GameObject), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Max Health");
        characterData.maxHealth = EditorGUILayout.FloatField(characterData.maxHealth);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Max Energy");
        characterData.maxEnergy = EditorGUILayout.FloatField(characterData.maxEnergy);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Power");
        characterData.power = EditorGUILayout.Slider(characterData.power, 0, 100);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("% Critical Chance");
        characterData.criticalChance = EditorGUILayout.Slider(characterData.criticalChance, 0, characterData.power);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name");
        characterData.name = EditorGUILayout.TextField(characterData.name);
        EditorGUILayout.EndHorizontal();

        if (characterData.prefab == null)
        {
            EditorGUILayout.HelpBox("This enemy needs a [Prefab] before it can be created.", MessageType.Warning);

        } else if(characterData.name == null || characterData.name.Length < 1)
        {
            EditorGUILayout.HelpBox("This enemy needs a [Name] before it can be created.", MessageType.Warning);

        }
        else if (GUILayout.Button("Finish and Save", GUILayout.Height(25)))
        {
            SaveCharacterData();
            window.Close();
        }
    }

    private void SaveCharacterData()
    {
        string prefabPath;
        string newPrefabPath = "Assets/Prefabs/Characters/";
        string dataPath = "Assets/Resources/CharacterData/Data/";

        switch (dataSetting)
        {
            case SettingsType.MAGE:

                //create asset file
                dataPath += "mage/" + EnemyDesignerWindow.mageInfo.name + ".asset";
                AssetDatabase.CreateAsset(EnemyDesignerWindow.mageInfo, dataPath);

                newPrefabPath += "mage/" + EnemyDesignerWindow.mageInfo.name + ".prefab";
                // get prefab path
                prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.mageInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject magePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if (!magePrefab.GetComponent<Mage>())
                    magePrefab.AddComponent(typeof(Mage));

                magePrefab.GetComponent<Mage>().mageData = EnemyDesignerWindow.mageInfo;

                break;
            case SettingsType.WARRIOR:

                //create asset file
                dataPath += "warrior/" + EnemyDesignerWindow.warriorInfo.name + ".asset";
                AssetDatabase.CreateAsset(EnemyDesignerWindow.warriorInfo, dataPath);

                newPrefabPath += "warrior/" + EnemyDesignerWindow.warriorInfo.name + ".prefab";
                // get prefab path
                prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.warriorInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject warriorPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if (!warriorPrefab.GetComponent<Warrior>())
                    warriorPrefab.AddComponent(typeof(Warrior));

                warriorPrefab.GetComponent<Warrior>().warriorData = EnemyDesignerWindow.warriorInfo;

                break;
            case SettingsType.ROGUE:

                //create asset file
                dataPath += "rogue/" + EnemyDesignerWindow.rogueInfo.name + ".asset";
                AssetDatabase.CreateAsset(EnemyDesignerWindow.rogueInfo, dataPath);

                newPrefabPath += "rogue/" + EnemyDesignerWindow.rogueInfo.name + ".prefab";
                // get prefab path
                prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.rogueInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject roguePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if (!roguePrefab.GetComponent<Rogue>())
                    roguePrefab.AddComponent(typeof(Rogue));

                roguePrefab.GetComponent<Rogue>().rogueData = EnemyDesignerWindow.rogueInfo;

                break;
        }
    }
}
