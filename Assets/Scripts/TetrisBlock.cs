using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;

    private float m_DragValue = 3;
    public bool m_hasToCollide;
    public int ID;

    [SerializeField]
    private int m_Max_height;
    

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody2D>().drag = GetDragFromAcceleration(Physics.gravity.magnitude, m_DragValue);
        m_hasToCollide = true;
        tag = "moving";
        ID = FindObjectsOfType<Spawner>()[0].getIdNb();

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
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(m_hasToCollide)
        {
            m_hasToCollide = false;

            ActionOnCollide(col);
      
        }
    }

    public void ActionOnCollide(Collision2D col)
    {
        this.GetComponent<Rigidbody2D>().drag = 0;
        tag = "set";
        /*Debug.Log("test");
        Debug.Log(col.gameObject.name);
        if (col.gameObject.name == "base") {
            
            TouchGround(this, EventArgs.Empty);
        }
        else
            TouchRemover(this, EventArgs.Empty);*/

        TouchGround(this, EventArgs.Empty);
        //Remove script usage
        this.enabled = false;
        this.GetComponent<TetrisBlock>().enabled = false;
      
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

    public event EventHandler TouchGround;
    public event EventHandler TouchRemover;
}
