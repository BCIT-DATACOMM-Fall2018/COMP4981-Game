using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    Slider hpBar;

    // Start is called before the first frame update
    void Start()
    {
        hpBar = GameObject.Find("HPBar").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void updateCharacterUI(Status status)
    {
        hpBar.value = status.hp;
    }
}
