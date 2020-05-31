using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Class Attributes
    public GameObject[] Tetrominos;
    public GameObject container;
    static int iDBlock = 0;

    private float y_start;
    #endregion

    private void Start()
    {
        y_start = this.transform.position.y;
    }

    public GameObject AddTetromino()
    {
        GameObject tetromino = Tetrominos[UnityEngine.Random.Range(0, Tetrominos.Length)];
        tetromino.GetComponent<TetrisBlock>().SetId(iDBlock);
        iDBlock++;
        return Instantiate(tetromino, transform.position, Quaternion.identity, container.transform);
    }

    //Essai de mouvement de caméra avec des raycasts. Jugé trop couteux en ressources, une nouvelle solution a été décidée.
    private void FixedUpdate()
    {
        /*Debug.Log(transform.position);
        Vector2 vec = new Vector2(transform.position.x, transform.position.y - 10);
        RaycastHit2D hitUpCam = Physics2D.Raycast(vec, Vector2.up, 10);

        RaycastHit2D hitDownCam = Physics2D.Raycast(vec, -Vector2.up, 5);

        // If it hits something...
        if (hitUpCam.collider != null && hitUpCam.collider.tag == "set")
        {
            Debug.Log("Brick is above");
            UpSpawnnerAndCam();
        }
        else if(hitDownCam.collider == null && transform.position.y > y_start)
        {
            Debug.Log("No bricks below");
            DownSpawnnerAndCam();
        }*/
        /*bool goUp = false;
        int counter = 0;
        for(int i = -20; i < 19; i++)
        {
            Vector2 vec = new Vector2(i, transform.position.y - 10);
            RaycastHit2D hitUpCam = Physics2D.Raycast(vec, Vector2.up, 10);

            RaycastHit2D hitDownCam = Physics2D.Raycast(vec, -Vector2.up, 5);

            if (hitUpCam.collider != null && hitUpCam.collider.tag == "set")
            {
                Debug.Log("Brick is above");
                goUp = true;
                //UpSpawnnerAndCam();
                break;
            }
            else if (hitDownCam.collider == null && transform.position.y > y_start)
            {
                counter++;
            }
        }

        if(goUp)
        {
            Debug.Log("Go UP");
            UpSpawnnerAndCam();
        }
        else if(counter == 40)
        {
            Debug.Log("No bricks everywhere");
        }*/
    }

    public int getIdNb()
    {
        return iDBlock;
    }
}
