using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAI : MonoBehaviour
{
    //Non-player character stats
    [SerializeField] private int char_hasMask;
    [SerializeField] private float char_socDist;
    public List<CharacterAI> char_closestContact;

    //Non-player health stats
    private int health_covid; //health_covid
    [SerializeField] private int health_covid_isInfected; //health_covid_infected
    [SerializeField] private bool health_covid_infectionCounterOn;
    [SerializeField] private bool health_covid_infectionCounterDone;
    [SerializeField] private float health_covid_infectionCounterRemaining;
    [SerializeField] private float health_covid_currentDist;

    //Government stats
    [SerializeField] private Government government;
    private float info_correct_socDist;

    //Player/non-player health reductions
    [SerializeField] private int healthReduc_infected;

    private Node topHealthNode;

    private void Awake()
    {
        RestartTimers();
        SetSocDistRadius();
        //info_correct_socDist = government.DispatchSocDistInfo;        
    }

    // Start is called before the first frame update
    void Start()
    {
        //char_hasMask = Random.Range(0, 2);       
        //health_covid_isInfected = Random.Range(0, 2);               
        info_correct_socDist = 5f;

        ConstructHealthBehaviorTree();
    }

    private void ConstructHealthBehaviorTree()
    {
        safetyNotificationNode safetyNotification = new safetyNotificationNode();
        isPlayerMaskOnNode isPlayerMaskOn = new isPlayerMaskOnNode(this);
        isContMaskOnNode isContMaskOn = new isContMaskOnNode(this, char_closestContact);
        isContInfectedNode isContInfected = new isContInfectedNode(this, char_closestContact);
        isPlayerInfectedNode isPlayerInfected = new isPlayerInfectedNode(this);
        isDistantNode isDistant = new isDistantNode(info_correct_socDist, this, char_closestContact);
        infectionCountdownNode infectionCountdown = new infectionCountdownNode(this, char_closestContact);
        infectedStatusChangeNode infectedStatusChange = new infectedStatusChangeNode(this);
        setHealthNode setHealth = new setHealthNode(this);

        Sequence isPlayerMaskOnSequence = new Sequence(new List<Node> { isPlayerMaskOn, safetyNotification });
        Sequence isContMaskOnSequence = new Sequence(new List<Node> { isContMaskOn, safetyNotification });
        Sequence isContNotInfectedSequence = new Sequence(new List<Node> { isContInfected, safetyNotification });
        Sequence isPlayerInfectedSequence = new Sequence(new List<Node> { isPlayerInfected, safetyNotification });
        Sequence isDistantSequence = new Sequence(new List<Node> { isDistant, safetyNotification });
        Sequence isNotDistantSequence = new Sequence(new List<Node> { infectionCountdown, infectedStatusChange, setHealth });

        Selector ifPlayerNotInfectedSelector = new Selector(new List<Node> { isDistantSequence, isNotDistantSequence });
        Selector ifContInfectedSelector = new Selector(new List<Node> { isPlayerInfectedSequence, ifPlayerNotInfectedSelector });
        Selector ifContMaskOffSelector = new Selector(new List<Node> { isContNotInfectedSequence, ifContInfectedSelector });
        Selector ifPlayerMaskOffSelector = new Selector(new List<Node> { isContMaskOnSequence, ifContMaskOffSelector });

        topHealthNode = new Selector(new List<Node> { isPlayerMaskOnSequence, ifPlayerMaskOffSelector });
    
    }

    // Update is called once per frame
    void Update()
    {
        topHealthNode.Evaluate();
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Colliders_Detected"))
        {
            Debug.Log("A close contact was added.");
            char_closestContact.Add(collision.transform.parent.GetComponent<CharacterAI>());            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("A close contact was removed.");
        char_closestContact.Remove(collision.transform.parent.GetComponent<CharacterAI>());
    }

    #region //Coroutines
    public void FetchStart()
    {
        Debug.Log("@ FetchStart()");
        RawExecuteCountdown();
    }

    private void RawExecuteCountdown()
    {
        
        if (health_covid_infectionCounterRemaining == 0f)
        {
            health_covid_infectionCounterRemaining = health_covid_currentDist;
        }

        float timeDifference = health_covid_infectionCounterRemaining - Time.deltaTime;
        health_covid_infectionCounterRemaining = Mathf.Clamp(timeDifference, 0, 20);

        if (health_covid_infectionCounterRemaining == 0)
        {
            health_covid_infectionCounterDone = true;
            health_covid_infectionCounterOn = false;
        }           
    }

    public void RestartTimers()
    {
        Health_COVID_InfectionCounterOn = false;
        Health_COVID_InfectionCounterDone = false;
        Health_COVID_InfectionCounterRemaining = 0f;
        Health_COVID_CurrentDist = 0f;
    }
    #endregion

    #region /*/Methods
    #region /**/Non-player character stats
    private void SetSocDistRadius()
    {
        CircleCollider2D col = Utilities.FindComponentInChildWithTag<CircleCollider2D>(gameObject, "Colliders_Detecting");
        col.radius = char_socDist;
    }

    
    #endregion
    #endregion

    #region /*/Getters and Setters
    #region /**/Nonplayer character stats
    public int Char_HasMask
    {
        get { return char_hasMask; }
    }
    #endregion

    #region /**/Non-player health stats

    public int Health_COVID
    {
        get { return health_covid; }
        set { health_covid = value; }
    }

    public int Health_COVID_IsInfected
    {
        get { return health_covid_isInfected; }
        set { health_covid_isInfected = value; }
    }

    public bool Health_COVID_InfectionCounterOn
    {
        get { return health_covid_infectionCounterOn; }
        set { health_covid_infectionCounterOn = value; }
    }

    public bool Health_COVID_InfectionCounterDone
    {
        get { return health_covid_infectionCounterDone; }
        set { health_covid_infectionCounterDone = value; }
    }

    public float Health_COVID_InfectionCounterRemaining
    {
        get { return health_covid_infectionCounterRemaining; }
        set { health_covid_infectionCounterRemaining = value; }
    }

    public float Health_COVID_CurrentDist
    {
        get { return health_covid_currentDist; }
        set { health_covid_currentDist = value; }
    }

    #endregion

    #region /**/Government stats
    public float Info_Correct_SocDist
    {
        get { return info_correct_socDist; }
    }
    #endregion

    #region /**/Player/non-player health reductions
    public int HealthReduc_infected
    {
        get { return healthReduc_infected; }
    }
    #endregion
    #endregion
}

public static class Utilities
{
    public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag)
        where T : Component
    {
        Transform t = parent.transform;
        foreach (Transform tr in t)
        {
            if (tr.tag == tag)
            {
                return tr.GetComponent<T>();
            }
        }
        return null;
    }
}

