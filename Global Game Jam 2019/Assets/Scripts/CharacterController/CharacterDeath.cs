using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterDeath : MonoBehaviour
{
    [SerializeField] GameObject particleSystem;
    public MenuController menuController;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void InitiateDeath()
    {
        GetComponent<CharacterAudio>().PlayerDieSound();
        GetComponent<CharacterController>().StopSound();
        Instantiate(particleSystem, transform.position, transform.rotation);
        Destroy(this.gameObject);
        menuController.LoadSceneWithDelay();
    }



   
}
