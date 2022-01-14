using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{
    CoinsController coins;
    public GameObject GameWin;
    CharacterMoveController Player;
    SoundControllerAll sound_controller_all;
    public GameObject boss;
    public GameObject boss_health;
    private int currentScore = 0;
    private int scoreboss;
    public int score_muncul_boss;
    public int level;
    private void Start()
    {
        // reset
        sound_controller_all = FindObjectOfType<SoundControllerAll>();
        Player = FindObjectOfType<CharacterMoveController>();
        coins = FindObjectOfType<CoinsController>();
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if (scoreboss < score_muncul_boss)
        {
            scoreboss = currentScore;
        }
        if (scoreboss == score_muncul_boss)
        {
            if (level == 1)
            {
                Time.timeScale = 0.1f;
                Win();
                scoreboss = score_muncul_boss + 1;
            }
            else
            {
                sound_controller_all.PlayBoss_step();
                Instantiate(boss, new Vector3(Player.transform.position.x + 25, -2.730872f, 0f), Quaternion.identity);
                boss.transform.localScale = new Vector2(0.5f, 0.5f);
                boss_health.SetActive(true);
                scoreboss = score_muncul_boss + 1;
            }
        }
    }

    public float GetCurrentScore()
    {
        return currentScore;
    }

    public void IncreaseCurrentScore(int increment)
    {
        currentScore += increment;
    }
    public void Win()
    {
        coins.FinishCoins();
        Time.timeScale = 0f;
        GameWin.SetActive(true);
        int a = PlayerPrefs.GetInt("level_unlocked");
        if (a <= level)
        {
            PlayerPrefs.SetInt("level_unlocked", level + 1);
        }
    }
    

}
