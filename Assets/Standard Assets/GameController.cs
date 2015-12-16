using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameController : MonoBehaviour
{
    public static GameController gc;

    private static PlayerData playerData;
    private static UIManager ui;
    private static GameObject Player;

    public AudioClip PickUpSound;
    public AudioClip deathSound;
    public AudioClip SnackSound;

    #region Standard Unity Methods
    void Awake()
    {
        if (gc == null)
        {
            DontDestroyOnLoad(gameObject);
            gc = this;
            playerData = new PlayerData();
            ui = gameObject.GetComponent<UIManager>();
            OnLevelWasLoaded();
        }
        else if (gc != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartNewGame();
    }

    void Update()
    {
        // Load data by key press
        if (Input.GetKeyUp(KeyCode.F1))
        {
            LoadPlayerData();
        }
        // Save data by key press
        if (Input.GetKeyUp(KeyCode.F2))
        {
            SavePlayerData();
        }
    }

    void OnLevelWasLoaded()
    {
        ui.FindUIComponents();
        Player = GameObject.FindGameObjectWithTag("Player");
        // Start particle system for level! 1 = Rain, 2 = Leave, 3 = Snow...
    }
    #endregion

    void StartNewGame()
    {
        playerData.hasKey = false;
        playerData.score = 0;
        playerData.deaths = 0;
        StartLevel();
    }

    void StartLevel()
    {
        playerData.hasKey = false;
        ui.HideMessage();
    }
    public void FoundKey()
    {
        playerData.hasKey = true;
        //		player.GetComponent<AudioSource>().clip = keySound;
        Player.gameObject.GetComponent<AudioSource>().PlayOneShot(SnackSound, 1.0f);
    }

    public void FoundSnack()
    {
        playerData.score += 100;
        ui.UpdateHUDText(playerData);
        //	GameObject player = GameObject.FindGameObjectWithTag ("Player");
        Player.GetComponent<AudioSource>().clip = PickUpSound;
        Player.GetComponent<AudioSource>().Play();

        //player.GetComponent<AudioSource>().PlayOneShot(coinSound, 2.0f);
    }

    public void EnteredExit()
    {
        if (playerData.hasKey)
        {
            playerData.currentLevel = Application.loadedLevel + 1;
            Application.LoadLevel(Application.loadedLevel + 1);
        }
        else
        {
            ui.ShowMessage("You need to find the key before winning!", 10.0f);
        }
    }

    #region Load/Save Player Data
    public bool DoesLoadPlayerDataExist()
    {
        bool result = false;
        if (File.Exists(Application.persistentDataPath + "/playerStats.dat"))
        {
            result = true;
        }
        return result;
    }

    public void LoadPlayerData()
    {
        if (DoesLoadPlayerDataExist())
        {
            print("LOADING FILE: " + Application.persistentDataPath + "/playerStats.dat");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/playerStats.dat", FileMode.Open);
            playerData = (PlayerData)bf.Deserialize(fs);
            fs.Close();
            print("LOADED FILE: " + Application.persistentDataPath + "/playerStats.dat");
            if (Application.loadedLevel != playerData.currentLevel)
            {
                Application.LoadLevel(playerData.currentLevel);
            }
        }
        else
        {
            print("NO FILE TO LOAD...");
        }
    }

    public void SavePlayerData()
    {
        print("SAVING PLAYER DATA...");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/playerStats.dat");
        if (fs != null)
        {
            bf.Serialize(fs, playerData);
            fs.Close();
            print("SAVE TO FILE: " + Application.persistentDataPath + "/playerStats.dat");
        }
        else
        {
            print("Failed to create files!");
        }
    }
    #endregion
}