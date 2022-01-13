using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOverController : MonoBehaviour
{
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Shop(int scene_id)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene_id);
    }
    public void Home(int scene_id)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene_id);
    }
    public void Next(int scene_id)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene_id);
    }
}
