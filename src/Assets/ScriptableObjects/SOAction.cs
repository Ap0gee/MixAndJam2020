using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Action", menuName = "ScriptableObjects/Action")]
public class SOAction : ScriptableObject
{
    public float actionCallbackDelay = .5f;
}