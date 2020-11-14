using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private MenusController m_menusController;

        public MenusController MenusController
        {
            get { return m_menusController; }
        }
    }
}