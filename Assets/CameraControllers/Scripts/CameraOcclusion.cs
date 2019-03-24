using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOcclusion : MonoBehaviour
{
    public Transform playerPos = null; //inspector assigned variable
    public LayerMask mask = -1; //inspecotr assigned variable, defalut is "Everything"
    public float radiusOffset = 1f;
    float distance = 1.0f;
    float m_radius = 1.0f; //the radius of the sphere being cast

    /// <summary>
    /// Set the Layer Mask in the inspector window
    /// members of this layer mask will need to have a collider and the OccludeWhenPointed script
    /// otherwise they will not change
    /// 
    /// m_sphereCastTarget should be a sphere attached to the player, the size of the thickness
    /// and offset behind the player between the player and camera.
    /// 
    /// this is because the thickness of the sphere will extend beyon the player, causing objects infront of the
    /// player to become transparent.
    /// </summary>
    void Update()
    {
        distance = Vector3.Distance(playerPos.position, transform.position); //Camera distance may change in the future
        //distance -= (m_radius + radiusOffset); //reduce the distance of the SphereCast so objects infront of the player do not become transparent
        distance = Mathf.Max(0.1f, distance); //ensures that m_dist is never less than 0.1f
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, m_radius, fwd, distance, mask);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            OccludeWhenPointed occlude = hit.collider.gameObject.GetComponent<OccludeWhenPointed>();
            if (occlude != null) //make sure what we collided with has the script
            {
                occlude.ChangeMatAlpha();
            }
        }
    }
}
