using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTeam : MonoBehaviour
{
    public Text a1;
    public Text a2;
    public Text a3;
    public int aIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectTeamA()
    {
        a1.text = "Player 1";
        a2.text = "";
        a3.text = "";

    }

    public void selectTeamB()
    {
        a1.text = "";
        a2.text = "";
        a3.text = "Player 1";

    }

    public void selectTeamIdle()
    {
        a1.text = "";
        a2.text = "Player 1";
        a3.text = "";

    }
}
