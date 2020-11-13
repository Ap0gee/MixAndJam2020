using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;
using GameJam.Managers;

namespace GameJam
{
    [DisallowMultipleComponent]
    public class HighlightEffectController : HighlightEffect
    {
        public Transform highlightTarget;

        public override void OnEnable()
        {
            if (highlightTarget == null)
            {
                highlightTarget = transform;
            }

            if (profile == null)
            {
                profile = ResourceManager.GlobalHighlightProfile;
            }

            profileSync = true;
            SetTarget(highlightTarget);
            ProfileLoad(profile);
            highlighted = false;

            base.OnEnable();
        }

        public override void SetTarget(Transform transform)
        {
            if (transform == target || transform == null)
                return;

            if (highlighted)
            {
                SetHighlighted(false);
            }

            target = transform;
        }
    }
}
