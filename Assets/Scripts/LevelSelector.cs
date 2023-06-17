using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private int amountLevel;
    [SerializeField] private ButtonScript buttonPrefab;
    [SerializeField] private Transform buttonContainer;
    private void Start()
    {
        for (int i = 1; i <= amountLevel; i++)
        {
            Instantiate(buttonPrefab, buttonContainer).SetLevel(i);
        
        }
    }
}
