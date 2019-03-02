using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineBetweenPoints : MonoBehaviour {

	public Vector3[] targets= new Vector3[2];
    public void setTarget(GameObject target, int i)
    {
        targets[i] = target.transform.position;
    }
    public void setTarget(Vector3 target, int i)
    {
        targets[i] = target;
    }
    public void setTargets(GameObject[] targets)
    {
        this.targets[0] = targets[0].transform.position;
        this.targets[1] = targets[1].transform.position;

    }
    public LineRenderer lineRenderer;

    [ContextMenu("draw")]
    public void draw()
    {
        lineRenderer.SetPosition(0, targets[0]);
        lineRenderer.SetPosition(1, targets[1]);
    }
    public void Start()
    {
        lineRenderer.startWidth = 3;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
    }

}
