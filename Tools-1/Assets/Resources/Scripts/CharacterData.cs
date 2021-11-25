using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : ScriptableObject
{
    public GameObject prefab;
    public float maxHealth;
    public float maxEnergy;
    public float criticalChance;
    public float power;
    public string name;
}
