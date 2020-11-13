using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Action", menuName = "ScriptableObjects/Action")]
public class SOAction : ScriptableObject
{
    public float tweenDuration = .4f;
    public float tweenStart = 120;
    public float tweenTarget = 360f;
    public float actionCallbackDelay = 1f;
    public float backgroundStartFill = .375f;
}