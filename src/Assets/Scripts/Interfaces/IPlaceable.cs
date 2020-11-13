using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam
{
    public interface IPlaceable
    {
        void Place();
        void UnPlace();
    }
}