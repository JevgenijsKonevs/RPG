using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        // each scene has an ID, which could be viewed by 
        // FILE - BUILD SETTINGS - drag and drop scene to view the number
        // Then change the number on portal element in Unity
        [SerializeField] int sceneToLoad = -1;
        private void OnTriggerEnter(Collider other)
        {
            // chech if the player is entering the trigger
            if (other.tag == "Player")
            {
                // load the scene
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
