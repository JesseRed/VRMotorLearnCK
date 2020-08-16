﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class GameSession : MonoBehaviour
{
    //public PlayerData playerData;
 
    public string relativeSavePath = "Data";
    public string relativeReadPath = "ExperimentalDesign";
    // public string fileDesignName = "Experiment1_Day1.csv";
    public string fileDesignName = "Paradigma1.json";
    //public Paradigma paradigma;
    private int numActiveBalls;
    public GameObject myBall;
    private void Awake()
    {
        SetUpSingleton();
        //Debug.Log("Awake");
    }

    private void SetUpSingleton()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberOfGameSessions > 1)
        {
            //print("already initialized");
            //GameObject MM = FindObject


            Destroy(gameObject);

        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start of GameSession");
        Instantiate(myBall, new Vector3(0.217f, 1.068f, 0.53f), Quaternion.identity);
        numActiveBalls = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    int getNumberActiveBalls()
    {
        return numActiveBalls;
    }
















//     public void InitializePlayerDataStructure()
//     {
//         // hole mir die Daten und initialisiere die Classe mit den PlayerData
//         //GameObject tmp = FindObjectOfType<VornameText>;
//         //GameObject tmp = GameObject.Find("VornameText");
//         //TextMeshProUGUI tmp2 = tmp.GetComponent<TextMeshProUGUI>();

//         string vorname = GameObject.Find("VornameInputField").GetComponent<TMP_InputField>().text;
//         string nachname = GameObject.Find("NachnameInputField").GetComponent<TMP_InputField>().text;
//         string gebDat = GameObject.Find("GebDatumInputField").GetComponent<TMP_InputField>().text;
//         string trainingsDaystring = GameObject.Find("BehandlungsgruppeInputField").GetComponent<TMP_InputField>().text;
//         int trainingsDay = int.Parse(trainingsDaystring);
        
//         string vpNummerstring = GameObject.Find("RandomNummerInputField").GetComponent<TMP_InputField>().text;
//         int vpNummer = int.Parse(vpNummerstring);



//         // string vorname = GameObject.Find("VornameText").GetComponent<TextMeshProUGUI>().text;
//         // string nachname = GameObject.Find("NachnameText").GetComponent<TextMeshProUGUI>().text;
//         // string gebDat = GameObject.Find("GebDatText").GetComponent<TextMeshProUGUI>().text;
//         // string trainingsDaystring = GameObject.Find("TrainingsDayText").GetComponent<TextMeshProUGUI>().text;
//         // trainingsDaystring = trainingsDaystring.Substring(0,trainingsDaystring.Length-1);
        
//         // int trainingsDay;
//         // int.TryParse(trainingsDaystring, out trainingsDay);
//         // string vpNummerstring = GameObject.Find("VPNummerText").GetComponent<TextMeshProUGUI>().text;
//         // vpNummerstring = vpNummerstring.Substring(0,vpNummerstring.Length-1);
//         // int vpNummer;
//         // int.TryParse(vpNummerstring, out vpNummer);
//         string appdatapath = Application.streamingAssetsPath;
//         print("appdatapath = " + appdatapath);
        
//         string relative_path_file = Path.Combine(relativeReadPath, fileDesignName);
//         print("relativeSavePath = " + relativeSavePath);
//         //string jsonString = Path.Combine(appdatapath, relative_path_file);
//         char x = Path.DirectorySeparatorChar;
//         string jsonFileName = Application.streamingAssetsPath + x + relativeReadPath + x + fileDesignName; //Path.DirectorySeparatorChar Combine(appdatapath, relative_path_file);
//                                                                                                 //        string jsonString = "G:\\Unity\\Elisabeth_Scheiben_game\\Elisabeth_Scheiben\\Assets\\ExperimentalDesign\\Paradigm1.json";
//                                                                                                         //string jsonString = "G:/Unity/Elisabeth_Scheiben_game/Elisabeth_Scheiben/Assets/ExperimentalDesign/Paradigm2.json";
//         string jsonString = File.ReadAllText(jsonFileName);

//         playerData = new PlayerData(vorname, nachname, gebDat, trainingsDay, vpNummer, jsonString);
//         isInitialized = true;
//         //playerData.PrintPlayerData();
//         //Paradigma paradigma = Paradigma(jsonString);
//     }

//     [System.Serializable]
//     public class PlayerData
//     {
//         public string vorname;
//         public string nachname;
//         public string gebDatum;
//         public int trainingsDay;
//         public int vpNummer;
//         public Paradigma paradigma;

//         private List<PlayerTrackEntry> playerTrackEntries;
//         public char lineSeperater = '\n'; // It defines line seperate character
//         public char fieldSeperator = ';'; // It defines field seperate chracter
    


//         // construktor .... ohne die persoenlichen Infos geht nix
//         public PlayerData(string vn, string nn, string gd, int td, int vpnum, string jsonString)
//         {
//             vorname = vn;
//             nachname = nn;
//             gebDatum = gd;
//             trainingsDay = td;
//             vpNummer = vpnum;
//             //paradigma = Paradigma(jsonString);
//             //print(jsonString);
//             //paradigma = Paradigma.CreateFromJSON(jsonString);
//             paradigma = JsonUtility.FromJson<Paradigma>(jsonString);
//             playerTrackEntries = new List<PlayerTrackEntry>();
//         }

//         public void AddData(int iblockIdx, float itimeSinceBlockStart, string ieventType, int iisHit, int ischeibenNum, float iposXmouse, float iposYmouse, float iposXScheibe, float iposYScheibe, float ivelocity, float ischeibenDiameter, float iexistenceTime, float imaxExistenceTime, int inumScheibenPresent, float timeToMovementInitiation, float cursorPathError, float finalPosError, float maxMovementVelocity)
//         {
//             playerTrackEntries.Add(new PlayerTrackEntry(iblockIdx, itimeSinceBlockStart, ieventType, iisHit, ischeibenNum, iposXmouse, iposYmouse, iposXScheibe, iposYScheibe, ivelocity, ischeibenDiameter, iexistenceTime, imaxExistenceTime, inumScheibenPresent, timeToMovementInitiation, cursorPathError, finalPosError, maxMovementVelocity));
//         }

//         public void SaveDataAsCSV()
//         {
//             char x = Path.DirectorySeparatorChar;
//             string path = Application.streamingAssetsPath + x + "Data";
//             print(path);
// //            string path = relativeFilePath; // Application.persistentDataPath;
//             string filename = path + x + "VP_" + vpNummer + "_TD_" + trainingsDay.ToString() + "_" + vorname + "_" + nachname + "_" + gebDatum.Replace('.','-') + ".csv";
//             print("filename = " + filename);
//             using (StreamWriter sw = new StreamWriter(filename))
//             {
//                 // heading line for csv File 


//                 string line = "BlockNumber" + fieldSeperator + "Time Since Block start in s" + fieldSeperator + "EventType" + fieldSeperator + "isHit" + fieldSeperator +
//                 "Scheiben Nummer (in Sequence)" + fieldSeperator + "MousePosX" + fieldSeperator + "MousePosY" + fieldSeperator + "Scheiben Pos X" + fieldSeperator + "Scheiben Pos Y" + fieldSeperator +
//                 "Scheiben Velocity" + fieldSeperator + "Scheiben Durchmesser" + fieldSeperator + "Zeit of Existence" + fieldSeperator +
//                 "Maximale Existenzzeit" + fieldSeperator + "Anzahl an Scheiben aktuell present" + fieldSeperator +
//                 "Time To Movement Initiation" + fieldSeperator + "Cursor Path Error" + fieldSeperator + 
//                 "final Pos Error" + fieldSeperator + "maxMovementVelocity in pixel/sec";



//                 sw.WriteLine(line);
//                 for (int i = 0; i < playerTrackEntries.Count; i++)
//                 {
//                     line = playerTrackEntries[i].getEntryString();
//                     sw.WriteLine(line);
//                 }
//             }
//         }

//         public void PrintPlayerData()
//         {
//             print("Ausgabe von Playerdata");
//             print("Name = " + vorname + ' ' + nachname);
//             print(paradigma.numBlocks);
//         }
//     }



//     public class PlayerTrackEntry
//     {

//         // blockIdx  |   Time since Block start  |   Type           |  Hit          |   ScheibenNr (in Sequence)  |    position x  |  pos y     |    velocity   |       Radius   | existence time | max_existence time |  num scheiben present
//         //    1                 1234 (ms)            mouseButton       1/0         |       1 bei miss naechste scheibe
//         //    1                 3323                instantiate        0           |       2
//         //    1                 3233 (ms)            destroy            0           |       3    
//         private int blockIdx;
//         private float timeSinceBlockStart;
//         private string eventType;
//         private int isHit;
//         private int scheibenNum;
//         private float posXmouse;
//         private float posYmouse;
//         private float posXScheibe;
//         private float posYScheibe;
//         private float velocity;
//         private float scheibenDiameter;
//         private float existenceTime;
//         private float maxExistenceTime;
//         private int numScheibenPresent;
//         public char fieldSeperator = ';'; // It defines field seperate chracter
//         private float timeToMovementInitiation;
//         private float cursorPathError;
//         private float finalPosError; 
//         private float maxMovementVelocity;

//         public PlayerTrackEntry(int iblockIdx, float itimeSinceBlockStart,
//             string ieventType, int iisHit, int ischeibenNum,
//             float iposXmouse, float iposYmouse,
//             float iposXScheibe, float iposYScheibe,
//             float ivelocity, float ischeibenDiameter,
//             float iexistenceTime, float imaxExistenceTime, int inumScheibenPresent, 
//             float itimeToMovementInitiation, float icursorPathError, 
//             float ifinalPosError, float imaxMovementVelocity)
//         {
//             blockIdx = iblockIdx;
//             timeSinceBlockStart = itimeSinceBlockStart;
//             eventType = ieventType;
//             isHit = iisHit;
//             scheibenNum = ischeibenNum;
//             posXmouse = iposXmouse;
//             posYmouse = iposYmouse;
//             posXScheibe = iposXScheibe;
//             posYScheibe = iposYScheibe;
//             velocity = ivelocity;
//             scheibenDiameter = ischeibenDiameter;
//             existenceTime = iexistenceTime;
//             maxExistenceTime = imaxExistenceTime;
//             numScheibenPresent = inumScheibenPresent;
//             timeToMovementInitiation = itimeToMovementInitiation;
//             cursorPathError = icursorPathError;
//             finalPosError = ifinalPosError; 
//             maxMovementVelocity = imaxMovementVelocity;

//         }

//         public string getEntryString()
//         {
//             string line = blockIdx.ToString() + fieldSeperator + timeSinceBlockStart.ToString() +
//             fieldSeperator + eventType + fieldSeperator + isHit.ToString() + fieldSeperator + scheibenNum.ToString() +
//             fieldSeperator + posXmouse.ToString() + fieldSeperator + posYmouse.ToString() + fieldSeperator + posXScheibe.ToString() + fieldSeperator + posYScheibe.ToString() +
//             fieldSeperator + velocity.ToString() + fieldSeperator + scheibenDiameter.ToString() + fieldSeperator + existenceTime.ToString() +
//             fieldSeperator + maxExistenceTime.ToString() + fieldSeperator + numScheibenPresent.ToString() +
//             fieldSeperator + timeToMovementInitiation.ToString() + 
//             fieldSeperator + cursorPathError.ToString() + 
//             fieldSeperator + finalPosError.ToString() + 
//             fieldSeperator + maxMovementVelocity.ToString();
//             return line;
//         }
//     }


//     [System.Serializable]
//     public class Paradigma
//     {
//         public int initialCountdown;

//         public float timeBetweenHitAndDisappear;
//         public int numBlocks;
//         public float timePerBlock;
//         public int numScheiben;
//         public float MinimumInterScheibenDelay;
//         public float MaximumInterScheibenDelay;
//         public float MinimumVelocity;
//         public float MaximumVelocity;
//         public float MinimumScheibenExistenceDuration;
//         public float MaximumScheibenExistenceDuration;
//         public float MinimumScheibenDiameter;
//         public float MaximumScheibenDiameter;
//         public float MinimumScheibenDistance;
//         public int InstantNewScheibeAfterHit;
//         public int Adaptive;

//         // public static Paradigma CreateFromJSON(string jsonString)
//         // {
//         //      return JsonUtility.FromJson<Paradigma>(jsonString);
//         // }

//     }
}
