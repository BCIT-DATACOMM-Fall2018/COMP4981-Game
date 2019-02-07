using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Status
{
    public float hp;
}

public class Character : MonoBehaviour
{
    private CharacterUI cUI;
    private Status status;

    // Start is called before the first frame update
    void Start()
    {
        cUI = GetComponent<CharacterUI>();
        initStatus();
    }

    // Update is called once per frame
    void Update()
    {
        cUI.updateCharacterUI(status);
        subtractHP(0.5f);
    }
    
    private void initStatus()
    {
        status.hp = 60;
    }

    private void subtractHP(float damage)
    {
        // TODO: should call client wrapper?
        status.hp -= damage;
    }
}
