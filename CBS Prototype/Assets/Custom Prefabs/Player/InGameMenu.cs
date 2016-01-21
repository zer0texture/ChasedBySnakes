using UnityEngine;
using System.Collections;
using System;

public class InGameMenu : MonoBehaviour
{
    private bool pauseEnabled;
    public GameObject playerGO;

    public bool isInGameMainMenu;
    public bool isInGameOptions;
    public bool isInGameOptionsAudio;
    public bool isInGameOptionsDisplay;

    // public float currentInGameVolume = GUIMainMenu.currentVolume;;

    Font pauseMenuFont; // change font/style - needs to be added as currently set to NULL
    public Texture aTexture;
    public Texture bTexture;

    AudioSource playerAS;

    void Start()
    {
        pauseEnabled = false;
        isInGameMainMenu = true;
        isInGameOptions = false;
        isInGameOptionsAudio = false;
        isInGameOptionsDisplay = false;
        Time.timeScale = 1;  //??
        AudioListener.volume = GUIMainMenu.currentVolume; //??
        AudioListener.pause = false;
        Cursor.visible = false; // Hide Cursor

        playerAS = playerGO.GetComponent<AudioSource>();
    }

    void Update()
    {


        //check if pause button (escape key) is pressed
        if (Input.GetKeyDown("escape"))
        {
            isInGameMainMenu = true;
            isInGameOptions = false;
            isInGameOptionsAudio = false;
            //check if game is already paused        
            if (pauseEnabled == true)
            {
                //unpause the game
                pauseEnabled = false;
                Time.timeScale = 1; //??
                AudioListener.volume = GUIMainMenu.currentVolume; //??
                AudioListener.pause = false;
                Cursor.visible = false;
            }

            //else if game isn't paused, then pause it
            else if (pauseEnabled == false)
            {
                pauseEnabled = true;
                AudioListener.volume = GUIMainMenu.currentVolume; //??
                AudioListener.pause = true;
                Time.timeScale = 0; //??
                Cursor.visible = true;
            }
        }
    }

    void OnGUI()
    {

        //GUI.DrawTexture(new Rect(50, 50, 120, 120), aTexture, ScaleMode.StretchToFill, true, 10.0F);

        GUI.skin.box.font = pauseMenuFont; // ^see above at 'pauseMenuFont' definition - currently set to NULL
        GUI.skin.button.font = pauseMenuFont; // ^see above at 'pauseMenuFont' definition - currently set to NULL
        GUI.backgroundColor = Color.white; // colour for boxes
        GUI.skin.box.border.left = 1;
        GUI.skin.box.border.right = 1;
        GUI.skin.box.border.top = 1;


        //GUI.contentColor = Color.green; // colour for text in boxes
        //GUI.color = Color.green; // colour for everything in GUI

        if (pauseEnabled == true)
        {
            if (isInGameMainMenu == true)
            {
                //Make a background box -- (in format of: (width initial position, height initial position, width scale, height scale), text to display)                         
                GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 250, 300), aTexture);

                //Make Resume button
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 250, 40), bTexture))
                {
                    pauseEnabled = false;
                    Time.timeScale = 1;
                    //AudioListener.volume = 1;
                    Cursor.visible = false;
                    AudioListener.pause = false;
                }

                //Make load button
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 60, 250, 40), "Load Last Save"))
                {
                    //INSERT LAST SAVE POINT

                    GameSaveManager.m_Instance.StartLoading();
                    GameSaveManager.SceneState save = new GameSaveManager.SceneState();
                    save.Load();
                    Application.LoadLevel(save.m_SceneNo);

                }

                //Make save button
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 20, 250, 40), "Save Game"))
                {
                    //INSERT SAVE POINT

                    GameSaveManager.SceneState sceneSave = new GameSaveManager.SceneState();
                    sceneSave.m_SceneNo = Application.loadedLevel;
                    sceneSave.Save();
                    GameSaveManager.m_Instance.SaveGame();
                }

                //Make Options button
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 20, 250, 40), "Options"))
                {
                    isInGameOptions = true;
                    isInGameMainMenu = false;
                }

                //Make Main Menu button
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 70, 250, 40), "Return to Main Menu"))
                {
                    Application.LoadLevel(0);
                }

                //Make quit game button
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 110, 250, 40), "Quit to Windows"))
                {
                    Application.Quit(); // quits application
                }
            }
            if (isInGameOptions == true)
            {
                //Make a background box -- (in format of: (width initial position, height initial position, width scale, height scale), text to display) ..or maybe not..                         
                GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 250, 300), "OPTIONS");

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 250, 50), "Audio Settings"))
                {
                    //INSERT SETTINGS HERE
                    isInGameOptionsAudio = true;
                    isInGameOptions = false;
                    // AudioListener.volume = 0;
                }
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 250, 50), "Display Settings"))
                {
                    isInGameOptionsDisplay = true;
                    isInGameOptions = false;
                }

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 250, 50), "Back"))
                {
                    isInGameOptions = false;
                    isInGameMainMenu = true;
                }
            }

            if (isInGameOptionsAudio)
            {
                GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 250, 300), "AUDIO SETTINGS");

                AudioListener.volume = GUI.HorizontalSlider(new Rect((Screen.width / 2) - 50, 110, 150, 30), GUIMainMenu.currentVolume, 0.0F, 1.0F);
                GUIMainMenu.currentVolume = AudioListener.volume;
                GUI.Label(new Rect(Screen.width / 2, 90, 100, 20), "VOLUME");
                GUI.Label(new Rect((Screen.width / 2) - 60, 110, 100, 20), "0");
                GUI.Label(new Rect((Screen.width / 2) + 105, 110, 100, 20), "100");


                // Player Sound Stuff (NOT COMPLETE)
                //playerAS.volume = GUI.HorizontalSlider(new Rect((Screen.width / 2) - 50, 50, 150, 30), playerAS.volume, 0.0F, 1.0F);
                //playerAS.volume = AudioListener.volume;
                //GUI.Label(new Rect(Screen.width / 2, 0, 100, 20), "Background (Player Sound) Noise");
                //GUI.Label(new Rect((Screen.width / 2) - 60, 110, 100, 20), "0");
                //GUI.Label(new Rect((Screen.width / 2) + 105, 110, 100, 20), "100");

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 250, 50), "Back"))
                {
                    isInGameOptionsAudio = false;
                    isInGameOptions = true;
                }
            }

            if (isInGameOptionsDisplay)
            {
                GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 250, 300), "DISPLAY SETTINGS");

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 50, 250, 50), "Resolution 640 x 480"))
                {
                    Screen.SetResolution(640, 480, true);
                }
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 250, 50), "Resolution 1920 x 1080"))
                {
                    Screen.SetResolution(1920, 1080, true);
                }


                /*AudioListener.volume = GUI.HorizontalSlider(new Rect((Screen.width / 2) - 50, 110, 150, 30), GUIMainMenu.currentVolume, 0.0F, 1.0F);
                    GUIMainMenu.currentVolume = AudioListener.volume;
                    GUI.Label(new Rect(Screen.width / 2, 90, 100, 20), "VOLUME");
                    GUI.Label(new Rect((Screen.width / 2) - 60, 110, 100, 20), "0");
                    GUI.Label(new Rect((Screen.width / 2) + 105, 110, 100, 20), "100");
                    */
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 250, 50), "Back"))
                {
                    isInGameOptionsDisplay = false;
                    isInGameOptions = true;
                }
            }
        }
    }
}

/* if (optionsDropDown == false)
                    {
                        optionsDropDown = true;
                    }
                    else
                    {
                        optionsDropDown = false;
                    }*/

// Options dropdown list
/* if (optionsDropDown == true)
 {
     if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2 - 50, 250, 50), "Slider 1"))
     {
         //NO INPUT ADDED YET
     }
     if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2, 250, 50), "Slider 2"))
     {
         //NO INPUT ADDED YET
     }
     if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2 + 50, 250, 50), "Slider 3"))
     {
         //NO INPUT ADDED YET
     }
     if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2 + 100, 250, 50), "Slider 4"))
     {
         //NO INPUT ADDED YET
     }
     if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2 + 150, 250, 50), "Slider 5"))
     {
         //NO INPUT ADDED YET
     }
     if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2 + 200, 250, 50), "Slider 6"))
     {
         //NO INPUT ADDED YET
     }

     if (Input.GetKeyDown("escape"))
     {
         optionsDropDown = false;
     }
 }*/
