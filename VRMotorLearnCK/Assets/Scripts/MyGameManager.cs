using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;

public class MyGameManager : MonoBehaviour
{
        // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myBall;
    public float target_radius = 1;
    public static int numActiveBalls;
    private OVRPlayerController ovrPlayerController;
    private static MyGameManager myGameManager;
    public string paradigmFolderName = Path.Combine(Application.streamingAssetsPath, "Paradigms"); 
    public string dataFolderName = Path.Combine(Application.streamingAssetsPath, "Data"); 
    public string datafilename = "tmpsave.json";
    public SaveData saveData;
    public LineObj lineObj;
    private int current_ball_id = -1;
    public float current_target_radius = 0.5f;
    private OVRCameraRig ovrCameraRig;
    // public static MyGameManager instance
    // {
    //     get
    //     {
    //         if (!myGameManager)
    //         {
    //             myGameManager = FindObjectOfType (typeof (MyGameManager)) as MyGameManager;

    //             if (!myGameManager)
    //             {
    //                 Debug.LogError ("There needs to be one active MyGameManger script on a GameObject in your scene.");
    //             }
    //             else
    //             {
    //                 myGameManager.Init (); 
    //             }
    //         }

    //         return myGameManager;
    //     }
    // }
    // // Start is called before the first frame update

    void Init()
    {
       // Instantiate(myBall, new Vector3(0.217f, 1.068f, 0.53f), Quaternion.identity);
        //numActiveBalls = 1;
    }

    private void Awake()
    {
        SetUpSingleton();
        saveData = FindObjectOfType<SaveData>();
        lineObj = FindObjectOfType<LineObj>();

        //Debug.Log("Awake");
    }

    public int get_new_Ball_ID(){
        current_ball_id +=1;
        saveData.register_new_Ball(current_ball_id);
        return current_ball_id;
    }

    public int get_current_Ball_ID(){
        return current_ball_id;
    }

    public float get_current_target_radius(){
        return current_target_radius;
        // the ball needs to know in which distance from the center
        // of the wall it will count as hit
        // this property should be flexible, therfore, each ball needs to get
        // this information after his instanciation

    }
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
        saveData = FindObjectOfType<SaveData>();
        StartCoroutine (CompensateHeadPosition());
        Instantiate(myBall, new Vector3(0.0f, -1.451f, -2.65f), Quaternion.identity);
        numActiveBalls = 1;
        
        
        //string paradigmFolderName = Path.Combine(Application.streamingAssetsPath, "Paradigms"); 
        //string dataFolderName = Path.Combine(Application.streamingAssetsPath, "Data"); 
        
        //OVRPlayerController.
        // Instantiate at position (0, 0, 0) and zero rotation.
        //Instantiate(myBall, new Vector3(0.217f, 1.068f, 0.53f), Quaternion.identity);
        //numActiveBalls = 1;
        //Instantiate(myBall, new Vector3(1, 1, 1), Quaternion.identity);
    }



    IEnumerator CompensateHeadPosition()
    {
        yield return new WaitForSeconds(2.0f);
        //ovrPlayerController.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
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

}
