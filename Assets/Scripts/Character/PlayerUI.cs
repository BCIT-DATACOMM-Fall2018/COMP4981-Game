using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    GameObject AbilityBar;
    PlayerAbilityController abilityController;
    Slider[] CooldownSliders;

    // Start is called before the first frame update
    void Start()
    {
        AbilityBar = GameObject.Find("AbilityBar");
        abilityController = GetComponent<PlayerAbilityController>();
        CooldownSliders = new Slider[4];
        CooldownSliders[0] = AbilityBar.transform.Find("Canvas/Ability1/CooldownSlider").GetComponent<Slider>();
        CooldownSliders[1] = AbilityBar.transform.Find("Canvas/Ability2/CooldownSlider").GetComponent<Slider>();
        CooldownSliders[2] = AbilityBar.transform.Find("Canvas/Ability3/CooldownSlider").GetComponent<Slider>();
        CooldownSliders[3] = AbilityBar.transform.Find("Canvas/Ability4/CooldownSlider").GetComponent<Slider>();
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
}
