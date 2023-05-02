using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Slider slider;
   
    public void StartButton()
    {
        SceneManager.LoadScene("Main 1");   
    }

    public void SetAudio(float value)
    {
        AudioListener.volume = slider.value;
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
