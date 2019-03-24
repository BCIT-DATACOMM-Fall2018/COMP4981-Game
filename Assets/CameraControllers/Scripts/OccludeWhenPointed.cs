using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

 
    public void ChangeMatAlpha()
    {
        switched = true;
        m_Renderer.material = m_Transparent;
        timer = occlusionTime;
    }

    private void ReturnMat()
    {
        switched = false;
        m_Renderer.material = initialMaterial;
    }

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
