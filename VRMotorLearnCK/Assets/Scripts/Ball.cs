using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour

{
    
    private int x = 0;
    public MyGameManager myGameManager;
    public GameSession gameSession;
    //public PlayerData playerData;
    private bool in_destroy_process = false;
    private bool ball_was_taken = false;
    private bool is_Hit = false;
    private int ID;
    
    private OVRPlayerController ovrPlayerController;
    public DrawDot drawDot;
    private bool is_grabbed = false;
    public GameObject wall;
    private GameObject leftHand, rightHand;
    private Transform leftHandPosition, rightHandPosition;
    // Start is called before the first frame update
    public GameObject trefferPrefab;
    public LineObj lineObj;
    
    public Parameter parameter;
    public Rigidbody rb;

    //* alle Variablen die mit einem "par_" beginnen muessen mit dem aktuellen
    //* Ball gespeichert werden und beschreiben die Wurfparameter
    private float par_target_radius;
    public float par_mass;
    public Vector3 par_gravity;
    public Vector3 par_offset_hand_pos;

    void Start()
    {
        print("start the BAll Script on Start of Object Ball");
        myGameManager = FindObjectOfType<MyGameManager>();
        gameSession = FindObjectOfType<GameSession>();
        parameter = FindObjectOfType<Parameter>();
        EventManager.StartListening ("Treffer", registerTreffer);
        ovrPlayerController = FindObjectOfType<OVRPlayerController>();
        wall = GameObject.Find("Wall");
        drawDot = FindObjectOfType<DrawDot>();
        lineObj = FindObjectOfType<LineObj>();
        //leftHand = GameObject.Find("LeftHandAnchor");
        //rightHand = GameObject.Find("RightHandAnchor");
        leftHand = GameObject.Find("CustomHandLeft");
        rightHand = GameObject.Find("CustomHandRight");
        rightHand = GameObject.Find("RightHandAnchor");
        Debug.Log(rightHand.ToString());
        //rightHand.transform = new Vector3(1.0f, -1.0f, 1.0f);

        rb = GetComponent<Rigidbody>(); 
        

        ID = myGameManager.get_current_Ball_ID();
        normalize_hand_controller();
        set_target_radius();
        set_physics();
        //set_hand_controller();
        
        //Debug.Log("wall scale = " + wall.transform.localScale.x ToString());
        //TODO Die Methode muss noch angepasst werden um alle Paramter mit zu speichern
        gameSession.register_new_Ball(
            ID, 
            parameter.current_target_radius,
            parameter.current_ball_mass, 
            parameter.current_gravity,
            parameter.current_force,
            parameter.current_offset_hand_pos, 
            parameter.current_offset_hand_vel,
            parameter.current_invert,
            parameter.current_tremor
            );
    }

    //* das Paramter Object wird durch den Gamemanager immer vor dem Spawnen 
    // eines neuen Balls geschrieben und ist zum Zeitpunkt des Balls
    private void set_target_radius()
    {
        par_target_radius = parameter.current_target_radius;
        Debug.Log("target_radius in ball = " + par_target_radius);
        lineObj.drawCircleX(par_target_radius, 0.051f);

    }

    private void set_physics()
    {

        par_mass = parameter.current_ball_mass;
        rb.mass = par_mass;
        rb.useGravity = true;
        //Debug.Log("par_mas = " + par_mas);
        par_gravity = parameter.get_gravitiy();
        Physics.gravity = par_gravity;

    }

    private void set_hand_controller()
    {
        Debug.Log("set Hand controller()...");
        Debug.Log("parameter.current_offset_hand_pos = " + parameter.current_offset_hand_pos);
        Debug.Log("parameter.current_offset_hand_vel = " + parameter.current_offset_hand_vel);
        Debug.Log("parameter.current_invert = " + parameter.current_invert);
        Debug.Log("parameter.current_tremor = " + parameter.current_tremor);
        OVRPlugin.carsten_offset_hand_pos = parameter.current_offset_hand_pos;
        OVRPlugin.carsten_offset_hand_vel = parameter.current_offset_hand_vel;
        OVRPlugin.carsten_invert = parameter.current_invert;
        OVRPlugin.carsten_tremor = parameter.current_tremor;       
        //rightHand.transform.position = rightHand.transform.position  + parameter.current_offset_hand_pos;
        //rightHand.transform.position = rightHand.transform.position  + new Vector3(0.2f, 0.0f, 0.0f);
        Debug.Log("offset = " + parameter.current_offset_hand_pos.ToString());
        Debug.Log("transform right hand to " + rightHand.transform.position.ToString());
        
    }

    private void normalize_hand_controller()
    {
        OVRPlugin.carsten_offset_hand_pos = new Vector3(0.0f, 0.0f, 0.0f);
        OVRPlugin.carsten_offset_hand_vel = new Vector3(1.0f, 1.0f, 1.0f);
        OVRPlugin.carsten_invert = new Vector3(0.0f, 0.0f, 0.0f);;
        OVRPlugin.carsten_tremor = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);;       
    }

    // Update is called once per frame
    void Update()
    {
            gameSession.add_Ball_Hand_Position(ID, 
            transform.position.x,
            transform.position.y,
            transform.position.z,
            rightHand.transform.position.x, 
            rightHand.transform.position.y, 
            rightHand.transform.position.z, 
            Time.time);
        
        //Debug.Log("Grabbed = " + transform.GetComponent<OVRGrabbable>().isGrabbed);
        
        // test if Ball was grabbed
        if (!is_grabbed && transform.GetComponent<OVRGrabbable>().isGrabbed){
            is_grabbed = true;
            gameSession.set_pick_up_time(ID, Time.time);
            ball_was_taken = true; // this is set one time and markes the start of the recording of the ball position
            OVRPlugin.carsten_ball_grap_position = transform.position;
            intitialize_forces_on_ball();
            set_hand_controller();
            //rb.useGravity = true;
        }
        if (is_grabbed && !transform.GetComponent<OVRGrabbable>().isGrabbed){
            // The ball was grapped in the past and is now released
            // this is necessary if the ball was grapped a second time
            is_grabbed = false;
            gameSession.set_leave_the_Hand_Time(ID, Time.time);
        }
    }

    private void intitialize_forces_on_ball()
    {
        rb.AddForce(parameter.current_force);
    }

    void OnTriggerEnter(Collider other){
        Debug.Log("OnTriggerEnter with "+ other.tag);
        switch (other.tag)
        {
            case "Ring":
                //print("HHHHHHHHHHHHHHHHHHHHHRingTriggerv XXXX");
                gameSession.set_Hit(ID, 1);
                EventManager.TriggerEvent("Treffer");
                if (!in_destroy_process){
                    in_destroy_process = true;
                    EventManager.TriggerEvent("Destroy");
                    EventManager.TriggerEvent("SpawnNewBall");
                }  
                break;

            
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //Debug.Log("XXXXXXXXXXXXXXXXX   Collided with " + col.gameObject.tag);

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
                float deviation = Vector3.Distance(contact.point, new Vector3(0f,0f,-0.1f))/((wall.transform.localScale.x+wall.transform.localScale.y)/2);
                Debug.Log("Hit with deviation of : " + deviation);
                bool is_hit = false;
                if (deviation<par_target_radius){
                    is_hit = true;
                    Debug.Log("Hit the Wall in Position x =" + transform.position.x + "y=" + transform.position.y + "z="+transform.position.z);
                    gameSession.set_Hit(ID, 1);
                    EventManager.TriggerEvent("Treffer");
                    //Instantiate(trefferPrefab, contact.point, new Quaternion() );
                }
                drawDot.drawTheDot(contact.point, is_hit);
                Debug.Log("deviation = " + deviation + "   vs. par_target_radius = " + par_target_radius);
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
        gameSession.SaveIntoJson();
    }


}


