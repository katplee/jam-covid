using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sampleScript : Node
{
    public int declaredNumber;
    public int notDeclaredNumber;

    public sampleScript(int declaredNumber)
    {
        this.declaredNumber = declaredNumber;
    }

    public override NodeState Evaluate()
    {
        return NodeState.SUCCESS;
    }

    public int decNumSquare()
    {
        for (int i = 0; i < declaredNumber; i++)
        {
            notDeclaredNumber = i;
            Debug.Log(i);
        }
        return notDeclaredNumber;
    }
}
