using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [Header("Score Highlight")]
    CharacterMoveController Player;
    public int scoreHighlightRange;
    public CharacterSoundController sound;
    public GameObject boss;
    public GameObject boss_health;
    private int currentScore = 0;
    private int lastScoreHighlight = 0;
    private int scoreboss;
    

    private void Start()
    {
        // reset
        Player = FindObjectOfType<CharacterMoveController>();
        currentScore = 0;
        lastScoreHighlight = 0;
    }
    private void Update()
    {
        if (scoreboss < 10)
        {
            scoreboss = currentScore;
        }
        if (scoreboss == 10)
        {
            Instantiate(boss, new Vector3(Player.transform.position.x+25, -2.730872f, 0f), Quaternion.identity);
            boss_health.SetActive(true);
            scoreboss = 100;
        }
            
    }

    public float GetCurrentScore()
    {
        return currentScore;
    }

    public void IncreaseCurrentScore(int increment)
    {
        currentScore += increment;

        if (currentScore - lastScoreHighlight > scoreHighlightRange)
        {
            sound.PlayScoreHighlight();
            lastScoreHighlight += scoreHighlightRange;
        }
    }

    public void FinishScoring()
    {
        // set high score
        if (currentScore > ScoreData.highScore)
        {
            ScoreData.highScore = currentScore;
        }
    }
}
