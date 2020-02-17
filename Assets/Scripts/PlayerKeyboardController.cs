using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardController : MonoBehaviour
{
    public Player Player;

    private void Start()
    {
        Player = Player == null ? GetComponent<Player>() : Player; //Player может быть не задан разработчиком. И если он указывает на null, проверяется его наличие как компонента, в противном случае будет установлено значение null (можно заменить на RequireComponent?!)
        if (Player == null)
        {
            Debug.Log("Player not set to keyboard controller");
        }
    }

    private void FixedUpdate()
    {
        if(Player != null) //игрок всё ещё может указывать на null, посему имеет смысл проверка (можно кинуть исключение вместо Debug.Log-а, тогда проверка не понадобится :) )
        {
            if (Input.GetKey(KeyCode.RightArrow)) //можно использовать .GetAxis(...), тогда необходимость создание разных вариаций скриптов для управления можно опустить (я посчитал это излишней абстракцией, если я правильно понимаю термин "абстракция")
            {
                Player.Move(Vector2.right);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Player.Move(Vector2.left);
            }
            if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            {
                Player.Jump();
            }
        }
    }
}
