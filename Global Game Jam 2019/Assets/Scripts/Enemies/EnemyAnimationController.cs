using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{

    [Range(0,1)] public float moveMagnitude;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementAnimation();
    }

    void MovementAnimation()
    {
        anim.SetFloat("Speed", moveMagnitude);
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    
}
