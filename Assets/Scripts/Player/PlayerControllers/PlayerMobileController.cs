using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobileController : MonoBehaviour
{
    public Player Player;

    private bool _isMoveLeft;

    private void Awake()
    {
        Player = Player == null ? GameObject.FindGameObjectWithTag("Player").GetComponent<Player>() : Player;
        if (Player == null)
        {
            Debug.Log("Player not set to mobile/keyboard controller");
        }
    }

    private void FixedUpdate()
    {
        if (Player != null)
        {
            if(_isMoveLeft)
            {
                Player.Move(Vector2.left);
            }
            else
            {
                Player.Move(Vector2.right);
            }
        }
    }

    public void Jump()
    {
        if(Player != null)
        {
            Player.Jump();
        }
    }

    public void MoveLeft()
    {
        _isMoveLeft = true;
    }

    public void MoveRight()
    {
        _isMoveLeft = false;
    }
}
