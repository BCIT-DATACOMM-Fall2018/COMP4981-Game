using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JungleCampController : MonoBehaviour
{
    public GameObject neutralMajor;
    public GameObject neutralMinor;
    public float spawnTime = 60f;
    public Transform spawnOrientation;
    BoxCollider spawnBox;

    // Start is called before the first frame update
    void Start()
    {
        spawnBox = GetComponent<BoxCollider>();
        InvokeRepeating("Spawn", 0, spawnTime);
    }

    void Spawn()
    {
        
        Instantiate(neutralMajor, transform.position + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)), GetSpawnPoint().rotation);
        Instantiate(neutralMinor, transform.position + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)), GetSpawnPoint().rotation);
        Instantiate(neutralMinor, transform.position + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)), GetSpawnPoint().rotation);



    }

    private static Vector3 GetRandomPoint(Vector3 center, Vector3 size)
    {

        return center + new Vector3(
          (Random.value - 0.5f) * size.x,
            0,
           (Random.value - 0.5f) * size.z
        );
   }

    private Transform GetSpawnPoint()
    {
        Transform spawnPoint = transform;
        Vector3 euler = transform.eulerAngles;
        euler.y = Random.Range(0f, 360f);
        spawnPoint.eulerAngles = euler;
        //spawnPoint.position = GetRandomPoint(spawnBox.center, spawnBox.size);


        return spawnPoint;
    }

    private Vector3[] GetColliderVertexPositions(GameObject obj)
    {
        BoxCollider b = obj.GetComponent<BoxCollider>(); //retrieves the Box Collider of the GameObject called obj
        Vector3[] vertices = new Vector3[8];
        vertices[0] = obj.transform.TransformPoint(b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f);
        vertices[1] = obj.transform.TransformPoint(b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f);
        vertices[2] = obj.transform.TransformPoint(b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f);
        vertices[3] = obj.transform.TransformPoint(b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f);
        vertices[4] = obj.transform.TransformPoint(b.center + new Vector3(-b.size.x, b.size.y, -b.size.z) * 0.5f);
        vertices[5] = obj.transform.TransformPoint(b.center + new Vector3(b.size.x, b.size.y, -b.size.z) * 0.5f);
        vertices[6] = obj.transform.TransformPoint(b.center + new Vector3(b.size.x, b.size.y, b.size.z) * 0.5f);
        vertices[7] = obj.transform.TransformPoint(b.center + new Vector3(-b.size.x, b.size.y, b.size.z) * 0.5f);

        return vertices;
    }
}
