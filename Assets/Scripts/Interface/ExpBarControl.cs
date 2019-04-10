using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// ---------------------------------------------- 
/// Class:        ExpBarControl - Class to manage the experience 
///                               display UI element
///
/// PROGRAM:      SKOM
///
/// FUNCTIONS:    public void Start()
///               public void Update()
///               void levelUp()
///               public static int currentLevel(int exp)
/// 
/// DATE:         April 3rd, 2019
///
/// REVISIONS:    April 7th, 2019
///                 - Modified to work with exp values sent from server
///
/// DESIGNER:     Simon Chen
///
/// PROGRAMMER:   Simon Chen, Cameron Roberts
///
/// NOTES:
/// ---------------------------------------------- 
public class ExpBarControl : MonoBehaviour
{

    const int LEVEL1_EXP = 128;
    const int LEVEL2_EXP = 256;
    const int LEVEL3_EXP = 512;

    
    private int level;
    public int exp;
    private int expToLvlUp;

    public Slider expBar;
    public Text lvlLabel;

    /// ----------------------------------------------
	/// FUNCTION:	Start()
	///
	/// DATE:		April 3rd, 2019
	///
	/// REVISIONS:  April 7th, 2019
    ///                 - Modified to work with exp values sent from server
    ///
	/// DESIGNER:	Simon Chen
	///
	/// PROGRAMMER:	Simon Chen
	///
	/// INTERFACE: 	void Start()
	///
	/// RETURNS: 	void
	///
	/// NOTES:		Start is called before the first frame update
	/// ----------------------------------------------    
    void Start()
    {
        level = 1;
        exp = 0;
    }

    /// ----------------------------------------------
	/// FUNCTION:	Update()
	///
	/// DATE:		April 3rd, 2019
	///
	/// REVISIONS:  April 7th, 2019
    ///                 - Modified to work with exp values sent from server
    ///
	/// DESIGNER:	Simon Chen
	///
	/// PROGRAMMER:	Simon Chen
	///
	/// INTERFACE: 	void Update()
	///
	/// RETURNS: 	void
	///
	/// NOTES:		Update is called once per frame
	/// ----------------------------------------------  
    void Update()
    {
        if(currentLevel(exp) > level){
            levelUp();
        }
        switch(level){
            case 1:
                expBar.value = (float)((float)exp)/(float)LEVEL1_EXP;
                break;
            case 2:
                expBar.value = (float)((float)exp - (float)LEVEL1_EXP)/(float)LEVEL2_EXP;
                break;
            case 3:
                expBar.value = (float)((float)exp - (float)LEVEL2_EXP)/((float)LEVEL3_EXP - (float)LEVEL2_EXP);
                break;
            case 4:
                expBar.value = 1;
                break;
        }
    }

    /// ----------------------------------------------
	/// FUNCTION:	levelUp()
	///
	/// DATE:		April 3rd, 2019
	///
	/// REVISIONS:  April 7th, 2019
    ///                 - Modified to work with exp values sent from server
    ///
	/// DESIGNER:	Simon Chen
	///
	/// PROGRAMMER:	Simon Chen
	///
	/// INTERFACE: 	void levelUp()
	///
	/// RETURNS: 	void
	///
	/// NOTES:		
	/// ---------------------------------------------- 
    void levelUp()
    {
        level++;
        lvlLabel.text = "Level " + level.ToString();
    }

    /// ----------------------------------------------
	/// FUNCTION:	currentLevel()
	///
	/// DATE:		April 3rd, 2019
	///
	/// REVISIONS:  
    ///
	/// DESIGNER:	Cameron Roberts
	///
	/// PROGRAMMER:	Cameron Roberts
	///
	/// INTERFACE: 	static int currentLevel(int exp)
	///
	/// RETURNS: 	void
	///
	/// NOTES:		
	/// ---------------------------------------------- 
    static int currentLevel(int exp) {
        if (exp < LEVEL1_EXP)
            return 1;
        if (exp < LEVEL2_EXP)
            return 2;
        if (exp < LEVEL3_EXP)
            return 3;
        return 4;
    }
}
