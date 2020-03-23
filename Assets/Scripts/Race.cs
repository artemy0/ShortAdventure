using UnityEngine;

public class Race : MonoBehaviour
{
    public event System.Action<Race> OnUpdate;

    public float TimeSinceStart { get; private set; }

    private bool _raceIsActive = false;

    public void StartRace()
    {
        if (_raceIsActive == false)
        {
            TimeSinceStart = 0f;
            _raceIsActive = true;
        }
    }

    public void StopRace()
    {
        if (_raceIsActive == true)
        {
            _raceIsActive = false;
        }
    }

    private void Update()
    {
        if (_raceIsActive == true)
        {
            TimeSinceStart += Time.deltaTime;

            OnUpdate?.Invoke(this);
        }
    }
}
