using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infectionCountdownNode : Node
{

    private CharacterAI self;
    private List<CharacterAI> contacts = new List<CharacterAI>();

    public infectionCountdownNode(CharacterAI self, List<CharacterAI> contacts)
    {
        this.self = self;
        this.contacts = contacts;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("@ infectionCountdownNode");
        bool isSusceptToInfection;
        isSusceptToInfection = IsSusceptToInfection();

        _nodeState = isSusceptToInfection ? NodeState.SUCCESS : NodeState.FAILURE;
        return _nodeState;
    }

    private bool IsSusceptToInfection()
    {
        Debug.Log("@ IsSusceptToInfection()");
        float minDistance = self.Info_Correct_SocDist;
        float countdownDist;

        bool isInfectedAtCountEnd =
             IsInContact(ref minDistance, out countdownDist) && CountdownDone(countdownDist);
        //does the countdownDist processed by the CountdownDone method equal the out variable of the IsInContact method?
        
        return isInfectedAtCountEnd;
    }    

    private bool IsInContact(ref float minDistance, out float countdownDist)
    {
        Debug.Log("@ IsInContact()");
        bool isInContact = false;
        for (int i = 0; i < contacts.Count; i++)
        {
            if (contacts[i].Health_COVID_IsInfected == 1)
            {
                float distance = Vector3.Distance(self.transform.position, contacts[i].transform.position);

                if (distance < minDistance)
                {
                    isInContact = true;
                    minDistance = distance;
                    Debug.Log("minDistance = " + minDistance);
                }
            }         
        }
        countdownDist = minDistance;
        self.Health_COVID_CurrentDist = minDistance;
        
        return isInContact;
    }
    
    private bool CountdownDone(float countdownDist)
    {
        Debug.Log("@ CountdownDone() " + countdownDist);
        if (!self.Health_COVID_InfectionCounterOn)
        {
            if (self.Health_COVID_InfectionCounterDone)
            {
                //calls this when timer is done
                Debug.Log("@ CountdownDone(1)");
                RestartTimers();
                return true;                
            }
            else
            {
                //calls this when timer hasn't started
                Debug.Log("@ CountdownDone(2)");
                RestartTimers();
                CountdownStart(countdownDist);
                return false;
            }

        }

        else
        {            
            Debug.Log("@ CountdownDone(3)");
            if (countdownDist < self.Health_COVID_InfectionCounterRemaining)
            {
                //calls this while a counter is running, to check whether distance has changed
                RestartTimers();
                CountdownStart(countdownDist);
            }
            else
            {
                //calls this to resume the countdown of the running timer
                CountdownStart(countdownDist);
            }
            return false;
        }
    }

    private void RestartTimers()
    {
        self.FetchStop();
        self.Health_COVID_InfectionCounterOn = false;
        self.Health_COVID_InfectionCounterDone = false;
        self.Health_COVID_InfectionCounterRemaining = 0f;
    }

    private void CountdownStart(float countdownDist)
    {
        self.Health_COVID_InfectionCounterOn = true;
        self.Health_COVID_InfectionCounterDone = false;
        self.FetchStart(countdownDist);
    }
}

