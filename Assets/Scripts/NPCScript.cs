using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : DungeonObject
{
    public enum Disposition { ally, neutral, enemy };//placeholder, probably will have disposition to each character
    public enum Action { attacking, wandering, standing };

    public GameObject target;
    public Disposition currentDisposition = Disposition.neutral;

    public NPCScript(int x, int y, int z)
        : base(x, y, z)
    {

    }

    public Disposition CurrentDisposition
    {
        get
        {
            return currentDisposition;
        }

        set
        {
            currentDisposition = value;
        }
    }




}
