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
    public UnityEvent OnDeath;

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

        if (LifeForce <= 0)
        {
            OnDeath.Invoke();
        }
    }


}
