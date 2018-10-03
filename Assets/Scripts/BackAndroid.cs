#if UNITY_ANDROID

//for android back button to load scene
//attach this to any helper/holder/manager objects in your scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackAndroid : MonoBehaviour {

    public bool moveTaskBack = false; //check this if on the main menu or you want back button to quit
    public Button UIBackButton;
    public string sceneName; //load a scene with this name

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //back button
        {
            if (!moveTaskBack)
            {
                Debug.Log("Hi");
                if (UIBackButton)
                    UIBackButton.onClick.Invoke(); //trigger a back button in the scene
                else
                    SceneManager.LoadScene(sceneName); //load scene
            }
            else
            {
                //push app into tasks manager / multitasking, i.e. like pressing home button
                AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
                activity.Call<bool>("moveTaskToBack", true);

                //Application.Quit(); //terminates app
            }        
        }
	}
}

#endif
