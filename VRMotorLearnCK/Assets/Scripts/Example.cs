using UnityEngine;

public class Example : MonoBehaviour
{
    // Creates a line renderer that follows a Sin() function
    // and animates it.

    Color c1 = Color.yellow;
    Color c2 = Color.red;
    int lengthOfLineRenderer = 20;

    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(0.2f, 0.2f);
        lineRenderer.SetVertexCount(lengthOfLineRenderer);
    }

    void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        var points = new Vector3[lengthOfLineRenderer];
        var t = Time.time;
        for (int i = 0; i < lengthOfLineRenderer; i++)
        {
            points[i] = new Vector3(i * 0.5f, Mathf.Sin(i + t), -0.2f);
        }
        lineRenderer.SetPositions(points);
    }
}