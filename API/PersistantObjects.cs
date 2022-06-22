using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PersistantObjects : PersistentLazySingleton<PersistantObjects>
{
    public static PersistantObjects persistantObject;

	private APIBridge apiBridge;

    public bool demo;
    public bool quitable;

    private bool connectivityCheck = true;

    public FacilitatorLogin facilitator;
    public FacilitatorRequest facilitatorReq;
    public AvailibleGames availibleGames;
    public UsedGames usedGames;
    public Focus gameFocus;

    public int gamesRemaining = 1;
    public int maxGames = 1;

    public string currentGameId;

    private void Awake()
    {
        if (persistantObject == null)
        {
            DontDestroyOnLoad(gameObject);
            persistantObject = this;
        }
        else if (persistantObject != this)
        {
            Destroy(gameObject);
        }
        UnityThread.initUnityThread();
        StartCoroutine(CheckConnection());
    }

    public async void Start()
    {
        if (!Directory.Exists(Application.persistentDataPath + ".data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/.data");
            StreamWriter file = new StreamWriter(DirectoryName("_README_"), false);
            file.WriteLine(ReadMe());
            file.Close();
        }
        if (File.Exists(DirectoryName(Constants.userLogin)))
        {
            facilitator = JsonUtility.FromJson<FacilitatorLogin>(LoadDataFromfile(Constants.userLogin));            

            if (FindObjectOfType<MainMenu>() != null)
            {
                FindObjectOfType<MainMenu>().emailField.GetComponent<InputField>().text = facilitator.email;
                FindObjectOfType<MainMenu>().passwordField.GetComponent<InputField>().text = facilitator.password;
                FindObjectOfType<MainMenu>().loggedIn = true;
                FindObjectOfType<MainMenu>().logOut.SetActive(true);
                FindObjectOfType<MainMenu>().generateCredit.GetComponent<Text>().text = "Generate Credit";
            }            
        }
        else
        {
            FindObjectOfType<MainMenu>().generateCredit.GetComponent<Text>().text = "Log In";
        }
        if (File.Exists(DirectoryName(Constants.userToken)))
        {
            facilitatorReq = JsonUtility.FromJson<FacilitatorRequest>(LoadDataFromfile(Constants.userToken));
        }
        try
        {
            if (File.Exists(DirectoryName(string.Format("{0}_{1}", facilitator.email, Constants.userAvailibleGames))))
            {
                if (LoadDataFromfile(string.Format("{0}_{1}", facilitator.email, Constants.userAvailibleGames)).Length > 0)
                {
                    availibleGames = JsonUtility.FromJson<AvailibleGames>(LoadDataFromfile(string.Format("{0}_{1}", facilitator.email, Constants.userAvailibleGames)));

                    gamesRemaining = availibleGames.games.Count;
                }
            }

            if (File.Exists(DirectoryName(string.Format("{0}_{1}", facilitator.email, Constants.userUsedGames))))
            {
                usedGames = JsonUtility.FromJson<UsedGames>(LoadDataFromfile(string.Format("{0}_{1}", facilitator.email, Constants.userUsedGames)));
                if (ConnectCheck())
                {
                    SendUsedKeys();
                }
            }
        }
        catch
        {
            availibleGames.games = new List<AvailibleGames.Game>();
            usedGames.used_games = new List<UsedGames.Game>();
            gamesRemaining = 0;
        }

        if (apiBridge == null)
        {
            apiBridge = FindObjectOfType<APIBridge>();
            if (apiBridge.transform.parent == null)
            {
                apiBridge.transform.SetParent(transform);
                DontDestroyOnLoad(gameObject);
            }
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            FindObjectOfType<MainMenu>().emailField.GetComponent<InputField>().text = facilitator.email;
            FindObjectOfType<MainMenu>().passwordField.GetComponent<InputField>().text = facilitator.password;
        }
    }

    void OnApplicationQuit()
    {
            Application.wantsToQuit += Quit;
    }

    bool Quit()
    {
        return quitable;
    }

    public void SendUsedKeys()
    {
        if (usedGames != null)
        {
            if (ConnectCheck())
            {
                if (usedGames.used_games.Count > 0)
                {
                    foreach (UsedGames.Game game in usedGames.used_games)
                    {
                        //try send
                        var usedGame = JsonUtility.ToJson(game);

                        APIBridge.Instance.UsedGame(SendSucceeded, SendFailed, usedGame);
                    }
                }
            }
        }
    }

    #region Data Handler
    public void SaveData(string key, string json)
    {
        PlayerPrefs.SetString(key, json);
    }
    public string LoadData(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetString(key);
        }

        return string.Format("Data Not Loaded : Key {0} Does not exist", key);
    }
    public void SaveDataToFile(string key, string json)
    {

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = new FileStream(DirectoryName(key), FileMode.Create);

        formatter.Serialize(file,Obfuscator(json));
        file.Close();
        LoadDataFromfile(key);
    }
    public string LoadDataFromfile(string key)
    {
        string data = "";
        if (File.Exists(DirectoryName(key)))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = new FileStream(DirectoryName(key), FileMode.Open);

            if (file.Length > 0)
            {
                data = DeObfuscator(formatter.Deserialize(file) as string);
            }
            //Debug.Log(formatter.Deserialize(file) as string);
            
            //formatter.Serialize(file, Obfuscator(json));
            file.Close();

            return data;

        }

        return string.Format("Data Not Loaded : Key {0} Does not exist", key);
    }
    #endregion

    string DirectoryName(string key)
    {
        return string.Format("{0}/{1}/{2}.txt",
            Application.persistentDataPath,
            ".data",
            key
            );
    }

    private string Obfuscator(string json)
    {
        string obfuString = json;
        obfuString = obfuString.Replace('{', 'ü');
        obfuString = obfuString.Replace('}', 'Æ');
        obfuString = obfuString.Replace('[', '▄');
        obfuString = obfuString.Replace(']', '▀');
        obfuString = obfuString.Replace('<', '╦');
        obfuString = obfuString.Replace('>', '├');
        obfuString = obfuString.Replace('.', 'Ç');
        obfuString = obfuString.Replace("@", "Ó");
        obfuString = obfuString.Replace('-', 'µ');
        obfuString = obfuString.Replace(':', '×');
        obfuString = obfuString.Replace('"', '$');
        obfuString = obfuString.Replace('a', 'Þ');
        obfuString = obfuString.Replace('b', 'ß');
        obfuString = obfuString.Replace('c', '¾');
        obfuString = obfuString.Replace('d', '§');
        obfuString = obfuString.Replace('e', '█');
        obfuString = obfuString.Replace('f', '▓');
        obfuString = obfuString.Replace('g', '▒');
        obfuString = obfuString.Replace('h', '╠');
        obfuString = obfuString.Replace('i', '»');
        obfuString = obfuString.Replace('j', '╚');
        obfuString = obfuString.Replace('k', '¿');
        obfuString = obfuString.Replace('l', 'Å');
        obfuString = obfuString.Replace('m', 'ô');
        obfuString = obfuString.Replace('n', '¶');
        obfuString = obfuString.Replace('o', '■');
        obfuString = obfuString.Replace('p', '³');
        obfuString = obfuString.Replace('q', 'Û');
        obfuString = obfuString.Replace('r', '¥');
        obfuString = obfuString.Replace('s', 'í');
        obfuString = obfuString.Replace("t", "æ");
        obfuString = obfuString.Replace('u', 'ï');
        obfuString = obfuString.Replace('v', '^');
        obfuString = obfuString.Replace('w', 'ø');
        obfuString = obfuString.Replace('x', '┐');
        obfuString = obfuString.Replace('y', '╝');
        obfuString = obfuString.Replace('z', '║');
        obfuString = obfuString.Replace('A', '«');
        obfuString = obfuString.Replace('B', '¸');
        obfuString = obfuString.Replace('C', '¤');
        obfuString = obfuString.Replace('D', '²');
        obfuString = obfuString.Replace('E', '¢');
        obfuString = obfuString.Replace('F', '®');
        obfuString = obfuString.Replace('G', '¬');
        obfuString = obfuString.Replace('H', '*');
        obfuString = obfuString.Replace('I', '¹');
        obfuString = obfuString.Replace('J', '£');
        obfuString = obfuString.Replace('K', '!');
        obfuString = obfuString.Replace('L', '~');
        obfuString = obfuString.Replace('M', '±');
        obfuString = obfuString.Replace('N', '+');
        obfuString = obfuString.Replace('O', '&');
        obfuString = obfuString.Replace('P', 'é');
        obfuString = obfuString.Replace('Q', 'î');
        obfuString = obfuString.Replace('R', 'ë');
        obfuString = obfuString.Replace('S', '©');
        obfuString = obfuString.Replace("T", "ƒ");
        obfuString = obfuString.Replace('U', '¼');
        obfuString = obfuString.Replace('V', '½');
        obfuString = obfuString.Replace('W', '¡');
        obfuString = obfuString.Replace('X', 'º');
        obfuString = obfuString.Replace('Y', 'ª');
        obfuString = obfuString.Replace('Z', '#');
        obfuString = obfuString.Replace('0', '═');
        obfuString = obfuString.Replace('1', '┴');
        obfuString = obfuString.Replace('2', '¯');
        obfuString = obfuString.Replace('3', '┘');
        obfuString = obfuString.Replace('4', 'ê');
        obfuString = obfuString.Replace('5', '╬');
        obfuString = obfuString.Replace('6', '≡');
        obfuString = obfuString.Replace('7', 'ð');
        obfuString = obfuString.Replace('8', '└');
        obfuString = obfuString.Replace('9', '┼');

        //DeObfuscator(obfuString);
        return obfuString;
    }
    private string DeObfuscator(string json)
    {
        string obfuString = json;
        obfuString = obfuString.Replace('ü', '{');
        obfuString = obfuString.Replace('Æ', '}');
        obfuString = obfuString.Replace('▄', '[');
        obfuString = obfuString.Replace('▀', ']');
        obfuString = obfuString.Replace('╦', '<');
        obfuString = obfuString.Replace('├', '>');
        obfuString = obfuString.Replace('Ç', '.');
        obfuString = obfuString.Replace('Ó', '@');
        obfuString = obfuString.Replace('µ', '-');
        obfuString = obfuString.Replace('×', ':');
        obfuString = obfuString.Replace('$', '"');
        obfuString = obfuString.Replace('Þ', 'a');
        obfuString = obfuString.Replace('ß', 'b');
        obfuString = obfuString.Replace('¾', 'c');
        obfuString = obfuString.Replace('§', 'd');
        obfuString = obfuString.Replace('█', 'e');
        obfuString = obfuString.Replace('▓', 'f');
        obfuString = obfuString.Replace('▒', 'g');
        obfuString = obfuString.Replace('╠', 'h');
        obfuString = obfuString.Replace('»', 'i');
        obfuString = obfuString.Replace('╚', 'j');
        obfuString = obfuString.Replace('¿', 'k');
        obfuString = obfuString.Replace('Å', 'l');
        obfuString = obfuString.Replace('ô', 'm');
        obfuString = obfuString.Replace('¶', 'n');
        obfuString = obfuString.Replace('■', 'o');
        obfuString = obfuString.Replace('³', 'p');
        obfuString = obfuString.Replace('Û', 'q');
        obfuString = obfuString.Replace('¥', 'r');
        obfuString = obfuString.Replace('í', 's');
        obfuString = obfuString.Replace("æ", "t");
        obfuString = obfuString.Replace('ï', 'u');
        obfuString = obfuString.Replace('^', 'v');
        obfuString = obfuString.Replace('ø', 'w');
        obfuString = obfuString.Replace('┐', 'x');
        obfuString = obfuString.Replace('╝', 'y');
        obfuString = obfuString.Replace('║', 'z');
        obfuString = obfuString.Replace('«', 'A');
        obfuString = obfuString.Replace('¸', 'B');
        obfuString = obfuString.Replace('¤', 'C');
        obfuString = obfuString.Replace('²', 'D');
        obfuString = obfuString.Replace('¢', 'E');
        obfuString = obfuString.Replace('®', 'F');
        obfuString = obfuString.Replace('¬', 'G');
        obfuString = obfuString.Replace('*', 'H');
        obfuString = obfuString.Replace('¹', 'I');
        obfuString = obfuString.Replace('£', 'J');
        obfuString = obfuString.Replace('!', 'K');
        obfuString = obfuString.Replace('~', 'L');
        obfuString = obfuString.Replace('±', 'M');
        obfuString = obfuString.Replace('+', 'N');
        obfuString = obfuString.Replace('&', 'O');
        obfuString = obfuString.Replace('é', 'P');
        obfuString = obfuString.Replace('î', 'Q');
        obfuString = obfuString.Replace('ë', 'R');
        obfuString = obfuString.Replace('©', 'S');
        obfuString = obfuString.Replace("ƒ", "T");
        obfuString = obfuString.Replace('¼', 'U');
        obfuString = obfuString.Replace('½', 'V');
        obfuString = obfuString.Replace('¡', 'W');
        obfuString = obfuString.Replace('º', 'X');
        obfuString = obfuString.Replace('ª', 'Y');
        obfuString = obfuString.Replace('#', 'Z');
        obfuString = obfuString.Replace('═', '0');
        obfuString = obfuString.Replace('┴', '1');
        obfuString = obfuString.Replace('¯', '2');
        obfuString = obfuString.Replace('┘', '3');
        obfuString = obfuString.Replace('ê', '4');
        obfuString = obfuString.Replace('╬', '5');
        obfuString = obfuString.Replace('≡', '6');
        obfuString = obfuString.Replace('ð', '7');
        obfuString = obfuString.Replace('└', '8');
        obfuString = obfuString.Replace('┼', '9');

        return obfuString;
    }

    private void SendSucceeded(UsedGames.Game game)
    {
        int count = 0;
        foreach (UsedGames.Game used in usedGames.used_games)
        {
            count++;
        }

        for (int i = 0; i < count; i++)
        {
            usedGames.used_games.RemoveAt(i);
        }

        var used_games = JsonUtility.ToJson(usedGames);

        SaveDataToFile(string.Format("{0}_{1}", facilitator.email, Constants.userUsedGames), used_games);
        SaveData(string.Format("{0}_{1}", facilitator.email, Constants.userUsedGames), used_games);
    }

    private void SendFailed(string error)
    {
        string err = error;
        string errType = "";
        string errReason = "";
        err = err.Replace("{", "");
        err = err.Replace("}", "");

        string[] codesA = err.Split('|');
        int part = 1;

        foreach (string codeA in codesA)
        {
            if (part == 1)
            {
                part++;
                errType = codeA.Replace("HTTP/1.1 ", "");
            }
            else
            {
                part = 1;
                string[] codesB = codeA.Split(':');
                foreach (string codeB in codesB)
                {
                    if (part == 1)
                    {
                        part++;
                    }
                    else
                    {
                        errReason = codeB;
                    }
                }

            }
        }
        if (FindObjectOfType<MainMenu>() != null)
        {
            FindObjectOfType<MainMenu>().ErrorPopup("Token Request Failed", string.Format("Error Type: {0} \nError Message: {1}", errType, errReason));
        }
    }

    string ReadMe()
    {
        string readme = "Hello, \n\nThanks for choosing SXP as your simulation provider. \n\nThe files in this folder are all encrypted and should not be edited manually otherwise the simulation will not be able to read the data correctly and your account may be suspended pending investigation in order to prevent tampering.";

        return readme;
    }

    IEnumerator CheckConnection()
    {  
        APIBridge.Instance.BanUser(ConnectionSuccess, ConnectionFail, JsonUtility.ToJson(new FacilitatorRequest()));

        yield return new WaitForSecondsRealtime(1);

        StartCoroutine(CheckConnection());
    }
   
    public bool ConnectCheck()
    {
        return connectivityCheck;
    }
    private void ConnectionSuccess(FacilitatorRequest req)
    {
        Debug.Log("This is here as a connectivity check");
    }
    private void ConnectionFail(string connection)
    {        
        if (connection.StartsWith("HTTP/1.1 401 Unauthorized"))
        {
            connectivityCheck = true;
        }
        else
        {
            connectivityCheck = false;
        }
    }
}