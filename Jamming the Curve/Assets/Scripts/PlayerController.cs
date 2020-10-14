using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform movePoint;

    private float speed = 5.0f;
    private Vector2 inputVector;

    private Animator animator;

    // Start is called before the first frame update
    private void Start(){

        movePoint.parent = null;

        animator = GetComponent<Animator>();
        
    }

    void Update(){

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
                
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");
        
        animator.SetFloat("Horizontal", inputVector.x);
        animator.SetFloat("Vertical", inputVector.y);
        animator.SetFloat("Speed", inputVector.sqrMagnitude);
        

        if(Vector3.Distance(transform.position, movePoint.position) <= 0.05f){
            Move(inputVector);
        }

    }

    private void Move(Vector2 inputVector){
        Vector3 _inputVector = (Vector3) inputVector;
        movePoint.position += _inputVector;
    }
    
}