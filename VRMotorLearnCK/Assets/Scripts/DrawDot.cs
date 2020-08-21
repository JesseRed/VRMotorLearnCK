using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawDot : MonoBehaviour
{

    //LineRenderer myDotlineRenderer;
    public Vector3 draw_origin;
    // Start is called before the first frame update
    public float max_radius = 0.131f;
    public float steps = 30;
    public float width = 0.014f;

    public int segments = 60;
    private Vector3 my_col;
    private IEnumerator coroutine;
    public GameObject wall;



    void Start()
    {
        wall = GameObject.Find("Wall");
    }



    public void drawTheDot(Vector3 new_origin, bool is_hit)
    {
        if (is_hit)
        {
            my_col = new Vector3(0.0f, 1.0f, 0.0f);
        }else{
            my_col = new Vector3(1.0f, 0.0f, 0.0f);
        }

        coroutine = Fade2(new_origin);
        StartCoroutine(coroutine);
       
    }


    private IEnumerator Fade2(Vector3 origin)
    {
        //transform.position = origin;
        //myDotlineRenderer lineRenderer = GetComponent<LineRenderer>();
        LineRenderer myDotlineRenderer = gameObject.AddComponent<LineRenderer>();
        myDotlineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        myDotlineRenderer.useWorldSpace = false;
        myDotlineRenderer.startWidth = width;
        myDotlineRenderer.endWidth = width;
        myDotlineRenderer.positionCount = segments + 1;
        
        float initial_alpha = 1.0f;
        float current_alpha = initial_alpha;

        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        //yield return new WaitForSeconds(0.3f);
        for (float current_radius=0.0f; current_radius<max_radius; current_radius+=max_radius/steps){

            for (int i = 0; i < pointCount; i++)
            {
                var rad = Mathf.Deg2Rad * (i * 360f / segments);
                points[i] = new Vector3(Mathf.Sin(rad) * current_radius + origin[0]  ,  Mathf.Cos(rad) * current_radius + origin[1], -0.1f);
                points[i] = new Vector3(Mathf.Sin(rad) * current_radius + origin[0]/wall.transform.localScale.x,  Mathf.Cos(rad) * current_radius + origin[1]/wall.transform.localScale.y, -0.1f);
            }
            //Debug.Log("x center ... " + points[1][0]);
            //Debug.Log("origin x " + origin[0]);
            if (current_radius>=current_radius/2.0f){
                current_alpha -= initial_alpha/(steps/2);
            }
            Color c1 = new Color(my_col[0], my_col[1], my_col[2], current_alpha);
            myDotlineRenderer.startColor=c1;
            myDotlineRenderer.endColor=c1;
            myDotlineRenderer.SetPositions(points);
            yield return null;
         //new WaitForSeconds(0.01f);
        }    
        Destroy(myDotlineRenderer);
    }

}
