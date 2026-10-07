using System;
using UnityEngine;

public class LineEffect : MonoBehaviour
{

    public GameObject Line;

    internal Vector2 StartLocation;
    internal Vector2 EndLocation;
    internal float totalTime;
    internal float currentTime;

    internal float extraTime;
    internal float travelmult;
    internal float distance;

    Action PlayHitSound;
    Action CreateHitEffect;    

    public void Setup(Vec2i startLocation, Vec2i endLocation, Actor_Unit target, Action hitSound, Action hitEffect, float travelmultiplier = 0f)
    {
        GeneralSetup(startLocation, endLocation);

        PlayHitSound = hitSound;
        CreateHitEffect = hitEffect;
        travelmult = travelmultiplier;
    }

    private void GeneralSetup(Vec2i startLocation, Vec2i endLocation)
    {
        StartLocation = startLocation;
        EndLocation = endLocation;
        Line.transform.position = StartLocation;
        distance = startLocation.GetDistance(endLocation);
        Debug.Log(distance);
        currentTime = 0;
        totalTime = 0.25f * travelmult;

        float angle = 90 + (float)(Math.Atan2(startLocation.y - endLocation.y, startLocation.x - endLocation.x) * 180 / Math.PI);
        Line.transform.localRotation = Quaternion.Euler(0, 0, angle);
    }



    private void Update()
    {
        if (State.GameManager.TacticalMode.PausedText.activeSelf)
            return;
        if (State.GameManager.CurrentScene != State.GameManager.TacticalMode)
        {
            Destroy(gameObject);
            return;
        }
        currentTime += Time.deltaTime;
        Line.GetComponent<SpriteRenderer>().size = new Vector2(1, Mathf.Lerp(0, distance, currentTime / totalTime));
        //Line.transform.localPosition = new Vector2(StartLocation.x, StartLocation.y + (Mathf.Lerp(0, distance, currentTime / totalTime) - 1)/2);
        if (currentTime > totalTime)
        {
            PlayHitSound?.Invoke();
            CreateHitEffect?.Invoke();
        }
        if (currentTime > totalTime + extraTime)
        {
            Destroy(gameObject);
        }
    }

}

