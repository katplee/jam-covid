using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isContMaskOnNode : Node
    //The logic returns a success if all contacts are wearing a mask.
    //And returns a failure if at least one of the contacts are not wearing a mask.
{
    private CharacterAI self;
    private List<CharacterAI> contacts = new List<CharacterAI>();

    public isContMaskOnNode(CharacterAI self, List<CharacterAI> contacts)
    {
        this.self = self;
        this.contacts = contacts;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("@ isContMaskOnNode");
        foreach (var contact in contacts)
        {
            if(contact.Char_HasMask == 0)
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
