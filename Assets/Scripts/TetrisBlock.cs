using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    public Transform[] blocks;

    private float m_DragValue = 3;
    public bool m_hasToCollide;
    public int ID;
    float radius;

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
        radius = (float) (blocks[0].GetComponent<Renderer>().bounds.size[0] * Math.Sqrt(2));

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
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
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.GetComponent<Rigidbody2D>().drag = GetDragFromAcceleration(Physics.gravity.magnitude, m_DragValue * 10);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            this.GetComponent<Rigidbody2D>().drag = GetDragFromAcceleration(Physics.gravity.magnitude, m_DragValue);
        }

        CheckColliders();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("is tetris : " +IsTetrisBlock(col.gameObject));
        if (IsTetrisBlock(col.gameObject))
        {
            if(!currentCollisions.Find(x => x.GetComponent<TetrisBlock>().GetID() == col.gameObject.GetComponent<TetrisBlock>().GetID()))
            {
                currentCollisions.Add(col.gameObject);
                Debug.Log("tetris n " + ID + " added n " + col.gameObject.GetComponent<TetrisBlock>().GetID());
            }
        }
        if (m_hasToCollide)
        {
            m_hasToCollide = false;

            ActionOnCollide(col);
        }        
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (IsTetrisBlock(col.gameObject))
        {
            currentCollisions.Remove(col.gameObject);
            Debug.Log("tetris n "+ID +" removed n " + col.gameObject.GetComponent<TetrisBlock>().GetID());
        }
    }

    public void ActionOnCollide(Collision2D col)
    {
        this.GetComponent<Rigidbody2D>().drag = 0;
        tag = "set";
        TouchGround(this, EventArgs.Empty);
        //Remove script usage
        this.enabled = false;
        this.GetComponent<TetrisBlock>().enabled = false;
      
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
        //Collider[] hitColliders = Physics.OverlapSphere(blocks[0].position, radius).Length;
        //Debug.Log("Block 0 touch : " + Physics.OverlapSphere(this.transform.position, radius*10).Length);
        Debug.Log("Block 1 touch : " + Physics.OverlapSphere(blocks[1].position, radius*10).Length);
        /*Debug.Log("Block 2 touch : " + Physics.OverlapSphere(blocks[2].position, radius).Length);
        Debug.Log("Block 3 touch : " + Physics.OverlapSphere(blocks[3].position, radius).Length);*/


    }

    public event EventHandler TouchGround;
    public event EventHandler TouchRemover;
}
