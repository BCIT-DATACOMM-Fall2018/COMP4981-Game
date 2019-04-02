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
    // Start is called before the first frame update
    void Start()
    {
        AbilityBar = GameObject.Find("AbilityBar");
        abilityController = GetComponent<PlayerAbilityController>();
        CooldownSliders = new Slider[4];
        AbilityIcons = new Image[4];
        CooldownSliders[0] = AbilityBar.transform.Find("Canvas/Ability1/CooldownSlider").GetComponent<Slider>();
        CooldownSliders[1] = AbilityBar.transform.Find("Canvas/Ability2/CooldownSlider").GetComponent<Slider>();
        CooldownSliders[2] = AbilityBar.transform.Find("Canvas/Ability3/CooldownSlider").GetComponent<Slider>();
        CooldownSliders[3] = AbilityBar.transform.Find("Canvas/Ability4/CooldownSlider").GetComponent<Slider>();

        AbilityIcons[0] = AbilityBar.transform.Find("Canvas/Ability1/Icon").GetComponent<Image>();
        AbilityIcons[1] = AbilityBar.transform.Find("Canvas/Ability2/Icon").GetComponent<Image>();
        AbilityIcons[2] = AbilityBar.transform.Find("Canvas/Ability3/Icon").GetComponent<Image>();
        AbilityIcons[3] = AbilityBar.transform.Find("Canvas/Ability4/Icon").GetComponent<Image>();
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

    void setAbilityIcon(int slot, AbilityType id) {
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
