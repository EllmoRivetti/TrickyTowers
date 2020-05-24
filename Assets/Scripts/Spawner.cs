using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] Tetrominos;
    public GameObject container;
    static int iDBlock = 0;

    public GameObject Min_distance_controller;

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
            UpSpawnnerAndCam();
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
        MoveObject(this.transform, new Vector3(this.transform.position.x, this.transform.position.y + 3, this.transform.position.z), 0.2f);
        MoveObject(Min_distance_controller.transform, new Vector3(this.transform.position.x, this.transform.position.y + 3, this.transform.position.z), 0.2f);
    }

    private void UpCamera()
    {
        //up camera
        Debug.Log("Up cam");
        Camera camera = Camera.main;
        MoveObject(camera.transform, new Vector3(camera.transform.position.x, camera.transform.position.y + 3, camera.transform.position.z), 0.2f);
    }
   
    private void MoveObject(Transform transform, Vector3 vec, float speed)
    {
        transform.position = Vector3.Lerp(transform.position, vec, speed * Time.deltaTime);
    }
}
