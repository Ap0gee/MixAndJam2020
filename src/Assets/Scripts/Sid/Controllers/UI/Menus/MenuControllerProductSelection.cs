using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam {
    public class MenuControllerProductSelection : MonoBehaviour
    {
        public Image makeIcon;

        public GridLayoutGroup requirementsGroup;

        public GameObject requirementPrefab;

        public void SetIcon(Sprite icon)
        {
            makeIcon.sprite = icon;
        }

        public void AddRequirements(Requirements[] requirements)
        {
            foreach (Requirements requirement in requirements)
            {

            }
        }

        public void ClearRequirements()
        {
            GameObject[] objs = requirementsGroup.gameObject.GetComponents<GameObject>();
            foreach (GameObject obj in objs)
            {
                Destroy(obj);
            }
        }
    }   
}