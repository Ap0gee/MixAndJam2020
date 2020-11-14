using UnityEngine;
using System;

namespace GameJam {

    public interface IInteractable
    {
        string Name { get; set; }
        Sprite Icon { get; set; }
    }
}