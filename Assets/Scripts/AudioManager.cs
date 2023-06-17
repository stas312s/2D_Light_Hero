using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip gamePlayTrack;
    [SerializeField] private AudioClip menuTrack;
    private AudioSource audioSource;
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get { return instance; }
    }
     private void Awake()
     {
         if (instance == null)
        {
            instance = this;
        }
        else
        {
            
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();

        SceneManager.activeSceneChanged+= OnSceneChanged;
     }
     public void SetVolume(float volume)
     {
        audioSource.volume = volume;
     }
     public float GetVolume()
     {
        return audioSource.volume;
     }
     private void OnSceneChanged(Scene scene1, Scene scene2)
     {
        if(scene2.buildIndex != 0)        audioSource.clip = gamePlayTrack;
        else audioSource.clip = menuTrack;

        audioSource.Play();
     }
}
