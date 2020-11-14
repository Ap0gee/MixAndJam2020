using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Managers;

namespace GameJam
{
    [DisallowMultipleComponent]
    public class HighlightEffectController : MonoBehaviour
    {
        public Transform highlightTarget;

        void OnEnable()
        {
            if (highlightTarget == null)
            {
                highlightTarget = transform;
            }
        }
    }
}