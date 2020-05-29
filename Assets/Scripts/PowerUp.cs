﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Powerups
    {
        None,
        Roots,
        Bricks,
        Thunder
    }

    public void ActivateRoot(GameObject main, List<GameObject> blocklist, List<int> listId)
    {
        List<GameObject> newList = new List<GameObject>();
        foreach(GameObject go in blocklist)
        {
            if (listId.Contains(go.GetComponent<TetrisBlock>().GetID()))
            {
                newList.Add(go);
            }
        }

        foreach(GameObject go in newList)
        {
            main.AddComponent<FixedJoint2D>();
            main.GetComponent<FixedJoint2D>().connectedBody = go.GetComponent<Rigidbody2D>();
        }
    }

    public void Brick(GameObject main)
    {
        main.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void Thunder(GameObject lastBlock)
    {
        if(lastBlock != null)
            Destroy(lastBlock);
    }

    

}
