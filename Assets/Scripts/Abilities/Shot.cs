using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{

    public float speed = 10f;
    public int creatorId;

    private Vector3 start;

    void Start ()
    {
        GetComponent<Rigidbody>().velocity *= speed;
        start = transform.position;
    }

    void Update(){
        if(Vector3.Distance(start, transform.position) > 49){
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.name == "Rock1")
        {
            Destroy(col.gameObject);
        }
    }


}