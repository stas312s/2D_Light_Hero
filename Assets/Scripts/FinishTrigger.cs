using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
  
  private void OnTriggerEnter2D()
  {
    uiManager.Win();
  } 
}
