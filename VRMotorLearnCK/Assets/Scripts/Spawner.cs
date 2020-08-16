using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Spawner : MonoBehaviour {
    // analog zu https://learn.unity.com/tutorial/create-a-simple-messaging-system-with-events?signup=true#5cf5960fedbc2a281acd21fa

    public int spawnCount;
    public int activeBalls = 0;
    // [Range (1,100)]
    // public int spawnSize = 1;
    // public float minionOffset = 1;
    public GameObject ball;

//    private UnityAction spawnListener;
//
//    void Awake () {
//        spawnListener = new UnityAction (Spawn);
//    }

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

    void SpawnNewBall () {
        Vector3 spawnPosition = GetSpawnPosition ();
        
        Quaternion spawnRotation = new Quaternion ();
        spawnRotation.eulerAngles = new Vector3 (0.0f, 0.0f);
        //Quaternion spawnRotation = new Quaternion ();
        Debug.Log("spawn new Ball with numAcitveBalls = " + MyGameManager.numActiveBalls);
        //if (MyGameManager.numActiveBalls <= 1)
        //{
        Instantiate (ball, spawnPosition, spawnRotation);

        MyGameManager.numActiveBalls ++;
        //MyGameManager.numActiveBalls += 1;
        //}
        // for (int i = 0; i < spawnCount; i++) {
            

        //     Quaternion spawnRotation = new Quaternion ();
        //     spawnRotation.eulerAngles = new Vector3 (0.0f, Random.Range (0.0f, 360.0f));
        //     if (spawnPosition != Vector3.zero) {
        //         Instantiate (minion, spawnPosition, spawnRotation);
        //     }
        // }
    }

    Vector3 GetSpawnPosition () {
        Vector3 spawnPosition = new Vector3 ();
        spawnPosition = new Vector3(0.217f, 1.068f, 0.53f);
        spawnPosition = new Vector3(0.0f, 0.534f, 0.0f);
        spawnPosition = new Vector3(0.0f, -1.451f, -2.65f);

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