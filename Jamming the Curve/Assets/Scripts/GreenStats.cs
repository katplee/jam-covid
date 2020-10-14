using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenStats : MonoBehaviour
{

    private bool socialPosition; //false: low-income; true: high-income
    
    private bool work;
    private bool work_flex;
    
    private int money;
    
    private float health;
    private int health_covid;
        private bool health_covid_infected; //tells if a person has already contracted the virus
                                            //isInfected
        private int health_covid_medDay;
    private int health_phys;
        private bool health_hunger_freq;
        private bool health_hunger_buyCpty;
        private int health_hunger_remStock;
    private int health_psych;
        private bool health_mental; //decides if person will stay inside or not
    private int health_medCap; //decides at what percent health should be before drinking meds
    private int health_hospCap; //decides at what percent health should be before going to a hospital

    private int info_level;
        private bool info_mask; //hasMask
        private int info_dist;
        private int info_quarantine;
        private int info_hasTelevision;
        private int info_hasRadio;
    
    private int nodePassThrough; //decides how many nodes other than destination player will pass through 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
