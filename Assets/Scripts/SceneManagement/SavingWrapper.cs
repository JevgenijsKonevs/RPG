using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {

        const string defaultSaveFile = "save";
        [SerializeField] float fadeInTime = 0.2f;
        // on game start load the last scene
        IEnumerator Start() {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
           yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
           yield return fader.FadeIn(fadeInTime);    
        }

        void Update()
        {
            // execute Load() by pressing "L"
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            // execute Save() by pressing "S"
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }
        public void Load()
        {
            // call to saving system load
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
        public void Save()
        {
            // call to saving system save
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
    }
}
