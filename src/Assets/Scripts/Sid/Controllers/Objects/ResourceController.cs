using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    [SerializeField]
    private float m_secondsIngotGenerationDelay = 60;
    [SerializeField]
    private int m_ingotGenerationAmount = 60;

    public int ingots;
    public int wires;
    public int plates;

    public Animation ingotAnimation;
    public AudioSource ingotAudio;
    public Text ingotCount;

    private void GenerateIngots()
    {
        ingots += m_ingotGenerationAmount;
    }

    public void Awake()
    {
        InvokeRepeating("GenerateIngots", 0, m_secondsIngotGenerationDelay);
    }

    private void Update()
    {
        if (ingotCount.text != ingots.ToString()) {
            ingotCount.text = ingots.ToString();
            ingotAnimation.Play();
            ingotAudio.Play();
        }
    }
}