using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ethnicity", menuName = "Ethnicitys")]
public class Ethnicity : ScriptableObject
{
    public string ethnicityName;
    public int population;
    public float health;
    public float endurance;
    public float intelligence;
    
}