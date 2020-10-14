using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infectedStatusChangeNode : Node
{
    private CharacterAI self;

    public infectedStatusChangeNode(CharacterAI self)
    {
        this.self = self;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("@ infectedStatusChangeNode");
        self.Health_COVID_IsInfected = 1;

        _nodeState = (self.Health_COVID_IsInfected == 1) ? NodeState.SUCCESS : NodeState.FAILURE;
        return _nodeState;
    }
}
