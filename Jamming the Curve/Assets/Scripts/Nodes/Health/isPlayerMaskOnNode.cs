using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isPlayerMaskOnNode : Node
    //The logic returns a success if character is wearing a mask.
    //And returns a failure is not wearing a mask.
{
    private CharacterAI self;

    public isPlayerMaskOnNode(CharacterAI self)
    {
        this.self = self;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("@ isPlayerMaskOnNode");
        if (self.Char_HasMask == 0)
        {
            _nodeState = NodeState.FAILURE;
            return _nodeState;       
        }
        _nodeState = NodeState.SUCCESS;
        self.RestartTimers();
        return _nodeState;
    }
}
