using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class extension
{
    public static Vector3 toVector(this Transform t)
    {
        return new Vector3(t.position.x, t.position.y, t.position.z);
    }
    public static Quaternion toQuat(this Transform t)
    {
        return new Quaternion(t.rotation.x, t.rotation.y, t.rotation.z, t.rotation.w);
    }
}
public class galaxyCreator : MonoBehaviour {

    private int numBranches = 3;
    private int branchSize = 10;
    private int starToStarDistance = 20;
    private int featherSize = 5;
    private int centralNoHabZoneRadius = 30;
    private int featheryness;
    private int interFeatherConnectedness;
    private int interBranchConnectedness;
    private int outerConnectedNess;
    private int innerConnectedNess;

    private int planaterySystemAverageSize = 4;
    private double emptySystemRate = .4;

    public GameObject holder;
    public GameObject baseStarFab;

    public void create()
    {
        Debug.Log("create " + numBranches);

        for (int i = 0; i < numBranches; i++)
        {
            Debug.Log("create branch");
            createBranch(i);
        }
    }
    private void createBranch(int branchI)
    {
        var first = Instantiate(baseStarFab, holder.transform);
        first.transform.RotateAround(Vector3.zero,Vector3.up, branchI*(360 / numBranches));
        first.transform.Translate(first.transform.forward * centralNoHabZoneRadius);
        
        for (int i = 1; i < branchSize; i++)
        {
            createStarSystem(first, i);
        }
    }
    private GameObject createStarSystem(GameObject first, int i)
    {
        Debug.Log("create star system");
        var newStar = Instantiate(baseStarFab, first.transform);
        newStar.transform.Translate(first.transform.forward * i * starToStarDistance);
        return newStar;
    }

}
