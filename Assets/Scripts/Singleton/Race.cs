using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Race : MonoBehaviour
{
    public float TimeSinceStart { get; private set; }

    public static Race Instance { get; private set; }

    public event System.Action<Race> OnUpdate;

    private bool _raceIsActive = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance.gameObject != gameObject)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance.gameObject == gameObject)
        {
            Instance = null;
        }
    }

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
            OnUpdate(this);
        }
    }
}
