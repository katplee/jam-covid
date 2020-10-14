using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool anyKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(anyKey = Input.GetKeyDown("space")){
            Debug.Log("A key or mouse click has been detected");
        }
    }
}
