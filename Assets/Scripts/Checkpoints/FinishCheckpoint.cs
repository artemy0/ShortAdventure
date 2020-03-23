using UnityEngine;

public class FinishCheckpoint : Checkpoint
{
    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);

        _collider.enabled = false; //при сполкновении коллайдор отключается

        Bag.Instance.AddTrophy();
        _race.StopRace();

        _animator.SetTrigger("Destroy");
    }
}
