using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NetworkLibrary;
using NetworkLibrary.MessageElements;
using UnityEngine.SceneManagement;

/// ----------------------------------------------
/// Interface:  	LoginManager - A class that manages the
///                                Login Scene
///
/// PROGRAM:        SKOM
///
/// CONSTRUCTORS:   N/A
///
/// FUNCTIONS:  	TBD
///
/// DATE: 	    	April 1st, 2019
///
/// REVISIONS:
///
/// DESIGNER:   	Cameron Roberts, Viktor Alvar
///
/// PROGRAMMER:     Viktor Alvar
///
/// NOTES:
/// ----------------------------------------------
public class LoginManager : MonoBehaviour
{
    public InputField IP;
    public InputField Name;
    public Text ErrorMessage;

    /// ----------------------------------------------
    /// FUNCTION:	onClickLogin
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:	April 1st, 2019
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts, Viktor Alvar
    ///
    /// INTERFACE: 	private void onClickLogin()
    ///
    /// NOTES:		Handles the Login onClick event.
    /// ----------------------------------------------
    public void OnClickLogin() {
        if (!string.IsNullOrEmpty(IP.text) || !string.IsNullOrEmpty(Name.text)) {
            // Send Connect Packet
            if (ConnectionManager.Instance.RequestConnection(IP.text, Name.text)) {
                SceneManager.LoadScene("Lobby");
            } else {
                ErrorMessage.text = "Connection Request Timeout";
            }
        } else {
            // Prompt User of Incorrect InputFields
            ErrorMessage.text = "Invalid Input Fields";
        }
    }
}
