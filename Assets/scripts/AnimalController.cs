using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]



public class AnimalController : MonoBehaviour
{
    public Animator animator;
    private Vector3 lastPos;

    bool charMoved()
    {
        var displacement = transform.position - lastPos;
        lastPos = transform.position;
        //Debug.Log("Moving");
        return displacement.magnitude > 0.001; // return true if char moved 1mm
    }
    
    void Start()
    {
        animator = GetComponent<Animator>();
        lastPos = transform.position;
    }
    void Update()
    {
        bool hasMoved = charMoved();

        if (hasMoved)
        {
            animator.SetBool("isWalking", true);
        }
        else {
            animator.SetBool("isWalking", false);
        }
    }
}