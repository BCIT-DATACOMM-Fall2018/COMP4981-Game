using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// ---------------------------------------------- 
/// Class:        CharacterUI
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
/// PROGRAMMER:   Keishi Asai
///
/// NOTES:
/// Character UI class. Any instruction related to
/// character UI is supposed to be done here.
/// ---------------------------------------------- 
public class CharacterUI : MonoBehaviour
{
    Slider hpBar;

    // Start is called before the first frame update
    void Start()
    {
        hpBar = gameObject.transform.Find("HPBarObject/Canvas/HPBar").GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateCharacterUI(Status status)
    {
        hpBar.value = status.HP;
    }
}
