using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ArmyGeneratorWindow : EditorWindow
{
    Texture2D headerSectionTexture;
    Texture2D bodySectionTexture;

    Rect headerSection;
    Rect bodySection;

    GUIStyle textColourStyle;

    Color headerSectionColor = new Color(13f / 255f, 32f / 255f, 44f / 255f);

    public GameObject[] prefabs;

    private Vector3 startPosition;
    int armySize = 1;

    SerializedObject serializedObject;

    [MenuItem("Window/Army Generator")]
    public static void OpenWindow()
    {
        ArmyGeneratorWindow window = (ArmyGeneratorWindow)GetWindow(typeof(ArmyGeneratorWindow));
        window.minSize = new Vector2(300, 150);
        window.Show();
    }
    private void OnEnable()
    {
        InitTextures();

        textColourStyle = new GUIStyle();
        textColourStyle.normal.textColor = Color.black;

        ScriptableObject target = this;
        serializedObject = new SerializedObject(target);
    }
    private void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawGeneratorSettings();
    }

    private void InitTextures()
    {
        headerSectionTexture = new Texture2D(1, 1);
        headerSectionTexture.SetPixel(0, 0, headerSectionColor);
        headerSectionTexture.Apply();

        bodySectionTexture = new Texture2D(1, 1);
        bodySectionTexture.SetPixel(0, 0, Color.gray);
        bodySectionTexture.Apply();
    }
    private void DrawLayouts()
    {
        // Header
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = position.width;
        headerSection.height = 50;

        GUI.DrawTexture(headerSection, headerSectionTexture);

        // Body
        bodySection.x = 0;
        bodySection.y = 50;
        bodySection.width = position.width;
        bodySection.height = position.height - 50;

        GUI.DrawTexture(bodySection, bodySectionTexture);
    }
    private void DrawHeader()
    {
        GUILayout.BeginArea(headerSection);
        // Content
        GUILayout.Label("Army Generator");

        GUILayout.EndArea();
    }

    private void DrawGeneratorSettings()
    {
        GUILayout.BeginArea(bodySection);

        // Content

        GUILayout.Label("Enemy Prefabs", textColourStyle);
        serializedObject.Update();
        SerializedProperty property = serializedObject.FindProperty("prefabs");
        EditorGUILayout.PropertyField(property, true);
        serializedObject.ApplyModifiedProperties();

        startPosition = EditorGUILayout.Vector3Field("Army Start Location", startPosition);

        armySize = EditorGUILayout.IntField("Size of army:", armySize);

        if (GUILayout.Button("Generate", GUILayout.Height(25)))
        {
            // put them in the scene
            LoadPrefabs(prefabs, startPosition, armySize);
        }

        GUILayout.EndArea();
    }

    private void LoadPrefabs(GameObject[] prefabs, Vector3 startPosition, int armySize)
    {
        for (int i = 0; i < armySize; i ++)
        {
            Instantiate(prefabs[Random.Range(0, prefabs.Length)],
                new Vector3(startPosition.x + i *2, startPosition.y, startPosition.z), Quaternion.identity);
        }
    }
}

