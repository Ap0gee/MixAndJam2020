using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsRoll : MonoBehaviour
{
    [SerializeField]
    private RectTransform m_credits;
    private Vector3 m_start_pos;

    [SerializeField]
    private float m_rollSpeed;

    private void Awake()
    {
        m_start_pos = m_credits.anchoredPosition;
        m_rollSpeed = 10f;
    }

    private void OnEnable()
    {
        m_credits.anchoredPosition = m_start_pos;

    }

    private void Update()
    {
        var yAdj = Time.deltaTime * m_rollSpeed;
        var newY = m_credits.anchoredPosition.y + yAdj;

        m_credits.anchoredPosition = new Vector3(m_credits.anchoredPosition.x, newY, 0);
    }
}
