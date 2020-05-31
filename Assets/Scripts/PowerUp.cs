using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Powerups
    {
        None,
        Chains,
        Bricks,
        Thunder
    }

    public void ActivateChains(GameObject main, List<GameObject> blocklist, List<int> listId)
    {
        List<GameObject> newList = new List<GameObject>();
        foreach (GameObject go in blocklist)
        {
            if (listId.Contains(go.GetComponent<TetrisBlock>().GetID()))
            {
                newList.Add(go);
            }
        }

        foreach (GameObject go in newList)
        {
            go.GetComponent<TetrisBlock>().ActiveChains();
            main.AddComponent<FixedJoint2D>();
            main.GetComponent<FixedJoint2D>().connectedBody = go.GetComponent<Rigidbody2D>();
        }
        main.GetComponent<TetrisBlock>().ActiveChains();
    }

    public void ActiveBrick(GameObject main)
    {
        main.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void ActiveThunder(GameObject lastBlock)
    {
        if (lastBlock != null)
            Destroy(lastBlock);
    }
}
