using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetVolume(float volume)
    {
        AudioManager.Instance.SetVolume(volume);
    } 
    public void SwitchSound()
    {
        if (AudioManager.Instance.GetVolume() > 0)
        {
            slider.value = 0;
        }
        else slider.value = 1;
    }
}
