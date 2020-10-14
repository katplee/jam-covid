using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safetyNotificationNode : Node
{
    public override NodeState Evaluate()
    {
        Debug.Log("You are safe!");

        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
