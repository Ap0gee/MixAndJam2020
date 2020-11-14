using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace GameJam
{
    public class House : MonoBehaviour
    {
        private UnityEvent<float> healthChanged;

        [SerializeField]
        private Image m_healthBar;

        [SerializeField]
        private float m_maxHealth = 100f;

        private float m_health;

        public float Health
        {
            get { return m_health; }
            set { if (value <= 0) { m_health = 0; } else if (m_health >= m_maxHealth) { m_health =  m_maxHealth; } else { m_health = value; } OnHealthChanged(m_health); }
        }

        public bool Destroyed
        {
            get { return m_health == 0; }
        }

        private void Awake()
        {
            m_health = m_maxHealth;
            OnHealthChanged(m_health);
            healthChanged.AddListener(OnHealthChanged);
        }

        private void OnHealthChanged(float newVal)
        {
            m_healthBar.fillAmount = 1 / m_maxHealth * m_health;
        }
    }
}
