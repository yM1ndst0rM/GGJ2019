using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerFootStep()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Footsteps", transform.position);
    }

    public void PlayerDieSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Death", transform.position);
    }
}
