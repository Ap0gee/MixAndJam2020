using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public int ingots;
    public int wires;
    public int plates;

    public Animation ingotAnimation;
    public AudioSource ingotAudio;
    public Text ingotCount;

    private void Update()
    {
        if (ingotCount.text != ingots.ToString()) {
            ingotCount.text = ingots.ToString();
            ingotAnimation.Play();
            ingotAudio.Play();
        }
    }
}