using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOrb : MonoBehaviour
{
    public float healAmout;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerCollector")
        {
            Destroy(this.gameObject);
            other.GetComponentInParent<LifeForceController>().LifeForce += healAmout;
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Light_Charge", transform.position);
        }
    }
}
