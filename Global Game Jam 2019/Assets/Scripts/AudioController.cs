using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string music = "";
    FMOD.Studio.EventInstance musicEv;




    Transform playerTransform;
    Transform targetTransform;


    public float distanceForSound = 10;

    float maxDistance;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        targetTransform = GameObject.FindGameObjectWithTag("Target").GetComponent<Transform>();



        musicEv = FMODUnity.RuntimeManager.CreateInstance(music);
        musicEv.start();

       

        maxDistance = Vector3.Distance(playerTransform.position, targetTransform.position);

    }

    private void Update()
    {
        float distance = 1;
        if (playerTransform != null && targetTransform != null)
        {
            distance = Vector3.Distance(playerTransform.position, targetTransform.position);
        }

        float musicParam = (distance / maxDistance) * 6;
        float actualParam = 7 - musicParam;

        musicEv.setParameterValue("Theme", actualParam);


    }

    


    public void Stop()
    {
        musicEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
