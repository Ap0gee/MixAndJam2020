using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace GameJam
{
    [CreateAssetMenu(fileName ="House", menuName ="ScriptableObjects/House")]
    public class SOHouse : ScriptableObject
    {
        private class healthChanged : UnityEvent<float> { }

    }
}
