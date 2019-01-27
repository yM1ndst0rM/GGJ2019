using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayHoverSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Music/Music_Select", transform.position);
    }
}
