using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;
using TMPro;
public class MyGameManager : MonoBehaviour
{
        // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myBall;

    public static int numActiveBalls;
    private OVRPlayerController ovrPlayerController;
    //private static MyGameManager myGameManager;
    public string paradigmFolderName = Path.Combine(Application.streamingAssetsPath, "Paradigms"); 
    public string dataFolderName = Path.Combine(Application.streamingAssetsPath, "Data"); 
    public string datafilename = "tmpsave.json";
    //public PlayerData playerData;
    public LineObj lineObj;
    private int current_ball_id = -1;
    //public float current_target_radius = 0.2f;
    private OVRCameraRig ovrCameraRig;
    private Spawner spawner;
    public GameObject wall;
    private int punkteBlock;
    private int punkteGesamt;
    public GameSession gameSession;
    public Parameter parameter;
        public GameObject anzeigeTextBall;
    public GameObject anzeigeTextTreffer;
    

//    public GameObject anzeigeTextPunkteGesamt;
    

    private void Awake()
    {
        SetUpSingleton();
        gameSession = FindObjectOfType<GameSession>();
        parameter = FindObjectOfType<Parameter>();
        lineObj = FindObjectOfType<LineObj>(); // Zeichnet den Zielkreis auf die Wand
        spawner = FindObjectOfType<Spawner>();
        wall = GameObject.Find("Wall");
        Debug.Log(gameSession.paradigma.ToString());
        Physics.gravity = new Vector3(0.0f, -9.81f , 0.0f);
        punkteGesamt = 0;
        punkteBlock = 0;
        //Debug.Log("Awake");
    }

    void OnEnable () {
//        EventManager.StartListening ("Spawn", spawnListener);
        //Debug.Log("S+++++++++++++onEnable Register SpawnNewBall on the Eventmanager");
        EventManager.StartListening ("SpawnNewBall", SpawnNewBall);
    }

    void OnDisable () {
//        EventManager.StopListening ("Spawn", spawnListener);
        //Debug.Log("S-------------- onDisable Delete SpawnNewBall from the Eventmanager");
        EventManager.StopListening ("SpawnNewBall", SpawnNewBall);
    }

    public void SpawnNewBall()
    {
        current_ball_id += 1;
        anzeigeTextBall.GetComponent<TextMeshPro>().SetText(current_ball_id.ToString());
        //gameSession.register_new_Ball(current_ball_id);
        parameter.configureParameterForNextBall();
        spawner.Spawn_A_NewBall();
        //lineObj.drawCircleX(get_current_target_radius(), 0.061f);
        //Debug.Log("current ball target = " + get_current_target_radius());
    }

    public int get_current_Ball_ID(){
        return current_ball_id;
    }

    public float get_current_Ball_Mass()
    {
        return parameter.current_ball_mass;
    }
    // public float get_current_target_radius(){
    //     // / 4 da das Object 4 Einheiten gross ist
    //     Debug.Log("currend target radius = " + gameSession.paradigma.target_size );
    //     return gameSession.paradigma.target_size; //wall.transform.localScale.x;
    //     // the ball needs to know in which distance from the center
    //     // of the wall it will count as hit
    //     // this property should be flexible, therfore, each ball needs to get
    //     // this information after his instanciation

    // }
    private void SetUpSingleton()
    {
        int numberOfGameSessions = FindObjectsOfType<MyGameManager>().Length;
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



    void Start()
    {
        ovrPlayerController = FindObjectOfType<OVRPlayerController>();
        ovrCameraRig = FindObjectOfType<OVRCameraRig>();
        StartCoroutine (CompensateHeadPosition());
        SpawnNewBall();
    }


    // public void InitializePlayerDataStructure()
    // {
    //     // hole mir die Daten und initialisiere die Classe mit den PlayerData
    //     //GameObject tmp = FindObjectOfType<VornameText>;
    //     //GameObject tmp = GameObject.Find("VornameText");
    //     //TextMeshProUGUI tmp2 = tmp.GetComponent<TextMeshProUGUI>();

    //     string vorname = GameObject.Find("VornameInputField").GetComponent<TMP_InputField>().text;
    //     string nachname = GameObject.Find("NachnameInputField").GetComponent<TMP_InputField>().text;
    //     string gebDat = GameObject.Find("GebDatumInputField").GetComponent<TMP_InputField>().text;
    //     string trainingsDaystring = GameObject.Find("BehandlungsgruppeInputField").GetComponent<TMP_InputField>().text;
    //     int trainingsDay = int.Parse(trainingsDaystring);
        
    //     string vpNummerstring = GameObject.Find("RandomNummerInputField").GetComponent<TMP_InputField>().text;
    //     int vpNummer = int.Parse(vpNummerstring);



    //     // string vorname = GameObject.Find("VornameText").GetComponent<TextMeshProUGUI>().text;
    //     // string nachname = GameObject.Find("NachnameText").GetComponent<TextMeshProUGUI>().text;
    //     // string gebDat = GameObject.Find("GebDatText").GetComponent<TextMeshProUGUI>().text;
    //     // string trainingsDaystring = GameObject.Find("TrainingsDayText").GetComponent<TextMeshProUGUI>().text;
    //     // trainingsDaystring = trainingsDaystring.Substring(0,trainingsDaystring.Length-1);
        
    //     // int trainingsDay;
    //     // int.TryParse(trainingsDaystring, out trainingsDay);
    //     // string vpNummerstring = GameObject.Find("VPNummerText").GetComponent<TextMeshProUGUI>().text;
    //     // vpNummerstring = vpNummerstring.Substring(0,vpNummerstring.Length-1);
    //     // int vpNummer;
    //     // int.TryParse(vpNummerstring, out vpNummer);
    //     string appdatapath = Application.streamingAssetsPath;
    //     print("appdatapath = " + appdatapath);
        
    //     string relative_path_file = Path.Combine(relativeReadPath, fileDesignName);
    //     print("relativeSavePath = " + relativeSavePath);
    //     //string jsonString = Path.Combine(appdatapath, relative_path_file);
    //     char x = Path.DirectorySeparatorChar;
    //     string jsonFileName = Application.streamingAssetsPath + x + relativeReadPath + x + fileDesignName; //Path.DirectorySeparatorChar Combine(appdatapath, relative_path_file);
    //                                                                                             //        string jsonString = "G:\\Unity\\Elisabeth_Scheiben_game\\Elisabeth_Scheiben\\Assets\\ExperimentalDesign\\Paradigm1.json";
    //                                                                                                     //string jsonString = "G:/Unity/Elisabeth_Scheiben_game/Elisabeth_Scheiben/Assets/ExperimentalDesign/Paradigm2.json";
    //     string jsonString = File.ReadAllText(jsonFileName);

    //     playerData = new PlayerData(vorname, nachname, gebDat, trainingsDay, vpNummer, jsonString);
    //     isInitialized = true;
    //     //playerData.PrintPlayerData();
    //     //Paradigma paradigma = Paradigma(jsonString);
    // }

    IEnumerator CompensateHeadPosition()
    {
        yield return new WaitForSeconds(2.0f);
        //ovrPlayerController.transform.position = new Vector3(0.0f, 0.0f, -2.7f);
        //yield return new WaitForSeconds(2.0f);
        //ovrPlayerController.transform.position = new Vector3(0.0f, 0.0f, -1.7f);
        // yield return new WaitForSeconds(2.0f);
        // ovrCameraRig.transform.position = new Vector3(0.0f, 0.0f, 0.00200245f);
        // yield return new WaitForSeconds(2.0f);
        // ovrCameraRig.transform.position = new Vector3(0.0f, 0.0f, 0.00400245f);
        // yield return new WaitForSeconds(2.0f);
        // ovrCameraRig.transform.position = new Vector3(0.0f, 0.0f, 0.00600245f);
        // yield return new WaitForSeconds(2.0f);
        // ovrPlayerController.transform.position = new Vector3(0.0f, 0.0f, 1.7500245f);
        // yield return new WaitForSeconds(2.0f);
        // ovrPlayerController.transform.position = new Vector3(0.0f, 0.38f, -3.1500245f);
        // yield return new WaitForSeconds(2.0f);
        // ovrPlayerController.transform.position = new Vector3(0.0f, 0.38f, 3.1500245f);
        //yield return new WaitForSeconds(2.0f);
        //ovrCameraRig.transform.position = new Vector3(0.0f, 0.38f, 0.100245f);

    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch));

        //Debug.Log("MY GAme manager is messaging");
        //Debug.Log( ovrPlayerController.transform.position.x + " " + ovrPlayerController.transform.position.y + " " + ovrPlayerController.transform.position.z);
    }

    public void register_Hit(){
        punkteBlock+=1;
        punkteGesamt+=1;
        anzeigeTextTreffer.GetComponent<TextMeshPro>().SetText(punkteGesamt.ToString());
//        anzeigeTextPunkteGesamt.GetComponent<TextMeshPro>().SetText(punkteGesamt.ToString());

    }

}
