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

    [SerializeField]
    private GameObject tetrominos_parent;

    private float up_distance;
    private float down_distance;


    //Player Stats
    public int max_health;
    private int current_health;

    private float last_loss_brick_time;

    private float cooldown_duration = 3f;

    private int score ;

    //Instanciation
    void Start()
    {
        blockList = new List<GameObject>();
        
        //Set camera movement distance
        up_distance = 20f;
        down_distance = 30f;

        //Set player stats
        current_health = max_health;
        score = 0;
        last_loss_brick_time = Time.time + cooldown_duration;

        //Spawn first brick
        Spawn();
    }

    public void ResetGame()
    {
        foreach (Transform child in tetrominos_parent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        currentBlock = false;
        Start();
    }

    public void EndGame()
    {
        blockList.Clear();
        foreach (Transform child in tetrominos_parent.transform)
        {
            GameObject.Destroy(child.gameObject);
        };
    }

    #region Game Loop
    void FixedUpdate()
    {
        //Check highest block
        float max_height = 0;
        foreach (GameObject g in blockList)
        {
            if (g.tag == "set")
            {
                if (max_height < g.transform.position.y)
                {
                    max_height = g.transform.position.y;
                }
            }
        }

        //Move camera according to previous check
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

        if (block.transform.position.y + 20 > score)
        {
            this.score = (int)block.transform.position.y + 20;
        }
            

        Spawn();        
    }

    void OnTouchRemover(object sender, EventArgs e)
    {
        TetrisBlock block = sender as TetrisBlock;
        GameObject go = sender as GameObject;
        block.TouchRemover -= OnTouchRemover;
        currentBlock = false;
        blockList.Remove(blockList.Find(x => x.GetComponent<TetrisBlock>().GetID()== block.GetID()));

        if(Time.time > last_loss_brick_time)
        {
            last_loss_brick_time = Time.time + cooldown_duration;
            this.current_health--;
        }
    }

    #endregion

    #region Camera and Spawner utility functions
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
    #endregion

    #region Getters
    public int GetScore()
    {
        return this.score;
    }

    public int GetMaxHealth()
    {
        return this.max_health;
    }

    public int GetCurrentHealth()
    {
        return this.current_health;
    }
    #endregion
}
