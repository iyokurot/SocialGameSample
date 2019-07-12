using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Serialize <GetItem>
{
    [SerializeField]
    List<GetItem> target;
    public List<GetItem> ToList() { return target; }

    public Serialize(List<GetItem> target)
    {
        this.target = target;
    }
}
