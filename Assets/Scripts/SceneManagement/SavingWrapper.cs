using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {

        const string defaultSaveFile = "save";

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
        private void Load()
        {
            // call to saving system load
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
        private void Save()
        {
            // call to saving system save
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
    }
}
