using System.Collections;
using UnityEngine;
namespace Game.Core.Coroutine
{
    public class WaitRoutine: IEnumerator{
        public float timeToWait;
        public float startTime;
        public float endTime;
        public bool finished{get;set;}
        public WaitRoutine(float timeToWait){
            this.timeToWait = timeToWait;
            this.startTime = Time.time;
            this.endTime = startTime + timeToWait;
        }
        public bool MoveNext(){
            return finished = Time.time < endTime;
        }
        public void Reset(){
            this.startTime = Time.time;
            this.endTime = startTime + timeToWait;
        }
        public object Current{get{return null;}}
    }
}