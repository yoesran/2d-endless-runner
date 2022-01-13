using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause_Controller : MonoBehaviour
{
    [SerializeField] GameObject pause_menu;
    public GameObject Audio_Object;
    Image AudioImage;
    public Sprite mute;
    public Sprite no_mute;

    private int Audio;
    private void Start()
    {
        AudioImage = Audio_Object.GetComponent<Image> ();
        Audio = PlayerPrefs.GetInt("Audio");
    }
    private void Update()
    {
        if (PlayerPrefs.HasKey("Audio"))
        {
            Audio = PlayerPrefs.GetInt("Audio");
        }
        else
        {
            PlayerPrefs.SetInt("Audio", 1);
            Audio = PlayerPrefs.GetInt("Audio");
        }
        AudioListener.volume = Audio;
        if (Audio == 1)
        {
            AudioImage.sprite = no_mute;
        }
        else
        {
            AudioImage.sprite = mute;
        }
    }



    public void Pause()
    {
        pause_menu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        pause_menu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Home(int scene_id)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene_id);
    }
    public void ToggleSound()
    {
        if (Audio == 1)
        { 
            PlayerPrefs.SetInt("Audio", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Audio", 1);
        }    
}
}
