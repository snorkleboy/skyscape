using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineBetweenPoints : MonoBehaviour {

	public GameObject[] targets= new GameObject[2];
    public void setTarget(GameObject target, int i)
    {
        targets[i] = target;
    }
    public void setTargets(GameObject[] targets)
    {
        this.targets = targets;
    }
    public LineRenderer lineRenderer;

    [ContextMenu("draw")]
    public void draw()
    {
        lineRenderer.SetPosition(0, targets[0].transform.position);
        lineRenderer.SetPosition(1, targets[1].transform.position);
    }
    public void Start()
    {
        lineRenderer.startWidth = 3;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
    }

}
