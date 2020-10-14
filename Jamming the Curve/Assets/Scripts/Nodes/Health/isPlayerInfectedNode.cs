using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isPlayerInfectedNode : Node
    //The logic returns a success if the player is infected.
    //And returns a failure if the player is not infected.
{
    private CharacterAI self;

    public isPlayerInfectedNode(CharacterAI self)
    {
        this.self = self;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("@ isPlayerInfectedNode");
        if (self.Health_COVID_IsInfected == 0)
        {
            _nodeState = NodeState.FAILURE;
            return _nodeState;
        }
        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
