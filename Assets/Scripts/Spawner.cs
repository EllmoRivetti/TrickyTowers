using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] Tetrominos;
    public GameObject container;
    static int iDBlock = 0;

    private float y_start;

    private int up_distance = 30;

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
        bool goUp = false;
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
        }


    }

    private void UpSpawnnerAndCam()
    {
        UpSpawnner();
        UpCamera();
    }

    private void UpSpawnner()
    {
        //up spawner
        MoveObject(this.transform, new Vector3(this.transform.position.x, this.transform.position.y + up_distance, this.transform.position.z), 0.3f);
    }

    private void UpCamera()
    {
        //up camera
        Debug.Log("Up cam");
        Camera camera = Camera.main;
        MoveObject(camera.transform, new Vector3(camera.transform.position.x, camera.transform.position.y + up_distance, camera.transform.position.z), 0.3f);
    }

    private void DownSpawnnerAndCam()
    {
        DownSpawnner();
        DownCamera();
    }

    private void DownSpawnner()
    {
        //Down spawner
        MoveObject(this.transform, new Vector3(this.transform.position.x, this.transform.position.y - 10, this.transform.position.z), 0.4f);
    }

    private void DownCamera()
    {
        //Down camera
        Camera camera = Camera.main;
        MoveObject(camera.transform, new Vector3(camera.transform.position.x, camera.transform.position.y - 10, camera.transform.position.z), 0.4f);
    }

    private void MoveObject(Transform transform, Vector3 vec, float speed)
    {
        /*Debug.Log("Current pos: " + transform.position);
        Debug.Log("Go to pos: " + vec);
        Debug.Log("Speed: " + speed);
        Debug.Log("-----");*/
        transform.position = Vector3.Lerp(transform.position, vec, speed * Time.deltaTime);
    }

    public int getIdNb()
    {
        return iDBlock;
    }
}
