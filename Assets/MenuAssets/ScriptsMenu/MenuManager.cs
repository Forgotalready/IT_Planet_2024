using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

    public class MenuManager : MonoBehaviour

    {

        public void Play()

        {

            SceneManager.LoadScene("SampleScene");

        }

        public void Exit()

        {

            

            #if UNITY_EDITOR

            UnityEditor.EditorApplication.isPlaying = false; 

            

            #elif UNITY_STANDALONE

            Application.Quit(); //выход из игры

            #endif

        }

    }


