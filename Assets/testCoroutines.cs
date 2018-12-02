using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util.Routiner;
public class testCoroutines : MonoBehaviour {

	// Use this for initialization
	public Routiner routiner;
	void Start () 
	{
		routiner = this.runRoutine(startState());
	}
	public void Update(){
		// Debug.Log("UPDATE TICK");
	}
	public IEnumerator startState()
	{
		yield return null;
		while(true){
				Debug.Log("startState");


			yield return new WaitForSeconds(1);
			Debug.Log("Run All Run All Run All Run All Run All Run All Run All Run All ");

			yield return Routiner.All(
				longer(),
				shorter()
			);
			yield return new WaitForSeconds(1);
			Debug.Log("Run Any Run Any Run Any Run Any Run Any Run Any Run Any Run Any ");

			yield return Routiner.Any(
				longer(),
				shorter()
			);
		}
	}
	public IEnumerator longer(){
		for(var i =0; i<5;i++){
			Debug.Log("longer " + i);
			yield return null;
		}
					Debug.Log("longer NESTING");
			yield return Nested();
					Debug.Log("longer UNNESTING");
		for(var i =0; i<5;i++){
			Debug.Log("longer " + i);
			yield return null;
		}
	}
	public IEnumerator shorter(){
		for(var i =0 ; i<5 ; i++){
			Debug.Log("SHORTER " + i);
			yield return null;
		}
	}
		public IEnumerator Nested(){
		for(var i =0 ; i<5 ; i++){
			Debug.Log("Nested " + i);
			yield return null;
		}
	}
	// Update is called once per frame

}
