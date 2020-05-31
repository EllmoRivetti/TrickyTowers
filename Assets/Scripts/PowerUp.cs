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
    //A partir de la liste des tetrominos en jeu et des id des blocks entourant le block main. Une nouvelle liste de tetromino va être crée selon les ID et pour chaque tetromino, un fixed
    // joint sera créer sur main reliant main à ce tetromino
    public void ActivateChains(GameObject main, List<GameObject> blocklist, List<int> listId)
    {
        FixedJoint2D[] fixedJoints;
        int compteur = 0;
        List<GameObject> newList = new List<GameObject>();

        foreach (GameObject go in blocklist)
        {
            if (listId.Contains(go.GetComponent<TetrisBlock>().GetID()))
            {
                newList.Add(go);
            }
        }
        //On ajoute dans un premier temps autant de fixedjoint2D que de tetrominos adjacents
        for (int i = 0; i < newList.Count; i++)
        {
            main.AddComponent<FixedJoint2D>();
        }
        //On récupère la liste de fixedJoint2D afin de pouvoir configurer chaque fixeJoint2D correctement
        fixedJoints = main.GetComponents<FixedJoint2D>();
        //On active les chains pour chaque tetrominos adjacents puis on le connecte au tetromino principal
        foreach (GameObject go in newList)
        {
            go.GetComponent<TetrisBlock>().ActiveChains();
            fixedJoints[compteur].connectedBody = go.GetComponent<Rigidbody2D>();
            compteur++;
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
