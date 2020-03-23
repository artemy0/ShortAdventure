using UnityEngine;

public class PlayerKeyboardController : MonoBehaviour
{
    public Player Player;

    private void Awake()
    {
        Player = Player == null ? GetComponent<Player>() : Player;
        if (Player == null)
        {
            Debug.Log("Player not set to keyboard controller");
        }
    }

    private void FixedUpdate()
    {
        if(Player != null)
        {
            //можно использовать .GetAxis(...), тогда необходимость создание разных вариаций скриптов для управления можно опустить (я посчитал это излишней абстракцией, если я правильно понимаю термин "абстракция")
            if (Input.GetKey(KeyCode.RightArrow))
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
