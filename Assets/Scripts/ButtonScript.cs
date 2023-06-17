using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class ButtonScript : MonoBehaviour
{
     private Button button;
     private TMP_Text text;
    // Start is called before the first frame update
    void Awake()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<TMP_Text>();
        
    }

    public void SetLevel(int level)
    {
        text.text = level.ToString();
        button.onClick.AddListener(()=> SceneManager.LoadScene(level));
    } 
}
