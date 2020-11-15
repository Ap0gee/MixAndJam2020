using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam {
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

        private Dictionary<string, Item> m_itemPrefabsMap = new Dictionary<string, Item>();

        [SerializeField]
        private List<Item> m_itemPrefabs = new List<Item>();

        private void RegisterItemPrefabs()
        {
            foreach(Item item in m_itemPrefabs) {
                m_itemPrefabsMap.Add(item.Type.ToString(), Instantiate(item));
            }
        }

        public Item GetItemInstance(string name)
        {
            Item instance;
            m_itemPrefabsMap.TryGetValue(name, out instance);

            return instance;
        }

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
            if (ingotCount.text != ingots.ToString())
            {
                ingotCount.text = ingots.ToString();
                ingotAnimation.Play();
                ingotAudio.Play();
            }
        }
    }
}