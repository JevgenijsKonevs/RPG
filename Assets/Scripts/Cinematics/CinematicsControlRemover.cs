using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;
namespace RPG.Conematics
{
    public class CinematicsControlRemover : MonoBehaviour
    {
        GameObject player;
        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
            // locating the Player
            player = GameObject.FindWithTag("Player");
        }

        // disable players control while the cinematic scene is playing
        void DisableControl(PlayableDirector pd)
        {

            // Stop all actions
            player.GetComponent<ActionSchedule>().CancelCurrentAction();
            // disable the component so that there is no control over it 
            player.GetComponent<PlayerController>().enabled = false;
        }
        // enable players control after the cinematic scene was played
        void EnableControl(PlayableDirector pd)
        {
            // Enabling the control back
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}