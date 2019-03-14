
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkLibrary;
using NetworkLibrary.MessageElements;

/// ---------------------------------------------- 
/// Class:        Character
///
/// PROGRAM:      Some Kind Of MOBA
///
/// FUNCTIONS:    public void Start()
///               public void Update()
///               private void initStatus()
///               private void subtractHP(float damage)
/// 
/// DATE:         February 7th, 2019
///
/// REVISIONS:    
///
/// DESIGNER:     Keishi Asai
///
/// PROGRAMMER:   Keishi Asai
///
/// NOTES:
/// Character class.
/// ---------------------------------------------- 
public class CharacterAbility : MonoBehaviour
{

    public GameObject Projectile;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            Debug.Log("Bang!" + transform.position.x);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            int actorId = gameObject.GetComponent<Character>().actorId;

            if (GameObject.Find("Terrain").GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity)) {
                ConnectionManager.Instance.QueueReliableElement(new AreaAbilityElement(actorId, AbilityType.Placeholder, hit.point.x, hit.point.z));
            }
        }
    }

    public void UseAbility(AbilityType abilityId, float x, float z)
    {
        var projectile = Instantiate(Projectile, transform.position + new Vector3(0, 10, 0), Quaternion.identity);
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());
        Vector3 targetLocation = new Vector3(x, 10, z);
        projectile.GetComponent<Rigidbody>().velocity = (targetLocation - transform.position).normalized;
        projectile.GetComponent<Shot>().creatorId = gameObject.GetComponent<Character>().actorId;
    }

}