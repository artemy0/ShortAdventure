using UnityEngine;

public class StartCheckpoint : Checkpoint
{
    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);

        if (transform.position.x > collision.transform.position.x) //is the player to the left of the start
        {
            _race.StartRace();
        }
        else
        {
            _collider.isTrigger = false; //make the object impossible to pass
        }
    }

    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);

        _collider.isTrigger = true; //make the object possible to pass
    }
}
