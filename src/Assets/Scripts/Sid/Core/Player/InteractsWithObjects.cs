using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace GameJam {

    [RequireComponent(typeof(ThirdPersonController))]
    public class InteractsWithObjects : MonoBehaviour
    {
        private ThirdPersonController m_thirdPersonController;
        [SerializeField]
        public Sprite onFocusCrosshairSprite;
        private int m_layerMask = 1 << 9; // interactable
        public float m_interationReach = 3f;
        private bool m_hasFocusObject = false;
        private Interactable m_focusObject;

        void Awake()
        {
            m_thirdPersonController = gameObject.GetComponent<ThirdPersonController>();
        }

        // Start is called before the first frame update
        void Start() {
           
        }

        private void FixedUpdate()
        {
            Vector3 rayOrigin = m_thirdPersonController.gameObject.transform.position;
            Vector3 direction = m_thirdPersonController.gameObject.transform.TransformDirection(Vector3.forward);

            Ray ray = new Ray(rayOrigin, direction);
            Debug.DrawRay(ray.origin, m_interationReach * ray.direction, Color.red);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, m_interationReach, m_layerMask))
            {
                Debug.DrawRay(ray.origin, m_interationReach * ray.direction, Color.green);

                Interactable ineractable = hit.transform.GetComponentInParent<Interactable>();

                if (ineractable)
                {
                    if (!m_hasFocusObject)
                    {
                        m_hasFocusObject = true;
                        m_focusObject = ineractable;
                        ineractable.Focus();
                    }
                }
            }
            else
            {
                if (m_hasFocusObject)
                {
                    m_focusObject.UnFocus();
                    m_focusObject = null;
                    m_hasFocusObject = false;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
