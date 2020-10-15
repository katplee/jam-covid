using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isContInfectedNode : Node
    //The logic returns a success if none of the contacts are infected.
    //And returns a failure is at least one of the contacts are infected.
{
    private CharacterAI self;
    private List<CharacterAI> contacts = new List<CharacterAI>();

    public isContInfectedNode(CharacterAI self, List<CharacterAI> contacts)
    {
        this.self = self;
        this.contacts = contacts;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("@ isContInfectedNode");
        foreach (var contact in contacts)
        {
            if (contact.Health_COVID_IsInfected == 1)
            {
                _nodeState = NodeState.FAILURE;
                return _nodeState;
            }
        }
        _nodeState = NodeState.SUCCESS;
        self.RestartTimers();
        return _nodeState;
    }
}
