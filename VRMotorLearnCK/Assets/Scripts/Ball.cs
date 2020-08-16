using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour

{
    
    private int x = 0;
    public MyGameManager myGameManager;
    public SaveData saveData;
    private bool in_destroy_process = false;
    private bool ball_was_taken = false;
    private bool is_Hit = false;
    private int ID;
 
    private bool is_grabbed = false;

    private float target_radius;
    private GameObject leftHand, rightHand;
    private Transform leftHandPosition, rightHandPosition;
    // Start is called before the first frame update
    void Start()
    {
        print("start the BAll Script on Start of Object Ball");
        myGameManager = FindObjectOfType<MyGameManager>();
        saveData = FindObjectOfType<SaveData>();
        EventManager.StartListening ("Treffer", registerTreffer);
        ID = myGameManager.get_new_Ball_ID();
        target_radius = myGameManager.get_current_target_radius();
        leftHand = GameObject.Find("LeftHandAnchor");
        rightHand = GameObject.Find("RightHandAnchor");
        leftHandPosition = leftHand.transform;
        rightHandPosition = rightHand.transform;
    }


    // Update is called once per frame
    void Update()
    {
            saveData.add_Ball_Hand_Position(ID, 
            transform.position.x,
            transform.position.y,
            transform.position.z,
            rightHandPosition.position.x, 
            rightHandPosition.position.y, 
            rightHandPosition.position.z, 
            Time.time);
        
        //Debug.Log("Grabbed = " + transform.GetComponent<OVRGrabbable>().isGrabbed);
        
        // test if Ball was grabbed
        if (!is_grabbed && transform.GetComponent<OVRGrabbable>().isGrabbed){
            is_grabbed = true;
            saveData.set_pick_up_time(ID, Time.time);
            ball_was_taken = true; // this is set one time and markes the start of the recording of the ball position
        }
        if (is_grabbed && !transform.GetComponent<OVRGrabbable>().isGrabbed){
            // The ball was grapped in the past and is now released
            // this is necessary if the ball was grapped a second time
            is_grabbed = false;
            saveData.set_leave_the_Hand_Time(ID, Time.time);
        }
    }

    void OnTriggerEnter(Collider other){
        Debug.Log("OnTriggerEnter with "+ other.tag);
        switch (other.tag)
        {
            case "Ring":
                //print("HHHHHHHHHHHHHHHHHHHHHRingTriggerv XXXX");
                saveData.set_Hit(ID, 1);
                EventManager.TriggerEvent("Treffer");
                if (!in_destroy_process){
                    in_destroy_process = true;
                    EventManager.TriggerEvent("Destroy");
                    EventManager.TriggerEvent("SpawnNewBall");
                }  
                break;
            //case "Hole":
            //    EventManager.TriggerEvent("Treffer");
            //    break;
            //case "Ground":
                //print("Ground");
                //EventManager.TriggerEvent("Destroy");
               // break;
            
        }
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("XXXXXXXXXXXXXXXXX   Collided with " + col.gameObject.tag);

        switch(col.gameObject.tag)
        {
            case "Ring":
                //print("Ring Collision.............................");
                break;
             case "Ground":
                //print("Ground Collision.............................");
                if (!in_destroy_process){
                    in_destroy_process = true;
                    EventManager.TriggerEvent("Destroy");
                    EventManager.TriggerEvent("SpawnNewBall");
                }            
                break;
                case "Wall":
                // check whether it is in the target radius
                ContactPoint contact = col.GetContact(0);
                float deviation = Vector3.Distance(contact.point, new Vector3(0f,0f,0f));
                Debug.Log("Hit with deviation of : " + deviation);
                if (deviation<target_radius){
                    Debug.Log("Hit the Wall in Position x =" + transform.position.x + "y=" + transform.position.y + "z="+transform.position.z);
                    saveData.set_Hit(ID, 1);
                    EventManager.TriggerEvent("Treffer");
                    Instantiate(trefferPrefab, contact.point, new Quaternion() )
                }
                //print("HHHHHHHHHHHHHHHHHHHHHRingTriggerv XXXX");
                
                // print("Points colliding: " + col.contacts.Length);
                // print("First point that collided: " + col.contacts[0].point);
                // foreach (ContactPoint contact in col.contacts)
                // {
                //     print(contact.thisCollider.name + " hit " + contact.otherCollider.name + "at point: " + contact.point);
                // }
                if (!in_destroy_process){
                    in_destroy_process = true;
                    EventManager.TriggerEvent("Destroy");
                    EventManager.TriggerEvent("SpawnNewBall");
                }  
                break;

        }
    }

    void OnCollisionExit(Collision col)
    {
        //Debug.Log("XXXXXXXXXXXXXXXXX   Collsion Exit  " + col.gameObject.tag);

        switch(col.gameObject.tag)
        {
            case "Ring":
                //print("Ring Collision.............................");
                break;
             case "Tisch":
//                Debug.Log("Ball was taken from the table");
                break;
        }
    }

    void registerTreffer()
    {
        Debug.Log("HHHHHHHHHHHHHHHHHHHHHHHIIIIIIIIIIIITTTTTT a Hit was registered in the Game Manager");
        saveData.SaveIntoJson();
    }


}


