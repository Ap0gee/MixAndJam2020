using UnityEngine;
using System.Collections;
using GameJam.Managers;

namespace GameJam.Controllers
{
    public class SplashController : MonoBehaviour
    {
        public float waitTime;
        public string nextScene;

        void Awake() {}

        void Start()
        {
            StartCoroutine(SceneSwitch());
        }

        IEnumerator SceneSwitch()
        {
            yield return new WaitForSeconds(waitTime);

            SwitchScene();
        }

        void Update()
        {
            if (Input.anyKey)
            {
                SwitchScene();
            }
        }

        void SwitchScene()
        {
            SceneStateManager.SwitchScene(nextScene);
        }
    }
}