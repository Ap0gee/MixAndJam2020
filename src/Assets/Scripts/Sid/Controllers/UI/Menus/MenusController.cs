using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam
{
    public class MenusController : MonoBehaviour
    {
        [SerializeField]
        private MenuInteractionController m_menuInteraction;

        public MenuInteractionController MenuInteraction
        {
            get { return m_menuInteraction; }
        }
    }
}