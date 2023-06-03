using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimatorByNavAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimationByVelocity();
    }

    void SetAnimationByVelocity()
    {
        Vector3 agentVelocity = OffsetByAngle(agent.velocity.normalized, -45f);
        anim.SetFloat("velX", agentVelocity.x);
        anim.SetFloat("velY", -agentVelocity.z);
    }

    Vector3 OffsetByAngle(Vector3 normalizedVector, float angle)
    {
        float angleRad = angle * Mathf.Deg2Rad;
        float rotatedX = normalizedVector.x * Mathf.Cos(angleRad) - normalizedVector.z * Mathf.Sin(angleRad);
        float rotatedZ = normalizedVector.x * Mathf.Sin(angleRad) + normalizedVector.z * Mathf.Cos(angleRad);

        Vector3 rotatedVector = new Vector3(rotatedX, 0f, rotatedZ);
        return rotatedVector;
    }
}
