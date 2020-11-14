using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameJam
{
    [Serializable]
    enum MachineTypes
    {
        Fabricator,
        Mill,
        Extruder,
        Press,
        Forge,
        Mining,
        PickNPlace
    }

    public class Machine : MonoBehaviour
    {
        [SerializeField]
        private MachineTypes m_type;

        MachineTypes Type
        {
            get { return m_type; }
        }
    }
}