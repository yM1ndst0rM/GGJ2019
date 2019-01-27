using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

public class GoalLocationController : MonoBehaviour
{
    public bool HasReachedGoal = false;
    public UnityEvent ReachedGoal;
    public GameObject GoalIndicator;

    public Material GoalSpecialMaterial;

    public void Start()
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        var materialArray = meshRenderer.materials;

        materialArray[1] = GoalSpecialMaterial;

        meshRenderer.materials = materialArray;

        GoalIndicator.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, GoalIndicator.transform.position.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !HasReachedGoal)
        {
            HasReachedGoal = true;
            ReachedGoal.Invoke();
        }
    }
}
