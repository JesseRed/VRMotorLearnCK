using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class GameSession : MonoBehaviour
{
    public PlayerData playerData;
    
    public bool isTutorial = false;
    public bool isInitialized = false;
    public string relativeSavePath = "Data";
    public string relativeReadPath = "Paradigms";
    // public string fileDesignName = "Experiment1_Day1.csv";
    public string fileDesignName = "Paradigma";
    private string fullSaveFileName;
    
    public Paradigma paradigma;
    
    
    public Ball _currentBall;

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

    }

    // Update is called once per frame
    void Update()
    {

    }

    // public void add_empty_Ball(int ID, float radius, float mass, Vector3 grav, Vector3 force, Vector3 offset_pos, Vector3 offset_vel, Vector3 invert, Vector4 tremor){
    //     float t = Time.time;
    //     playerData._Balls.Add(new OneBall(t, mass));
    // }

    public void set_Hit(int ID, int x){
        // if the current ball has hit the target 

        playerData._Balls[ID].is_Hit = x;
        playerData.num_hits ++;
    }

    public void register_new_Ball(int ID, float radius, float mass, Vector3 grav, Vector3 force, Vector3 offset_pos, Vector3 offset_vel, Vector3 invert, Vector4 tremor){
        float t = Time.time;
        playerData._Balls.Add(new OneBall(ID, t, radius, mass, grav, force, offset_pos, offset_vel, invert, tremor));
        // while (ID>playerData._Balls.Count-1){

        // }
    }
    public void set_pick_up_time(int ID, float t){
        playerData._Balls[ID].pick_up_Time = t;
    }
    //[SerializeField] private SubjectData _SubjectData = new SubjectData();
    public void set_leave_the_Hand_Time(int ID, float t){
        playerData._Balls[ID].leave_the_Hand_Time = t;
    }

    public void add_Ball_Hand_Position(int ID, float ball_X, float ball_Y, float ball_Z, 
        float hand_X, float hand_Y, float hand_Z, float t)
    {
        BallPositionInfo _mycurrentBallPositionInfo = new BallPositionInfo(ball_X, ball_Y, ball_Z, hand_X, hand_Y, hand_Z, t);
        playerData._Balls[ID].add_BallPostionInfo(_mycurrentBallPositionInfo);
    }
    public void SaveIntoJson(){
        // nun speichern wir den aktuellen Stand ab ... dies 
        // geschiet nach jedem Ball um einen Zwischenstand zu haben
        //string fullfilename = Path.Combine(Application.streamingAssetsPath, relativeSavePath, save_name);

        string data_string = JsonUtility.ToJson(playerData);
        
        System.IO.File.WriteAllText(fullSaveFileName, data_string);
    }

    public void InitializePlayerDataStructure()
    {
        // hole mir die Daten und initialisiere die Classe mit den PlayerData
        //GameObject tmp = FindObjectOfType<VornameText>;
        //GameObject tmp = GameObject.Find("VornameText");
        //TextMeshProUGUI tmp2 = tmp.GetComponent<TextMeshProUGUI>();
        fileDesignName = "Paradigm";
        string vorname = GameObject.Find("VornameInputField").GetComponent<TMP_InputField>().text;
        string nachname = GameObject.Find("NachnameInputField").GetComponent<TMP_InputField>().text;
        string gebDat = GameObject.Find("GebDatumInputField").GetComponent<TMP_InputField>().text;
        string trainingsDaystring = GameObject.Find("BehandlungsgruppeInputField").GetComponent<TMP_InputField>().text;
        int trainingsDay = int.Parse(trainingsDaystring);
        
        string vpNummerstring = GameObject.Find("RandomNummerInputField").GetComponent<TMP_InputField>().text;
        int vpNummer = int.Parse(vpNummerstring);



        string appdatapath = Application.streamingAssetsPath;
        print("appdatapath = " + appdatapath);
        
        string relative_path_file = Path.Combine(relativeReadPath, fileDesignName);
        print("relativeSavePath = " + relativeSavePath);
        //string jsonString = Path.Combine(appdatapath, relative_path_file);
        char x = Path.DirectorySeparatorChar;
        string jsonFileName = Application.streamingAssetsPath + x + relativeReadPath + x + fileDesignName + trainingsDay + ".json"; //Path.DirectorySeparatorChar Combine(appdatapath, relative_path_file);
                                                                                                //        string jsonString = "G:\\Unity\\Elisabeth_Scheiben_game\\Elisabeth_Scheiben\\Assets\\ExperimentalDesign\\Paradigm1.json";
                                                                                                        //string jsonString = "G:/Unity/Elisabeth_Scheiben_game/Elisabeth_Scheiben/Assets/ExperimentalDesign/Paradigm2.json";
        string jsonString = File.ReadAllText(jsonFileName);
        playerData = new PlayerData(vorname, nachname, gebDat, trainingsDay, vpNummer, jsonString);
        isInitialized = true;
        string saveFileName = "VP_" + vpNummer + "_TD_" + trainingsDay.ToString() + "_" + vorname + "_" + nachname + "_" + gebDat.Replace('.','-') + ".json";
        fullSaveFileName = Path.Combine(Application.streamingAssetsPath, relativeSavePath, saveFileName);
        //paradigma = Paradigma(jsonString);
        paradigma = JsonUtility.FromJson<Paradigma>(jsonString);
        //playerData.PrintPlayerData();
        //Paradigma paradigma = Paradigma(jsonString);
    }
}
    [System.Serializable]
    public class PlayerData
    {
        public string vorname;
        public string nachname;
        public string gebDatum;
        public int trainingsDay;
        public int vpNummer;
        public Paradigma paradigma;
        public int num_hits;
        public List<OneBall> _Balls;


        // construktor .... ohne die persoenlichen Infos geht nix
        public PlayerData(string vn, string nn, string gd, int td, int vpnum, string jsonString)
        {
            vorname = vn;
            nachname = nn;
            gebDatum = gd;
            trainingsDay = td;
            vpNummer = vpnum;
            //paradigma = Paradigma(jsonString);
            //print(jsonString);
            //paradigma = Paradigma.CreateFromJSON(jsonString);
            paradigma = JsonUtility.FromJson<Paradigma>(jsonString);
            _Balls = new List<OneBall>();
            num_hits = 0;
        }

    public void add_Ball(OneBall new_Ball){
        _Balls.Add(new_Ball);
    }

    }

//  public void register_new_Ball(int ID, float radius, float mass, Vector3 grav, Vector3 force, Vector3 offset_pos, Vector3 offset_vel, Vector3 invert, Vector4 tremor){
//         float t = Time.time;
//         playerData._Balls.Add(new OneBall(ID, t, radius, mass, grav, force, offset_pos, offset_vel, invert, tremor);
      


[System.Serializable]
public class OneBall
{
    public int is_Hit;
    public float creation_Time;
    public int ID;
    public float target_radius;
    public Vector3 gravity;
    public Vector3 ball_force;
    public Vector3 offset_hand_pos;
    public Vector3 offset_hand_vel;
    public Vector3 hand_invertation;
    public Vector4 hand_tremor;
    public float pick_up_Time;
    public float leave_the_Hand_Time;
    public float ball_mass;
    public List<BallPositionInfo> _BallPositionInfoList;
    public OneBall(int _ID, float t, float radius, float mass, Vector3 grav, Vector3 force, Vector3 offset_pos, Vector3 offset_vel, Vector3 invert, Vector4 tremor)
    {
        _BallPositionInfoList = new List<BallPositionInfo>();
        is_Hit = 0;
        ball_mass = mass;
        creation_Time = t;
        ID = _ID;
        target_radius = radius;
        gravity = grav;
        ball_force = force;
        offset_hand_pos = offset_pos;
        offset_hand_vel = offset_vel;
        hand_invertation = invert;
        hand_tremor = tremor;

    }
    public void add_BallPostionInfo(BallPositionInfo new_BallPositionInfo)
    {
        _BallPositionInfoList.Add(new_BallPositionInfo);
    }
}



[System.Serializable]
public class BallPositionInfo
{
    public float ball_x;
    public float ball_y;
    public float ball_z;
    public float hand_x;
    public float hand_y;
    public float hand_z;
    public float t;

    public BallPositionInfo(float new_x, float new_y, float new_z, float new_hand_x, float new_hand_y, float new_hand_z, float new_t)
    {
        //time
        t = new_t;
        ball_x = new_x;
        ball_y = new_y;
        ball_z = new_z;
        hand_x = new_hand_x;
        hand_y = new_hand_y;
        hand_z = new_hand_z;
    }
}






    [System.Serializable]
    public class Paradigma
    {

    public int numBalls;
    public float target_size;

    public float ball_mass;

    public float gravity_X_min;
    public float gravity_X_max;
    public float gravity_Y_min;
    public float gravity_Y_max;
    public float gravity_Z_min;
    public float gravity_Z_max;
    public float force_X_min;
    public float force_X_max;
    public float force_Y_min;
    public float force_Y_max;
    public float force_Z_min;
    public float force_Z_max;
    public float offset_hand_pos_X_min;
    public float offset_hand_pos_X_max;
    public float offset_hand_pos_Y_min;
    public float offset_hand_pos_Y_max;
    public float offset_hand_pos_Z_min;
    public float offset_hand_pos_Z_max;
    public float offset_hand_vel_X_min;
    public float offset_hand_vel_X_max;
    public float offset_hand_vel_Y_min;
    public float offset_hand_vel_Y_max;
    public float offset_hand_vel_Z_min;
    public float offset_hand_vel_Z_max;
    public float hand_invert_X;
    public float hand_invert_Y;
    public float hand_invert_Z;
    public float hand_tremor_X;
    public float hand_tremor_Y;
    public float hand_tremor_Z;
    public float hand_tremor_freq;
    public int Adaptive;
    public float desired_hit_rate;



    }

