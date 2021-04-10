using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;

public class MyGameManager : MonoBehaviour
{
        // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public PlayBall playBall;

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
    //public GameObject wall;
    public GameSession gameSession;
    public Parameter parameter;
    //public Catmul playBall;
    public bool is_game_active = true;


    private void Awake()
    {
        SetUpSingleton();
        gameSession = FindObjectOfType<GameSession>();
        parameter = FindObjectOfType<Parameter>();
        Physics.gravity = new Vector3(0.0f, -9.81f , 0.0f);
        playBall = FindObjectOfType<PlayBall>();
    }


    public int get_current_Ball_ID(){
        return current_ball_id;
    }

    public float get_current_Ball_Mass()
    {
        return parameter.current_ball_mass;
    }
    
    private void SetUpSingleton()
    {
        int numberOfGameSessions = FindObjectsOfType<MyGameManager>().Length;
        if (numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    void Start()
    {
        Debug.Log("MyGameManager:Start()");
        Application.targetFrameRate = 60;
        ovrPlayerController = FindObjectOfType<OVRPlayerController>();
        ovrCameraRig = FindObjectOfType<OVRCameraRig>();
       
        //StartCoroutine (CompensateHeadPosition());
        parameter.adaptHand();
        parameter.set_bounding_box_to_play();
        //SpawnNewBall();

        StartCoroutine(spawnmanagement());
        
        
  
    }


    void Update(){
        
        //SpawnNewBall();
    }


    IEnumerator spawnmanagement()
    {
        while (is_game_active){
        // 1. das Playfield muss initialisiert sein 
        // 2. es darf aktuell keinen activen Ball geben
        // 3. dann wird ein neuer Ball reseted und mit einer neuen Id versehen 
        //    und bei der GameSession registriert
        if (parameter.is_playarea_initialized){
            Debug.Log("MyGameManager:spawnmanagement parameter.is_playarea_initialized ... now SpawnNewBall");
            if (!playBall.is_active){
                Debug.Log("MyGameManager:spawnmanagement playBall.is_active = "+ playBall.is_active);
                SpawnNewBall();
            }
        }
        yield return new WaitForSeconds(1.0f);
        
        }
        // // StartCoroutine(MoveTheBall(ball, spawnPosition, 5.0f));
    }


    public void SpawnNewBall()
    {
        current_ball_id += 1;
        playBall.set_ID(current_ball_id);
        gameSession.register_new_Ball(current_ball_id);
  //      parameter.configureParameterForNextBall();
        Debug.Log("spawn a new Ball Nr = " + current_ball_id);
        Quaternion spawnRotation = new Quaternion ();
        spawnRotation.eulerAngles = new Vector3 (0.0f, 0.0f);
        //Catmul myball = Instantiate (playBall, parameter.playarea_center, spawnRotation) as Catmul;
        playBall.transform.position = parameter.playarea_center;
        playBall.set_bounding_box(parameter.playarea_min, parameter.playarea_max);
        playBall.waitForStart();//start_moving();
        
//        spawner.Spawn_A_NewBall();
       
    }


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

}
