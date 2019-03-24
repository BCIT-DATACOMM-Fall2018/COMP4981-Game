using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccludeWhenPointed : MonoBehaviour
{
    public bool m_Switched = false; //incase we want to set this from an external function later
    public Material m_Transparent;
    private float m_OcclusionTime = 0.1f;
    private float _Timer;
    private Material m_StartMat;
    public Renderer m_Renderer;

    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        m_StartMat = m_Renderer.material; //this is the game object original material
        //m_Transparent = Resources.Load<Material>("Transparent");
        /*Transparent material must be located inside of the resources folder
         * ./Resources/Transparent.mat
         * or it cannot be loaded and used. Also, the transparent material should be "fade" and not "transparent"
         * or it will how an outline.
         * */
    }

    /// <summary>
    /// Camera will trigger
    /// Function will change alpha and set timer to "m_OcclusiongTime"
    /// This keeps the object transparent as long as the camera ray hits it
    /// Update function will handle transition back
    /// </summary>
    public void ChangeMatAlpha()
    {
        m_Switched = true;
        m_Renderer.material = m_Transparent;
        _Timer = m_OcclusionTime;
    }

    /// <summary>
    /// This function will return the object to it's original material once
    /// time has expired. It also sets the bool m_Switched back to false
    /// </summary>
    private void ReturnMat()
    {
        m_Switched = false;
        m_Renderer.material = m_StartMat;
    }
    // Update is called once per frame
    void Update()
    {
        if (m_Switched)
        {
            //now we check the timer
            if (_Timer > 0.0f)
                _Timer = Mathf.Max(0.0f, _Timer - (1.0f * Time.deltaTime)); //1.0f * Time.deltaTime to get transition over 1 second
            else
            {
                ReturnMat();
            }
        }
    }
}
