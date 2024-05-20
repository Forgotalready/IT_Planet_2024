// using System.Collections;
// using System.Collections.Generic;
// using System.Diagnostics;
// using UnityEngine.UI;
// using UnityEngine;



// public class GameManager : MonoBehaviour
// {
//     [SerializeField] GameObject manualPanel;
//     [SerializeField] GameObject panicHighPanel;

//     [SerializeField] Button manualeClose;
//     [SerializeField] Button manuale�lue;


//     private void Start()
//     {
//         manualeClose.onClick.AddListener(MainManualHide);
//         manuale�lue.onClick.AddListener(MainManualShow);
//         Invoke("MainManualShow", 2f);  
//     }

//     private void OnEnable()
//     {
//         EventBus.onPanicHight += PanicManualShow;
//         EventBus.onPanicLow += PanicManualHide;
//         EventBus.onManuallOff += MainManualHide;
//         EventBus.onManuallOn += MainManualShow;
//     }

//     private void OnDisable()
//     {
//         EventBus.onPanicHight -= PanicManualShow;
//         EventBus.onPanicLow -= PanicManualHide;
//         EventBus.onManuallOff -= MainManualHide;
//         EventBus.onManuallOn -= MainManualShow;
//     }

//     private void PanicManualShow()
//     {
       
//         panicHighPanel.SetActive(true);
//     }

//     private void PanicManualHide()
//     {
        
//         panicHighPanel.SetActive(false);
//     }

//     private void MainManualShow()
//     {

//         manualPanel.SetActive(true);
//     }

//     private void MainManualHide()
//     {
//         UnityEngine.Debug.Log("I work. Seriously");
//         manualPanel.SetActive(false);
//     }

// }
