using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyDesignerWindow : EditorWindow
{
    Texture2D headerSectionTexture;
    Texture2D mageSectionTexture;
    Texture2D warriorSectionTexture;
    Texture2D rogueSectionTexture;

    Color headerSectionColor = new Color(13f/255f, 32f/255f, 44f/255f);

    Rect headerSection;
    Rect mageSection;
    Rect warriorSection;
    Rect rogueSection;

    [MenuItem("Window/Enemy Designer")]
    static void OpenWindow()
    {
        EnemyDesignerWindow window = (EnemyDesignerWindow)GetWindow(typeof(EnemyDesignerWindow));
        window.minSize = new Vector2(600, 300);
        window.Show(); 
    }

    /// <summary>
    /// Similar to Start() or Awake()
    /// </summary>
    private void OnEnable()
    {
        InitTextures();
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

    }

    /// <summary>
    /// Draw contents of mage section
    /// </summary>
    private void DrawMageSettings()
    {

    }

    /// <summary>
    /// Draw contents of warrior section
    /// </summary>
    private void DrawWarriorSettings()
    {

    }

    /// <summary>
    /// Draw contents of rogue section
    /// </summary>
    private void DrawRogueSettings()
    {

    }
}
