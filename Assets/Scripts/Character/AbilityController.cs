
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkLibrary;
using NetworkLibrary.MessageElements;

public class AbilityController : MonoBehaviour
{

    public GameObject Projectile;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseAbility(AbilityType abilityId, float x, float z)
    {
        var projectile = Instantiate(Projectile, transform.position + new Vector3(0, 10, 0), Quaternion.identity);
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());
        Vector3 targetLocation = new Vector3(x, 0, z);
        projectile.GetComponent<Rigidbody>().velocity = (targetLocation - transform.position).normalized;
        projectile.GetComponent<Shot>().creatorId = gameObject.GetComponent<Character>().ActorId;
    }

}