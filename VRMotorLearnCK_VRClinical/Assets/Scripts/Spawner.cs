using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Spawner : MonoBehaviour {
    // analog zu https://learn.unity.com/tutorial/create-a-simple-messaging-system-with-events?signup=true#5cf5960fedbc2a281acd21fa

    //public int spawnCount;
    //public int activeBalls = 0;
    // [Range (1,100)]
    // public int spawnSize = 1;
    // public float minionOffset = 1;
    private GameObject leftHand, rightHand, rightEye;
    public GameObject ball;
private Vector3 initalBallSpawnPosition;
//    private UnityAction spawnListener;
//
    void Awake () {
        //spawnListener = new UnityAction (Spawn);
        leftHand = GameObject.Find("LeftHandAnchor");
        rightHand = GameObject.Find("RightHandAnchor");
        rightEye = GameObject.Find("RightEyeAnchor");
    }
 //   public MyGameManager myGameManager;
    //public List<BallInfo> _BallInfos = new List<BallInfo>();
    // public MyBallCollection myBallCollection = new MyBallCollection();
    private bool first_ball;
    void Start()
    {
        first_ball = true;
//        myGameManager = FindObjectOfType<MyGameManager>();

    }



    public void Spawn_A_NewBall () {
        // Wenn ein neuer Ball gespawned wird dann
        // muss auch vom Gamemanager der aktuelle Zielradius erfragt werden
        // und dieser sollte gezeichnet werden
        Vector3 spawnPosition = GetSpawnPosition ();
        
        float sec=2.0f; 
        if (first_ball) {
            sec =0.1f;
            first_ball = false;
        }
        StartCoroutine(InstantiateNow(spawnPosition, sec));
        
         
    }

    Vector3 GetSpawnPosition () {
        Vector3 spawnPosition = new Vector3 ();
        //spawnPosition = new Vector3(0.217f, 1.068f, 0.53f);
        //spawnPosition = new Vector3(0.0f, 0.534f, 0.0f);
        //spawnPosition = new Vector3(0.0f, -0.951f, -2.65f);
        spawnPosition = new Vector3(rightEye.transform.position.x, rightEye.transform.position.y, rightEye.transform.position.z+0.2f);
        Debug.Log("spawnPosition=" + spawnPosition.ToString());
        Debug.Log("spawnrightEyePosition=" + rightEye.transform.position.ToString());
        if (first_ball) {
            initalBallSpawnPosition = spawnPosition;
        }else{
            return initalBallSpawnPosition;
        }
        
//        spawnPosition = new Vector3(0.0f, 0.0f, -2.65f);

        return spawnPosition;
    }        


    IEnumerator  InstantiateNow(Vector3 spawnPosition, float sec) 
    {
        yield return new WaitForSeconds (sec);
        Quaternion spawnRotation = new Quaternion ();
        spawnRotation.eulerAngles = new Vector3 (0.0f, 0.0f);

        Instantiate (ball, spawnPosition, spawnRotation);
    }



}