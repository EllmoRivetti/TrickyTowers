using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PowerUp))]
public class GameLoop : MonoBehaviour
{
    public Spawner spawner;
    public Camera camera;

    private List<GameObject> blockList;
    private bool currentBlock = false;

    private float up_distance = 20f;
    private float down_distance = 30f;

    // Start is called before the first frame update
    void Start()
    {
        blockList = new List<GameObject>();
        Spawn();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float max_height = 0;
        foreach (GameObject g in blockList)
        {
            if(g.tag == "set")
            {
                if (max_height < g.transform.position.y)
                {
                    max_height = g.transform.position.y;
                }
            }
        }

        //Debug.Log("Max height: " + max_height);
        //Debug.Log("spawner height: " + spawner.transform.position.y);
        
        if (spawner.transform.position.y - 15 > max_height)
        {
            DownSpawnnerAndCam();
        }
        else if (spawner.transform.position.y - 13 < max_height)
        {
            UpSpawnnerAndCam();
        }
    }

    void Spawn()
    {
        if (!currentBlock)
        {
            GameObject block = spawner.AddTetromino();
            block.GetComponent<TetrisBlock>().TouchGround += OnTouchGround;
            block.GetComponent<TetrisBlock>().TouchRemover += OnTouchRemover;
            blockList.Add(block);
            currentBlock = true;
        }
    }

    void OnTouchGround(object sender, EventArgs e)
    {
        TetrisBlock block = sender as TetrisBlock;
        block.TouchGround -= OnTouchGround;
        currentBlock = false;

        Spawn();        
    }

    void OnTouchRemover(object sender, EventArgs e)
    {
        TetrisBlock block = sender as TetrisBlock;
        GameObject go = sender as GameObject;
        block.TouchRemover -= OnTouchRemover;
        currentBlock = false;
        //Debug.Log("avant " + blockList.Count);
        blockList.Remove(blockList.Find(x => x.GetComponent<TetrisBlock>().GetID()== block.GetID()));
        //Debug.Log("apres " + blockList.Count);
    }

    private void UpSpawnnerAndCam()
    {
        UpSpawnner();
        UpCamera();
    }

    private void UpSpawnner()
    {
        //up spawner
        MoveObject(spawner.transform, new Vector3(this.spawner.transform.position.x, this.spawner.transform.position.y + up_distance, this.spawner.transform.position.z), 0.3f);
    }

    private void UpCamera()
    {
        MoveObject(this.camera.transform, new Vector3(this.camera.transform.position.x, this.camera.transform.position.y + up_distance, this.camera.transform.position.z), 0.3f);
    }

    private void DownSpawnnerAndCam()
    {
        DownSpawnner();
        DownCamera();
    }

    private void DownSpawnner()
    {
        //Down spawner
        MoveObject(this.spawner.transform, new Vector3(this.spawner.transform.position.x, this.spawner.transform.position.y - down_distance, this.spawner.transform.position.z), 0.4f);
    }

    private void DownCamera()
    {
        //Down camera
        MoveObject(this.camera.transform, new Vector3(this.camera.transform.position.x, this.camera.transform.position.y - down_distance, this.camera.transform.position.z), 0.4f);
    }

    private void MoveObject(Transform transform, Vector3 vec, float speed)
    {
        transform.position = Vector3.Lerp(transform.position, vec, speed * Time.deltaTime);
    }
}
