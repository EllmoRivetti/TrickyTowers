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
    private GameObject lastBlock;

    [SerializeField]
    private GameObject tetrominos_parent;
    private PowerUp powerUp;
    public Transform trash;


    private float up_distance;
    private float down_distance;


    //Player Stats
    public int max_health;
    private int current_health;

    private float last_loss_brick_time;

    private float cooldown_duration = 3f;

    private int score ;

    private int next_score_for_pwr;//Next score value needed to have power up 
    private int next_score_for_pwr_add_value = 15; //Value used to add new score objective (default +15)

    private bool root = false;
    private bool bricks = false;

    private PowerUp.Powerups current_powerup;

    



    //Instanciation
    void Start()
    {
        blockList = new List<GameObject>();
        powerUp = this.GetComponent<PowerUp>();
        
        //Set camera movement distance
        up_distance = 20f;
        down_distance = 30f;

        //Set player stats
        current_health = max_health;
        score = 0;
        next_score_for_pwr = next_score_for_pwr_add_value;
        last_loss_brick_time = Time.time + cooldown_duration;

        //Set current power up
        current_powerup = PowerUp.Powerups.Roots;

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
    /*public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (root)
            {
                Debug.Log("root désactivée");
            }
            else
            {
                Debug.Log("root activée");
            }
            root = !root;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (bricks)
            {
                Debug.Log("bricks désactivée");
            }
            else
            {
                Debug.Log("bricks activée");
            }
            bricks = !bricks;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Thunder();
        }
    }*/

    #region Game Loop
    void Update()
    {
        //Check for powerup application
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed");
            switch (this.current_powerup)
            {
                case PowerUp.Powerups.Roots:
                    Debug.Log("Roots !");
                    root = true;
                    break;
                case PowerUp.Powerups.Thunder:
                    Debug.Log("Thunder !");
                    Thunder();
                    break;
                case PowerUp.Powerups.Bricks:
                    Debug.Log("Bricks !");
                    bricks = true;
                    break;
                default:
                    Debug.Log("None...");
                    break;
            }

            this.current_powerup = PowerUp.Powerups.None;
        }

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

        if (block.transform.position.y + 17 > score) // Harcoded value to create a coherent score (pas fou je sais)
        {
            this.score = (int)block.transform.position.y + 17;
            CheckScoreForPowerUp();
        }

        if (root)
        {
            root = false;
            if (block.GetIdList().Count > 0)
                powerUp.ActivateRoot(block.gameObject, blockList, block.GetIdList());
        }
        if (bricks)
        {
            bricks = false;
            powerUp.Brick(block.gameObject);
        }
        lastBlock = block.gameObject;
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

    private void Thunder()
    {
        lastBlock.transform.position = trash.position;
        this.current_health++;
    }

    private void CheckScoreForPowerUp()
    {
        if(this.score >= this.next_score_for_pwr)
        {
            AddPowerUp();
            this.next_score_for_pwr += this.next_score_for_pwr_add_value;
        }
    }

    private void AddPowerUp()
    {
        Array values = Enum.GetValues(typeof(PowerUp.Powerups));
        System.Random random = new System.Random();
        int rand = random.Next(values.Length);
        
        //Avoid getting "None"
        while (rand == 0)
            rand = random.Next(values.Length);

        this.current_powerup = (PowerUp.Powerups)values.GetValue(rand);
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

    public PowerUp.Powerups GetCurrentPowerUp()
    {
        return this.current_powerup;
    }

    public int GetNextScoreForPowerUp()
    {
        return next_score_for_pwr;
    }
    #endregion
}
