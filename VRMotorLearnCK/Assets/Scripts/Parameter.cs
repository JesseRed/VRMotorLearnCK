using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameter : MonoBehaviour
{
    public GameSession gameSession;
    public LineObj lineObj;
    public Paradigma paradigma;
    public MyGameManager myGameManager;

//general difficulty ... each offset scales with this factor
    public float difficulty = 1.0f;
    
    public float current_target_radius;
    public float current_ball_mass;
    public Vector3 current_gravity;
    public Vector3 current_offset_hand_pos;
    public Vector3 current_offset_hand_vel;
    public Vector3 current_invert;
    public Vector4 current_tremor; // der letzte Eintrag fuer die Frequenz
    
    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        
        lineObj = FindObjectOfType<LineObj>(); // Zeichnet den Zielkreis auf die Wand
        myGameManager = FindObjectOfType<MyGameManager>();

        // set initial values
        current_target_radius = gameSession.paradigma.target_size;
        current_ball_mass = gameSession.paradigma.ball_mass;
        current_gravity = initialize_gravity();
        current_offset_hand_pos = initialize_offset_hand_pos();
        current_offset_hand_vel = initialize_offset_hand_vel();
        current_invert = new Vector3(1.0f, 1.0f, 1.0f);
        current_tremor = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
        
        //spawner = FindObjectOfType<Spawner>();
        
        //Debug.Log("Awake");
    }

    // Update is called once per frame

    
    public void configureParameterForNextBall()
    {
        Debug.Log("into configure_ParameterForNextBall");
        current_target_radius = estimate_current_target_radius(current_target_radius);
        Debug.Log("current_target_radius = " + current_target_radius);
        //* Estimate Gravity ggf. change in 
        float gravity_X = Random.Range(gameSession.paradigma.gravity_X_min, gameSession.paradigma.gravity_X_max);
        float gravity_Y = Random.Range(gameSession.paradigma.gravity_Y_min, gameSession.paradigma.gravity_Y_max);
        float gravity_Z = Random.Range(gameSession.paradigma.gravity_Z_min, gameSession.paradigma.gravity_Z_max);
        //Physics.gravity = new Vector3(gravity_X, gravity_Y, gravity_Z);
        current_gravity = new Vector3(gravity_X, gravity_Y, gravity_Z);
//        current_offset_hand_pos = new Vector3(1.2f,0.0f,0.0f);
        current_offset_hand_pos = estimate_offset_hand_pos(current_offset_hand_pos);
        current_offset_hand_vel = estimate_offset_hand_vel(current_offset_hand_vel);
        current_invert = estimate_invert();
        current_tremor = estimate_tremor();
    }

    public Vector3 get_gravitiy(){
        return current_gravity;
    }

    public Vector3 estimate_offset_hand_pos(Vector3 old_offset_hand_pos){
        Vector3 new_offset_hand_pos = old_offset_hand_pos;
        if (gameSession.paradigma.Adaptive==0){
            return new Vector3(0.0f, 0.0f, 0.0f);

        }
        else{
            float new_offset_hand_pos_X = Random.Range(gameSession.paradigma.offset_hand_pos_X_min, gameSession.paradigma.offset_hand_pos_X_max) * difficulty;
            float new_offset_hand_pos_Y = Random.Range(gameSession.paradigma.offset_hand_pos_Y_min, gameSession.paradigma.offset_hand_pos_Y_max) * difficulty;
            float new_offset_hand_pos_Z = Random.Range(gameSession.paradigma.offset_hand_pos_Z_min, gameSession.paradigma.offset_hand_pos_Z_max) * difficulty;
            new_offset_hand_pos = new Vector3(new_offset_hand_pos_X, new_offset_hand_pos_Y, new_offset_hand_pos_Z);
            //Debug.Log("in Parameter new_offst_hand_pos = " + new_offset_hand_pos);
        }    
        return new_offset_hand_pos;
    }

    public Vector3 estimate_offset_hand_vel(Vector3 old_offset_hand_vel){
        Vector3 new_offset_hand_vel = old_offset_hand_vel;
        if (gameSession.paradigma.Adaptive==0){
            return new Vector3(0.0f, 0.0f, 0.0f);

        }
        else{
            float new_offset_hand_vel_X = Random.Range(gameSession.paradigma.offset_hand_vel_X_min, gameSession.paradigma.offset_hand_vel_X_max) * difficulty;
            float new_offset_hand_vel_Y = Random.Range(gameSession.paradigma.offset_hand_vel_Y_min, gameSession.paradigma.offset_hand_vel_Y_max) * difficulty;
            float new_offset_hand_vel_Z = Random.Range(gameSession.paradigma.offset_hand_vel_Z_min, gameSession.paradigma.offset_hand_vel_Z_max) * difficulty;
            new_offset_hand_vel = new Vector3(new_offset_hand_vel_X, new_offset_hand_vel_Y, new_offset_hand_vel_Z);
            Debug.Log("in Parameter new_offst_hand_pos = " + new_offset_hand_vel);
        }    
        return new_offset_hand_vel;
    }

    public Vector3 estimate_invert()
    {
        Vector3 new_invert = new Vector3(1.0f, 1.0f, 1.0f);
        if (gameSession.paradigma.hand_invert>0) {
            int rnd = Random.Range(1,8);
            switch (rnd)
            {
                case 1:
                    new_invert = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
                case 2:
                    new_invert = new Vector3(1.0f, 1.0f, -1.0f);
                    break;
                case 3:
                    new_invert = new Vector3(1.0f, -1.0f, 1.0f);
                    break;
                case 4:
                    new_invert = new Vector3(-1.0f, 1.0f, 1.0f);
                    break;
                case 5:
                    new_invert = new Vector3(1.0f, -1.0f, -1.0f);
                    break;
                case 6:
                    new_invert = new Vector3(-1.0f, 1.0f, -1.0f);
                    break;
                case 7:
                    new_invert = new Vector3(-1.0f, -1.0f, 1.0f);
                    break;
                case 8:
                    new_invert = new Vector3(-1.0f, -1.0f, -1.0f);
                    break;
            }
        }
        return new_invert; 
    }

    public Vector4 estimate_tremor()
    {
        return new Vector4(gameSession.paradigma.hand_tremor_X, gameSession.paradigma.hand_tremor_X, gameSession.paradigma.hand_tremor_Z, gameSession.paradigma.hand_tremor_freq);

    }

    public float estimate_success_rate_all(){
        float rate = -1.0f;
        if (gameSession.playerData._Balls.Count>2) 
        {
            Debug.Log("num_hits = " + gameSession.playerData.num_hits);
            Debug.Log("ball count = " + gameSession.playerData._Balls.Count);
            rate = (float)gameSession.playerData.num_hits/gameSession.playerData._Balls.Count;
        }
        Debug.Log("estimated Success rate in Parameter = " + rate);
        return rate;
    }

    public float estimate_current_target_radius(float old_target_radius){
        // Anpassung nur wenn schon min 3 Baelle geworfen wurden
        float new_target_radius = old_target_radius;
        int num_balls = gameSession.playerData._Balls.Count;
        if (num_balls>=3){
            if (estimate_success_rate_all()>gameSession.paradigma.desired_hit_rate) {
                // verkleinere nur wenn der letzte BAll ein Treffer war
                if (gameSession.playerData._Balls[num_balls-1].is_Hit==1){
                    new_target_radius= old_target_radius - old_target_radius * 0.1f;
                }
            }
            else {
                // vergroessere nur wenn der letzte Ball ein Fehler war
                if (gameSession.playerData._Balls[num_balls-1].is_Hit==0){
                    new_target_radius= old_target_radius + old_target_radius * 0.1f;
                }
            }
        }
        
        if (new_target_radius>=0.5f)
        {
            new_target_radius = 0.5f;
        }
        return new_target_radius;
    }

    private Vector3 initialize_gravity(){
        float gravity_X = Random.Range(gameSession.paradigma.gravity_X_min, gameSession.paradigma.gravity_X_max);
        float gravity_Y = Random.Range(gameSession.paradigma.gravity_Y_min, gameSession.paradigma.gravity_Y_max);
        float gravity_Z = Random.Range(gameSession.paradigma.gravity_Z_min, gameSession.paradigma.gravity_Z_max);
        //Physics.gravity = new Vector3(gravity_X, gravity_Y, gravity_Z);
        return new Vector3(gravity_X, gravity_Y, gravity_Z);
           
    }

    private Vector4 initialize_offset_hand_pos()
    {
        return new Vector4(1.0f, 0.0f, 0.0f, 5.0f);
    }
    private Vector3 initialize_offset_hand_vel()
    {
            float new_offset_hand_vel_X = Random.Range(gameSession.paradigma.offset_hand_vel_X_min, gameSession.paradigma.offset_hand_vel_X_max) * difficulty;
            float new_offset_hand_vel_Y = Random.Range(gameSession.paradigma.offset_hand_vel_Y_min, gameSession.paradigma.offset_hand_vel_Y_max) * difficulty;
            float new_offset_hand_vel_Z = Random.Range(gameSession.paradigma.offset_hand_vel_Z_min, gameSession.paradigma.offset_hand_vel_Z_max) * difficulty;
            return new Vector3(new_offset_hand_vel_X, new_offset_hand_vel_Y, new_offset_hand_vel_Z);
            
    }

}
