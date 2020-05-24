using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class min_distance_manager : MonoBehaviour
{

    public int nbObjInside = 0;
    public GameObject Spawner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "spawner")
        {
            nbObjInside++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "spawner")
        {
            nbObjInside--;
            if (nbObjInside <= 0)
            {
                DownSpawnnerAndCam();
            }
        }
    }

    private void DownSpawnnerAndCam()
    {
        DownSpawnner();
        DownCamera();
    }

    private void DownSpawnner()
    {
        //Down spawner
        MoveObject(this.transform, new Vector3(this.transform.position.x, this.transform.position.y - 10, this.transform.position.z), 0.3f);
        MoveObject(Spawner.transform, new Vector3(this.transform.position.x, this.transform.position.y - 10, this.transform.position.z), 0.3f);
    }

    private void DownCamera()
    {
        //Down camera
        Camera camera = Camera.main;
        MoveObject(camera.transform, new Vector3(camera.transform.position.x, camera.transform.position.y - 10, camera.transform.position.z), 0.3f);
    }

    private void MoveObject(Transform transform, Vector3 vec, float speed)
    {
        transform.position = Vector3.Lerp(transform.position, vec, speed * Time.deltaTime);
    }
}
