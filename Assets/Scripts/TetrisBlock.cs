using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    public Transform[] blocks;
    public LayerMask tetrominoLayer;

    private float m_DragValue = 3;
    public bool m_hasToCollide;
    public int ID;
    float radius;
    List<int> idList;
    bool playable;

    [SerializeField]
    private int m_Max_height;
    List<GameObject> currentCollisions;


    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody2D>().drag = GetDragFromAcceleration(Physics.gravity.magnitude, m_DragValue);
        m_hasToCollide = true;
        tag = "moving";
        ID = FindObjectsOfType<Spawner>()[0].getIdNb();
        currentCollisions = new List<GameObject>();
        radius = (float) (blocks[0].GetComponent<Renderer>().bounds.size[0] * Math.Sqrt(2)/2);
        idList = new List<int>();
        playable = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (playable)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-0.5f, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(0.5f, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //Appel de "transform.TransformPoint" pour passer de coordonnées locale à globale
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                this.GetComponent<Rigidbody2D>().drag = GetDragFromAcceleration(Physics.gravity.magnitude, m_DragValue * 10);
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                this.GetComponent<Rigidbody2D>().drag = GetDragFromAcceleration(Physics.gravity.magnitude, m_DragValue);
            }
        }

        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (m_hasToCollide)
        {
            m_hasToCollide = false;

            ActionOnCollide(col);
        }        
    }

    private void OnCollisionExit2D(Collision2D col)
    {

    }

    public void ActionOnCollide(Collision2D col)
    {
        this.GetComponent<Rigidbody2D>().drag = 0;
        CheckColliders();
        tag = "set";
        TouchGround(this, EventArgs.Empty);
        //Remove script usage
        playable = false;
        //this.enabled = false;
        //this.GetComponent<TetrisBlock>().enabled = false;
      
    }

    public void TouchedRemover()
    {
        TouchRemover(this, EventArgs.Empty);
    }

    public static float GetDrag(float aVelocityChange, float aFinalVelocity)
    {
        return aVelocityChange / ((aFinalVelocity + aVelocityChange) * Time.fixedDeltaTime);
    }
    public static float GetDragFromAcceleration(float aAcceleration, float aFinalVelocity)
    {
        return GetDrag(aAcceleration * Time.fixedDeltaTime, aFinalVelocity);
    }
    public void SetId(int id)
    {
        this.ID = id;
    }
    public int GetID()
    {
        return this.ID;
    }
    public bool IsTetrisBlock(GameObject go)
    {
        return go.GetComponent<TetrisBlock>() != null;
        
    }

    public void CheckColliders()
    {
        List<Collider2D> listCollider = new List<Collider2D>();
        foreach (Transform block in blocks)
        {
            CheckColliderList(listCollider, Physics2D.OverlapCircleAll(block.position, radius, tetrominoLayer));
        }

    }


    public void CheckColliderList(List<Collider2D> listCollider, Collider2D[] array)
    {
        listCollider.AddRange(array);
        if (listCollider.Find(x => x.GetComponent<TetrisBlock>().GetID() == this.GetID()))
            listCollider.RemoveAll(x => x.GetComponent<TetrisBlock>().GetID() == this.GetID());

        foreach(Collider2D col in listCollider)
        {
            if(!idList.Contains(col.GetComponent<TetrisBlock>().GetID()))
            {
                idList.Add(col.GetComponent<TetrisBlock>().GetID());
                Debug.Log(idList.Count);
            }
        }
    }

    public List<int> GetIdList()
    {
        return idList;
    }
    


    public event EventHandler TouchGround;
    public event EventHandler TouchRemover;
}
