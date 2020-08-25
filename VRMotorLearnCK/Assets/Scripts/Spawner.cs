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
    public GameObject ball;

//    private UnityAction spawnListener;
//
//    void Awake () {
//        spawnListener = new UnityAction (Spawn);
//    }
 //   public MyGameManager myGameManager;
    //public List<BallInfo> _BallInfos = new List<BallInfo>();
    // public MyBallCollection myBallCollection = new MyBallCollection();

    void Start()
    {
//        myGameManager = FindObjectOfType<MyGameManager>();
    }



    public void Spawn_A_NewBall () {
        // Wenn ein neuer Ball gespawned wird dann
        // muss auch vom Gamemanager der aktuelle Zielradius erfragt werden
        // und dieser sollte gezeichnet werden
        Vector3 spawnPosition = GetSpawnPosition ();
        
        Quaternion spawnRotation = new Quaternion ();
        spawnRotation.eulerAngles = new Vector3 (0.0f, 0.0f);
        //Quaternion spawnRotation = new Quaternion ();

        Instantiate (ball, spawnPosition, spawnRotation);
         
    }

    Vector3 GetSpawnPosition () {
        Vector3 spawnPosition = new Vector3 ();
        spawnPosition = new Vector3(0.217f, 1.068f, 0.53f);
        spawnPosition = new Vector3(0.0f, 0.534f, 0.0f);
        spawnPosition = new Vector3(0.0f, -0.951f, -2.65f);

        // Vector3 spawnPosition = new Vector3 ();
        // float startTime = Time.realtimeSinceStartup;
        // bool test = false;
        // while (test == false) {
        //     Vector2 spawnPositionRaw = Random.insideUnitCircle * spawnSize;
        //     spawnPosition = new Vector3 (spawnPositionRaw.x, minionOffset, spawnPositionRaw.y);
        //     test = !Physics.CheckSphere (spawnPosition, 0.75f);
        //     if (Time.realtimeSinceStartup - startTime > 0.5f) {
        //         Debug.Log ("Time out placing Minion!");
        //         return Vector3.zero;
        //     }
        // }
        return spawnPosition;
    }
}