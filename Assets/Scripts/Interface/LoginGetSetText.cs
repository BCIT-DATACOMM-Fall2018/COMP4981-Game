using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginGetSetText : MonoBehaviour
{
    public InputField IP;
    public InputField Name;
    public InputField placeholder;

    public void setget(){
        Debug.Log("IP=" + IP.text + " Name=" + Name.text + " PlaceHolder=" + placeholder.text);
    }
}
