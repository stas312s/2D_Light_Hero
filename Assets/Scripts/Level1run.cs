using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1run : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Play()
    {
         SceneManager.LoadScene(1);
         Destroy(GameObject.Find("Audio Source"));
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
