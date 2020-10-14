using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Government : Base
{

    private float socDist;

    public Government(float socDist)
    {
        this.socDist = socDist;
    }

    public float DispatchSocDistInfo
    {
        get { return socDist; }
    }
}
