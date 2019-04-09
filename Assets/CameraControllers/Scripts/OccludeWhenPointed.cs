using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ----------------------------------------------
/// Interface: 	OccludeWhenPointed
///
/// PROGRAM: SKOM
///
/// FUNCTIONS:	TBD
///
/// DATE: 		March 24th, 2019
///
/// REVISIONS:
///
/// DESIGNER: 	Jeffrey Choy
///
/// PROGRAMMER: Jeffrey Choy
///
/// NOTES:
/// ----------------------------------------------
public class OccludeWhenPointed : MonoBehaviour
{
    public bool switched = false; //incase we want to set this from an external function later
    public Material m_Transparent;
    private float occlusionTime = 0.1f;
    private float timer;
    private Material initialMaterial;
    public Renderer m_Renderer;

    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        initialMaterial = m_Renderer.material; //this is the game object original material

    }

    //
    /// ----------------------------------------------
    /// CONSTRUCTOR: ChangeMatAlpha
    ///
    /// DATE: 		March 24th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Jeffrey Choy
    ///
    /// PROGRAMMER:	Jeffrey Choy
    ///
    /// INTERFACE: 	ChangeMatAlpha()
    ///
    /// NOTES: Stores the original material and changes to the transparent occluded material
    /// ----------------------------------------------
    public void ChangeMatAlpha()
    {
        switched = true;
        m_Renderer.material = m_Transparent;
        timer = occlusionTime;
    }

    //
    /// ----------------------------------------------
    /// CONSTRUCTOR: ReturnMat
    /// DATE: 		March 24th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Jeffrey Choy
    ///
    /// PROGRAMMER:	Jeffrey Choy
    ///
    /// INTERFACE: 	ReturnMat()
    ///
    /// NOTES: Removes transparent occluded material and restores the original material
    /// ----------------------------------------------
    private void ReturnMat()
    {
        switched = false;
        m_Renderer.material = initialMaterial;
    }

    /// ----------------------------------------------
    /// CONSTRUCTOR: Update
    ///
    /// DATE: 		March 24th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Jeffrey Choy
    ///
    /// PROGRAMMER:	Jeffrey Choy
    ///
    /// INTERFACE: 	Update()
    ///
    /// NOTES: Called once per tick after all update calls.
    /// ----------------------------------------------
    void Update()
    {
        if (switched)
        {
            //now we check the timer
            if (timer > 0.0f)
                timer = Mathf.Max(0.0f, timer - (1.0f * Time.deltaTime));
            else
            {
                ReturnMat();
            }
        }
    }
}
