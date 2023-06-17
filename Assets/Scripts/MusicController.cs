using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);
        else 
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
