using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBarControl : MonoBehaviour
{
    private int level;
    private int exp;
    private int expToLvlUp;

    public Slider expBar;
    public Text lvlLabel;
    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        exp = 50;
        expToLvlUp = 100;
        expBar.value = exp;
        expBar.maxValue = expToLvlUp;

    }

    // Update is called once per frame
    void Update()
    {
        if(exp >= expToLvlUp)
        {
            levelUp();
        }
    }

    public int getLevel()
    {
        return level;
    }

    public int getExp()
    {
        return exp;
    }

    public int getExpToLvlUp()
    {
        return expToLvlUp;
    }

    public void setLevel(int value)
    {
        level = value;
    }

    public void setExp(int value)
    {
        exp = value;
    }

    public void SetExpToLvlUp(int value)
    {
        expToLvlUp = value;
    }

    void levelUp()
    {
        exp = 0;
        expBar.value = exp;
        expToLvlUp += expToLvlUp;
        expBar.maxValue = expToLvlUp;
        level += 1;
        lvlLabel.text = "Level " + level.ToString();
    }
}
