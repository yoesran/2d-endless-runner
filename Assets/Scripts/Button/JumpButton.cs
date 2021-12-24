using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JumpButton : MonoBehaviour
{
    CharacterMoveController Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<CharacterMoveController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickBtn()
    {
        Player.JumpButton();
    }
}
