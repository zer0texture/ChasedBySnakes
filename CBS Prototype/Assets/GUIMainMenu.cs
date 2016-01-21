using UnityEngine;
using System.Collections;

public class GUIMainMenu : MonoBehaviour, GameSaveManager.IGameSaver
{
    //private bool pauseEnabled;


    public bool isMainMenu;
    public bool isMainMenuOptions;
    public bool isMainMenuSliders;
    public bool isMainMenuOptionsAudio;

    public float sliderValue;
    public static float barMaxSliderValue = 100.0f;
    public static float snakeMaxSliderValue = 50.0f;
    public static float otherMaxSliderValue = 10.0f;

    //public static float healthCurrentSliderValue;
    //public static float oilCurrentSliderValue;
    //public static float snakeCurrentSliderValue; 
    //public static float lanternCurrentSliderValue;
    //public static float trailCurrentSliderValue;

    //public static float healthTotalSliderValue = barMaxSliderValue - healthCurrentSliderValue;
    //public static float oilTotalSliderValue = barMaxSliderValue - oilCurrentSliderValue;
    //public static float snakeTotalSliderValue = snakeMaxSliderValue - snakeCurrentSliderValue;
    //public static float lanternTotalSliderValue = otherMaxSliderValue - lanternCurrentSliderValue;
    //public static float trailTotalSliderValue = otherMaxSliderValue - trailCurrentSliderValue;

    static public float currentVolume = 1.0F;

    public enum MenuOption
    {
        MENU_CONTINUE,
        MENU_NEW_GAME,
        MENU_OPTION,
        MENU_SLIDERS,
        MENU_CREDITS,
        MENU_QUIT,
        MENU_BACK
    }

    public MenuOption menuOption;

    UISlider.SliderSave sliderSave;

    void Start()
    {
        //pauseEnabled = false;
        isMainMenu = true;
        isMainMenuOptions = false;
        isMainMenuSliders = false;
        isMainMenuOptionsAudio = false;
        Time.timeScale = 1;  //??
        AudioListener.volume = currentVolume; //??
        //AudioListener.pause = false;
        //Cursor.visible = false; // Hide Cursor

        Load();
        Save();
        
    }

    void Update()
    {

        //check if pause button (escape key) is pressed
        /*if (Input.GetKeyDown("escape"))
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
                AudioListener.volume = currentVolume; //??
                AudioListener.pause = false;
                Cursor.visible = false;
            }

            //else if game isn't paused, then pause it
            else if (pauseEnabled == false)
            {
                pauseEnabled = true;
                AudioListener.volume = currentVolume; //??
                AudioListener.pause = true;
                Time.timeScale = 0; //??
                Cursor.visible = true;
            }
        }*/
    }

    void OnGUI()
    {

        //GUI.DrawTexture(new Rect(50, 50, 120, 120), aTexture, ScaleMode.StretchToFill, true, 10.0F);

       /*GUI.skin.box.font = pauseMenuFont; // ^see above at 'pauseMenuFont' definition - currently set to NULL
        GUI.skin.button.font = pauseMenuFont; // ^see above at 'pauseMenuFont' definition - currently set to NULL
        GUI.backgroundColor = Color.white; // colour for boxes
        GUI.skin.box.border.left = 1;
        GUI.skin.box.border.right = 1;
        GUI.skin.box.border.top = 1;*/


        //GUI.contentColor = Color.green; // colour for text in boxes
        //GUI.color = Color.green; // colour for everything in GUI

        //if (pauseEnabled == true)
       // {
            if (isMainMenu == true)
            {
                //Make a background box -- (in format of: (width initial position, height initial position, width scale, height scale), text to display)                         
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Main Menu");

                //Make Resume button
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 250, 40), "New Game"))
                {
                    Application.LoadLevel(2);

                //AudioListener.volume = 1;                 

                }

                //Make load button
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 60, 250, 40), "Load Last Save"))
                {
                    //INSERT LAST SAVE POINT
                    /*
                    GameSaveManager.m_Instance.StartLoading();
                    GameSaveManager.SceneState save = new GameSaveManager.SceneState();
                    save.Load();
                    Application.LoadLevel(save.m_SceneNo);
                    */
                }

                //Make save button
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 20, 250, 40), "Sliders"))
                {
                    isMainMenuSliders = true;
                    isMainMenu = false;
                }

                //Make Options button
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 20, 250, 40), "Options"))
                {
                    isMainMenuOptions = true;
                    isMainMenu = false;
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

                if(isMainMenuSliders == true)
                    {
                        //Make a background box -- (in format of: (width initial position, height initial position, width scale, height scale), text to display) ..or maybe not..                         
                        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "SLIDERS");

                        sliderValue = GUI.HorizontalSlider(new Rect((Screen.width / 2) - 50,  50, 150, 30), UISlider.GetSliderValue(UISlider.SliderType.HEALTH), 0.0F, barMaxSliderValue);
                        UISlider.SetSliderValue(UISlider.SliderType.HEALTH, sliderValue);
                       // sliderValue = healthCurrentSliderValue;

                                GUI.Label(new Rect((Screen.width / 2) - 45, 30, 300, 20), "Amount of Player Health");
                                GUI.Label(new Rect((Screen.width / 2) - 60, 50, 100, 20), "0");
                                GUI.Label(new Rect((Screen.width / 2) + 105, 50, 100, 20), "100");

                        sliderValue = GUI.HorizontalSlider(new Rect((Screen.width / 2) - 50,  100, 150, 30), UISlider.GetSliderValue(UISlider.SliderType.OIL_MAX), 0.0F, barMaxSliderValue);
                        UISlider.SetSliderValue(UISlider.SliderType.OIL_MAX, sliderValue);
                       // sliderValue = oilCurrentSliderValue;

                                GUI.Label(new Rect((Screen.width / 2) - 35, 80, 300, 20), "Amount of Player Oil");
                                GUI.Label(new Rect((Screen.width / 2) - 60, 100, 100, 20), "0");
                                GUI.Label(new Rect((Screen.width / 2) + 105, 100, 100, 20), "100");

                        sliderValue = GUI.HorizontalSlider(new Rect((Screen.width / 2) - 50,  150, 150, 30), UISlider.GetSliderValue(UISlider.SliderType.NUM_OF_SNAKES), 0.0F, snakeMaxSliderValue);
                        UISlider.SetSliderValue(UISlider.SliderType.NUM_OF_SNAKES, sliderValue);
                       // sliderValue = snakeCurrentSliderValue;

                                GUI.Label(new Rect((Screen.width / 2) - 30, 130, 300, 20), "Number of Snakes");
                                GUI.Label(new Rect((Screen.width / 2) - 60, 150, 100, 20), "0");
                                GUI.Label(new Rect((Screen.width / 2) + 105, 150, 100, 20), "50");

                        sliderValue = GUI.HorizontalSlider(new Rect((Screen.width / 2) - 50, 200, 150, 30), UISlider.GetSliderValue(UISlider.SliderType.LANTERN_SIZE), 0.0F, otherMaxSliderValue);
                        UISlider.SetSliderValue(UISlider.SliderType.LANTERN_SIZE, sliderValue);
                       // sliderValue = lanternCurrentSliderValue;

                                GUI.Label(new Rect((Screen.width / 2) - 30, 180, 300, 20), "Player Lantern Size");
                                GUI.Label(new Rect((Screen.width / 2) - 60, 200, 100, 20), "0");
                                GUI.Label(new Rect((Screen.width / 2) + 105, 200, 100, 20), "10");

                        sliderValue = GUI.HorizontalSlider(new Rect((Screen.width / 2) - 50, 250, 150, 30), UISlider.GetSliderValue(UISlider.SliderType.LENGTH_OF_TRAIL), 0.0F, otherMaxSliderValue);
                        UISlider.SetSliderValue(UISlider.SliderType.LENGTH_OF_TRAIL, sliderValue);
                       // sliderValue = trailCurrentSliderValue;

                                GUI.Label(new Rect((Screen.width / 2) - 35, 230, 300, 20), "Length of Player Trail");
                                GUI.Label(new Rect((Screen.width / 2) - 60, 250, 100, 20), "0");
                                GUI.Label(new Rect((Screen.width / 2) + 105, 250, 100, 20), "10");

            if (GUI.Button(new Rect(Screen.width / 2 - 100, 300, 250, 50), "Back"))
                        {
                            isMainMenuSliders = false;
                            isMainMenu = true;
                        }

                    }

                if (isMainMenuOptions == true)
                    {
                        //Make a background box -- (in format of: (width initial position, height initial position, width scale, height scale), text to display) ..or maybe not..                         
                        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "OPTIONS");

                    if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 250, 50), "Audio Settings"))
                        {
                        //INSERT SETTINGS HERE
                            isMainMenuOptionsAudio = true;
                            isMainMenuOptions = false;
                            // AudioListener.volume = 0;
                        }
                        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 250, 50), "Display Settings"))
                        {
                            //INSERT SETTINGS HERE
                        }

                        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 250, 50), "Back"))
                        {
                            isMainMenuOptions = false;
                            isMainMenu = true;
                        }
                    }

                        if (isMainMenuOptionsAudio)
                        {
                            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "AUDIO SETTINGS");

                            AudioListener.volume = GUI.HorizontalSlider(new Rect((Screen.width / 2) - 50, 110, 150, 30), AudioListener.volume, 0.0F, 1.0F);
                            currentVolume = AudioListener.volume;
                            GUI.Label(new Rect(Screen.width / 2, 90, 100, 20), "VOLUME");
                            GUI.Label(new Rect((Screen.width / 2) - 60, 110, 100, 20), "0");
                            GUI.Label(new Rect((Screen.width / 2) + 105, 110, 100, 20), "100");

                            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 250, 50), "Back"))
                            {
                                isMainMenuOptionsAudio = false;
                                isMainMenuOptions = true;
                            }
                        }
    }


    public void Save()
    {
        if (sliderSave == null)
            sliderSave = new UISlider.SliderSave();

        sliderSave.snakesNum = (int)UISlider.GetSliderValue(UISlider.SliderType.NUM_OF_SNAKES);
        sliderSave.lanternSize = UISlider.GetSliderValue(UISlider.SliderType.LANTERN_SIZE);
        sliderSave.playerHealth = (int)UISlider.GetSliderValue(UISlider.SliderType.HEALTH);
        sliderSave.oilMax = UISlider.GetSliderValue(UISlider.SliderType.OIL_MAX);
        sliderSave.trailLength = UISlider.GetSliderValue(UISlider.SliderType.LENGTH_OF_TRAIL);

        sliderSave.Save();
    }

    public void Load()
    {
        if (sliderSave == null)
            sliderSave = new UISlider.SliderSave();
        sliderSave.Load();

        if (sliderSave.LoadedSuccessfully())
        {
            UISlider.SetSliderValue(UISlider.SliderType.NUM_OF_SNAKES, sliderSave.snakesNum);
            UISlider.SetSliderValue(UISlider.SliderType.LANTERN_SIZE, sliderSave.lanternSize);
            UISlider.SetSliderValue(UISlider.SliderType.HEALTH, sliderSave.playerHealth);
            UISlider.SetSliderValue(UISlider.SliderType.OIL_MAX, sliderSave.oilMax);
            UISlider.SetSliderValue(UISlider.SliderType.LENGTH_OF_TRAIL, sliderSave.trailLength);
        }
        else
        {
            UISlider.SetSliderValue(UISlider.SliderType.HEALTH, barMaxSliderValue);
            UISlider.SetSliderValue(UISlider.SliderType.OIL_MAX, barMaxSliderValue);
            UISlider.SetSliderValue(UISlider.SliderType.NUM_OF_SNAKES, snakeMaxSliderValue);
            UISlider.SetSliderValue(UISlider.SliderType.LANTERN_SIZE, otherMaxSliderValue);
            UISlider.SetSliderValue(UISlider.SliderType.LENGTH_OF_TRAIL, otherMaxSliderValue);
        }
    }

    public void AddAsListener()
    {
        GameSaveManager.m_Instance.AddSaveListener(this);
    }
}

