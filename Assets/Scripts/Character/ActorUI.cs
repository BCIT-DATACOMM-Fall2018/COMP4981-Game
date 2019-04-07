using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// ----------------------------------------------
/// Class:        ActorUI
///
/// PROGRAM:      Some Kind Of MOBA
///
/// FUNCTIONS:    public void Start()
///               public void Update()
///               public updateCharacterUI
///
/// DATE:         February 7th, 2019
///
/// REVISIONS:
///
/// DESIGNER:     Keishi Asai
///
/// PROGRAMMER:   Keishi Asai, Simon Shoban
///
/// NOTES:
/// Character UI class. Any instruction related to
/// character UI is supposed to be done here.
/// ----------------------------------------------
public class ActorUI : MonoBehaviour
{
    Slider hpBar;
    Slider hpBarSlowFill;
    Slider hpBarSlowDrain;
    Text nameTag;


    /// ----------------------------------------------
    /// FUNCTION:	Start
    ///
    /// DATE:		February 7th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Keishi Asai, Simon Shoban
    ///
    /// PROGRAMMER: Keishi Asai, Simon Shoban
    ///
    /// INTERFACE: 	void Start()
    ///
    /// NOTES:	    Start is called before the first frame update
    /// ----------------------------------------------
    void Start()
    {
        hpBar = gameObject.transform.Find("HPBarObject/Canvas/HPBar").GetComponent<Slider>();
        hpBarSlowFill = gameObject.transform.Find("HPBarObject/Canvas/HPBarSlowFill").GetComponent<Slider>();
        hpBarSlowDrain = gameObject.transform.Find("HPBarObject/Canvas/HPBarSlowDrain").GetComponent<Slider>();
        nameTag = gameObject.transform.Find("HPBarObject/Canvas/Name").GetComponent<Text>();

        if (nameTag == null)
        {
            Debug.Log("nametag is null");
        }

        foreach (var player in ConnectionManager.Instance.playerInfo)
        {
            if (player.Id == GetComponent<Actor>().ActorId)
            {
                nameTag.text = player.Name;

                if (gameObject.tag == "Ally")
                {
                    // Light blue
                    nameTag.color = new Color32(161, 215, 255, 255);
                }
                else
                {
                    // Light red
                    nameTag.color = new Color32(242, 132, 130, 255);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(hpBarSlowFill.value < hpBar.value){
            hpBarSlowFill.value += 10;
            hpBarSlowFill.value = hpBarSlowFill.value > hpBar.value ? hpBar.value : hpBarSlowFill.value;
        }

        if(hpBarSlowDrain.value > hpBar.value){
            hpBarSlowDrain.value -= 10;
            hpBarSlowDrain.value = hpBarSlowDrain.value < hpBar.value ? hpBar.value : hpBarSlowDrain.value;
        }
    }

    public void UpdateCharacterUI(Status status)
    {

        hpBar.value = status.HP;

        if(hpBar.value < hpBarSlowFill.value){
            hpBarSlowFill.value = hpBar.value;
        }

        if(hpBar.value > hpBarSlowDrain.value){
            hpBarSlowDrain.value = hpBar.value;
        }

    }
}
