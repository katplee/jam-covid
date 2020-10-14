using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isDistantNode : Node
//The logic returns a success if all contacts are at least the value of correct_socDist.
//And returns a failure if at least one of the contacts is at a distance less than correct_socDist from the player.
{
    private float correct_socDist;
    private CharacterAI self;
    private List<CharacterAI> contacts = new List<CharacterAI>();

    public isDistantNode(float correct_socDist, CharacterAI self, List<CharacterAI> contacts)
    {
        this.correct_socDist = correct_socDist;
        this.self = self;
        this.contacts = contacts;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("@ isDistantNode");
        foreach (var contact in contacts)
        {
            float distance = Vector3.Distance(self.transform.position, contact.transform.position);

            if (distance < correct_socDist)
            {
                _nodeState = NodeState.FAILURE;
                return _nodeState;
            }
        }
        _nodeState = NodeState.SUCCESS;
        RestartTimers();
        return _nodeState;
    }

    private void RestartTimers()
    {
        self.FetchStop();
        self.Health_COVID_InfectionCounterOn = false;
        self.Health_COVID_InfectionCounterDone = false;
        self.Health_COVID_InfectionCounterRemaining = 0f;
        self.Health_COVID_CurrentDist = 0f;

    }
}
