using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GlobalTeamLife : MonoBehaviour
{
    public Text globalLife;
    private int redLifeCount = 50;
    private int blueLifeCount = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        globalLife.text = "Red " + redLifeCount.ToString() + " | " + "Blue " + blueLifeCount.ToString();
    }
    public int getRedLifeCount()
    {
        return redLifeCount;
    }

    public int getBlueLifeCount()
    {
        return blueLifeCount;
    }

    public void setRedLifeCount(int value)
    {
        redLifeCount = value;
    }

    public void setBlueLifeCount(int value)
    {
        blueLifeCount = value;
    }
}
