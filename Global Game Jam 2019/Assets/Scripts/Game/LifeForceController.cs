using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeForceController : MonoBehaviour
{
    public float MaxLifeForce = 10;
    public float StartLifeForce = 10;
    public float LifeForceDepletionPerSecond = -1;
    public float LifeForceRestorationPerSecond = 1;
    public float LifeForce;
    public bool IsRestoringLifeForce;
    public float lifeLightScale = 0.2f;
    public UnityEvent OnDeath;

    private Light radialLight;
    
    void Awake()
    {
        initLifeLight();
    }
    void initLifeLight()
    {
        Light[] lights = GetComponentsInChildren<Light>();
        foreach (Light l in lights)
            if (l.CompareTag("LifeLight"))
                radialLight = l;
    }

    // Start is called before the first frame update
    void Start()
    {
        LifeForce = StartLifeForce;
        IsRestoringLifeForce = false;
    }

    // Update is called once per frame
    void Update()
    {
        var lifeForceDelta = IsRestoringLifeForce ? LifeForceRestorationPerSecond : LifeForceDepletionPerSecond;
        LifeForce = Mathf.Max(0, LifeForce + lifeForceDelta * Time.deltaTime);

        float radiansAngle = Mathf.Atan( LifeForce / radialLight.transform.position.y) * 2f;
        radialLight.spotAngle = Mathf.Rad2Deg * radiansAngle;
        radialLight.intensity = radialLight.spotAngle * lifeLightScale;

        if (LifeForce <= 0)
        {
            OnDeath.Invoke();
        }
    }


}
