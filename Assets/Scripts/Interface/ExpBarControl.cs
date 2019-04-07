using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBarControl : MonoBehaviour
{

    const int LEVEL1_EXP = 128;
    const int LEVEL2_EXP = 256;
    const int LEVEL3_EXP = 512;

    
    private int level {get; set;}
    public int exp {get;set;}
    private int expToLvlUp {get;set;}

    public Slider expBar;
    public Text lvlLabel;
    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        exp = 0;
    }

    // Update is called once per frame
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
                expBar.value = (float)((float)exp - (float)LEVEL1_EXP - (float)LEVEL2_EXP)/(float)LEVEL3_EXP;
                break;
            case 4:
                expBar.value = 1;
                break;
        }
    }

    void levelUp()
    {
        level++;
        lvlLabel.text = "Level " + level.ToString();
    }

    public static int currentLevel(int exp) {
            if (exp < LEVEL1_EXP)
                return 1;
            if (exp < LEVEL2_EXP)
                return 2;
            if (exp < LEVEL3_EXP)
                return 3;
            return 4;
        }
}
