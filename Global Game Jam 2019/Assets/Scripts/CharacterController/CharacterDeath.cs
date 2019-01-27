using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeath : MonoBehaviour
{
    [SerializeField] GameObject particleSystem;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void InitiateDeath()
    {
        GetComponent<CharacterAudio>().PlayerDieSound();
        Instantiate(particleSystem, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
