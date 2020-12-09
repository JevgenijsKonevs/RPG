using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool alreadyTriggered = false;
        // when entering the specific ppint - trigger the cinematic scene
        private void OnTriggerEnter(Collider other)
        {
            if (!alreadyTriggered && other.gameObject.tag == "Player")
            {
                GetComponent<PlayableDirector>().Play();
                // set cinematic scene status to "played"
                alreadyTriggered = true;
            }
        }
    }
}
