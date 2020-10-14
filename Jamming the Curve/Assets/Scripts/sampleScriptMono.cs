using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sampleScriptMono : MonoBehaviour
{
    sampleScript sampleNode;
    public int result;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sampleNode = new sampleScript(200);
        result = sampleNode.decNumSquare();
    }
}
