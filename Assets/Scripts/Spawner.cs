using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] Tetrominos;
    public GameObject container;
    static int iDBlock = 0;

    public GameObject AddTetromino()
    {
        GameObject tetromino = Tetrominos[UnityEngine.Random.Range(0, Tetrominos.Length)];
        tetromino.GetComponent<TetrisBlock>().SetId(iDBlock);
        iDBlock++;
        return Instantiate(tetromino, transform.position, Quaternion.identity, container.transform);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Collide cam");
        if (collision.gameObject.tag == "set")
        {

            //up spawner
            Vector3 vec = new Vector3(this.transform.position.x, this.transform.position.y + 3, this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, vec, 0.2f * Time.deltaTime);



            //up camera
            Debug.Log("Up cam");
            Camera camera = Camera.main;
            vec = new Vector3(camera.transform.position.x, camera.transform.position.y + 3, camera.transform.position.z);
            camera.transform.position = Vector3.Lerp(camera.transform.position, vec, 0.2f * Time.deltaTime);
        }
    }
}
