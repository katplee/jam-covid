using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setHealthNode : Node
{
    private CharacterAI self;

    public setHealthNode(CharacterAI self)
    {
        this.self = self;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("@ setHealthNode");
        self.Health_COVID -= self.HealthReduc_infected;

        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
