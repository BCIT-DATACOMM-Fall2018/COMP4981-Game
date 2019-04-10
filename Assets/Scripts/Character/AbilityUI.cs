using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NetworkLibrary;

public class AbilityUI : MonoBehaviour
{
    GameObject AbilityBar;
    PlayerAbilityController abilityController;
    Slider[] CooldownSliders;
    Image[] AbilityIcons;

	/// ----------------------------------------------
	/// FUNCTION:	Start()
	///
	/// DATE:		April 6th, 2019
	///
	/// REVISIONS:  April 6th, 2019 -- Ian Lo
	///             -Externalized initialization.
	///
	/// DESIGNER:	?????
	///
	/// PROGRAMMER:	????? , Ian Lo
	///
	/// INTERFACE: 	void Start()
	///
	/// RETURNS: 	void
	///
	/// NOTES:		Start is called before the first frame update
	/// ----------------------------------------------
    void Start()
    {
		init ();
		makeCooldownSliders ();
		makeAbilityIcons ();
    }

	/// ----------------------------------------------
	/// FUNCTION:	init()
	///
	/// DATE:		April 6th, 2019
	///
	/// REVISIONS:  
	///
	/// DESIGNER:	Ian Lo
	///
	/// PROGRAMMER:	Ian Lo
	///
	/// INTERFACE: 	void init()
	///
	/// RETURNS: 	void
	///
	/// NOTES:		Externalized initialization of the Ability bar Object
	/// ----------------------------------------------
	private void init()
	{
		if(AbilityBar == null)
			AbilityBar = GameObject.Find("AbilityBar");
		if(abilityController == null)
			abilityController = GetComponent<PlayerAbilityController>();
	}

	/// ----------------------------------------------
	/// FUNCTION:	makeCooldownSliders()
	///
	/// DATE:		April 6th, 2019
	///
	/// REVISIONS:  
	///
	/// DESIGNER:	Ian Lo
	///
	/// PROGRAMMER:	Ian Lo
	///
	/// INTERFACE: 	void makeCooldownSliders)
	///
	/// RETURNS: 	void
	///
	/// NOTES:		Externalized make cooldown Sliders to allow for dynamic creation.
	/// ----------------------------------------------
	private void makeCooldownSliders()
	{
		if (CooldownSliders == null)
		{
			CooldownSliders = new Slider[4];
			CooldownSliders [0] = AbilityBar.transform.Find ("Canvas/Ability1/CooldownSlider").GetComponent<Slider> ();
			CooldownSliders [1] = AbilityBar.transform.Find ("Canvas/Ability2/CooldownSlider").GetComponent<Slider> ();
			CooldownSliders [2] = AbilityBar.transform.Find ("Canvas/Ability3/CooldownSlider").GetComponent<Slider> ();
			CooldownSliders [3] = AbilityBar.transform.Find ("Canvas/Ability4/CooldownSlider").GetComponent<Slider> ();
		}
	}
    
	/// ----------------------------------------------
	/// FUNCTION:	makeAbilityIcons()
	///
	/// DATE:		April 6th, 2019
	///
	/// REVISIONS:  
	///
	/// DESIGNER:	Ian Lo
	///
	/// PROGRAMMER:	Ian Lo
	///
	/// INTERFACE: 	void makeAbilityIcons()
	///
	/// RETURNS: 	void
	///
	/// NOTES:		Externalized make abilities to be called
	///             when required to transform the ability function.
	/// ----------------------------------------------
	private void makeAbilityIcons()
	{
		if (AbilityIcons == null)
		{
			AbilityIcons = new Image[4];
			AbilityIcons [0] = AbilityBar.transform.Find ("Canvas/Ability1/Icon").GetComponent<Image> ();
			AbilityIcons [1] = AbilityBar.transform.Find ("Canvas/Ability2/Icon").GetComponent<Image> ();
			AbilityIcons [2] = AbilityBar.transform.Find ("Canvas/Ability3/Icon").GetComponent<Image> ();
			AbilityIcons [3] = AbilityBar.transform.Find ("Canvas/Ability4/Icon").GetComponent<Image> ();
		}
	}


    // Update is called once per frame
    void Update()
    {
        // Update ability bars
        for (int i = 0; i < CooldownSliders.Length; i++)
        {
            CooldownSliders[i].value = abilityController.Cooldowns[i]/abilityController.MaxCooldowns[i];
        }
    }


	/// ----------------------------------------------
	/// FUNCTION:	UpdateAbilityAssignment
	///
	/// DATE:		April 6th, 2019
	///
	/// REVISIONS:  April 6th, 2019 -- Ian Lo
	///             -performs init, cooldowns, abilities creation.
	///
	/// DESIGNER:	?????
	///
	/// PROGRAMMER:	?????, Simon Wu, Ian Lo
	///
	/// INTERFACE: 	void setAbilityIcon(int slot, AbilityType id)
	///
	/// RETURNS: 	void
	///
	/// NOTES:		??????????????????????????????????
	/// ----------------------------------------------
    public void setAbilityIcon(int slot, AbilityType id) {
		init ();
		makeCooldownSliders ();
		makeAbilityIcons ();
        switch(id) {
            case AbilityType.Wall:
                AbilityIcons[slot].sprite = Resources.Load<Sprite>("AbilityIcons/Normal/wall");
                break;
            case AbilityType.Banish:
                AbilityIcons[slot].sprite = Resources.Load<Sprite>("AbilityIcons/Normal/banish");
                break;
            case AbilityType.BulletAbility:
                AbilityIcons[slot].sprite = Resources.Load<Sprite>("AbilityIcons/Normal/bullet");
                break;
            case AbilityType.PorkChop:
                AbilityIcons[slot].sprite = Resources.Load<Sprite>("AbilityIcons/Normal/porkchop");
                break;
            case AbilityType.Dart:
                AbilityIcons[slot].sprite = Resources.Load<Sprite>("AbilityIcons/Basic/dart");
                break;
            case AbilityType.Purification:
                AbilityIcons[slot].sprite = Resources.Load<Sprite>("AbilityIcons/Normal/purification");
                break;
            case AbilityType.UwuImScared:
                AbilityIcons[slot].sprite = Resources.Load<Sprite>("AbilityIcons/Normal/uwuimscared");
                break;
            case AbilityType.Fireball:
                AbilityIcons[slot].sprite = Resources.Load<Sprite>("AbilityIcons/Ultimate/fireball");
                break;
            case AbilityType.WeebOut:
                AbilityIcons[slot].sprite = Resources.Load<Sprite>("AbilityIcons/Basic/weebout");
                break;
            case AbilityType.Whale:
                AbilityIcons[slot].sprite = Resources.Load<Sprite>("AbilityIcons/Ultimate/whale");
                break;
            case AbilityType.Blink:
                AbilityIcons[slot].sprite = Resources.Load<Sprite>("AbilityIcons/Normal/blink");
                break;
        }
    }
}
