using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobileController : MonoBehaviour
{
    public Player Player;

    private bool _isMoveLeft;

    private void Start()
    {
        Player = Player == null ? GameObject.FindGameObjectWithTag("Player").GetComponent<Player>() : Player; //Player может быть не задан разработчиком. И если он указывает на null, проверяется его наличие как компонента, в противном случае будет установлено значение null (можно заменить на RequireComponent?!)
        if (Player == null)
        {
            Debug.Log("Player not set to keyboard controller");
        }
    }

    private void FixedUpdate()
    {
        if (Player != null) //игрок всё ещё может указывать на null, посему имеет смысл проверка (можно кинуть исключение вместо Debug.Log-а, тогда проверка не понадобится :) )
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
