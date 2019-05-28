using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace util
{
    public static class GameObjectExtensionCoRoutineHelper
    {
        public static Routiner runRoutine(this MonoBehaviour go,IEnumerator routine){
            var wroutine = new Routiner(routine);
            wroutine.unityRoutine = go.StartCoroutine(wroutine);
            return wroutine;
        }
    }
    
}

namespace util
{

    [Serializable]
    public class Routiner:IEnumerator {
        public static IEnumerator All(params IEnumerator[] routines){
            return new All(routines);
        }
        public static IEnumerator Any(params IEnumerator[] routines){
            return new Any(routines);
        }    
        public static IEnumerator wait(float time){
            return new WaitRoutine(time);
        }
        public IEnumerator mainRoutine;
        public Routiner subRoutine;

        public bool finished{get;set;}
        public Coroutine unityRoutine;
        public Routiner(IEnumerator routine)
        {
            mainRoutine = routine;
        }
        public bool MoveNext()
        {
            if (subRoutine != null){
                if (runSubRoutine()){
                    return true;
                }else{
                    return runMainRoutine();
                }
            }else{
                return runMainRoutine();
            }
        }
        private bool runSubRoutine(){
            bool moved = subRoutine.MoveNext();
            if (!moved){
                Current = subRoutine.Current;
                subRoutine = null;
            }
            return moved;
        }
        private bool runMainRoutine(){
            bool moved = mainRoutine.MoveNext();
            Current = mainRoutine.Current;
            if(moved && mainRoutine.Current is IEnumerator)
            {
                var newSubroutine = (IEnumerator)mainRoutine.Current;
                if (subRoutine != null){
                    Debug.LogWarning("over writing existing subroutine");
                }
                subRoutine = new Routiner(newSubroutine);
            }
            finished = moved;
            return moved;
        }
        public void Reset()
        {
            throw new System.NotImplementedException("no reset of CR");
        }
        public object Current{get;set; }

    }
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
    public abstract class MultiRoutiner :IEnumerator{
        public HashSet<IEnumerator> routines = new HashSet<IEnumerator>();
        public bool finished{get;set;}
        public MultiRoutiner(params IEnumerator[] routines){
            finished = false;
            foreach (var routine in routines)
            {
                this.routines.Add(new util.Routiner(routine));
            }
        }
        public abstract bool MoveNext();
        protected void RunRoutines(){
            var toRemove = new List<IEnumerator>();
            
            foreach(var routine in routines)
            {
                if(!routine.MoveNext())
                {
                    toRemove.Add(routine);
                }
            }
            foreach (var routine in toRemove)
            {
                routines.Remove(routine);
            }
        }

        public void Reset()
        {
            throw new System.NotImplementedException("no reset of CR");
        }
        public object Current
        {
            get
            {
                return null;
            }
        }
    }


    public class All:MultiRoutiner{
        public All(IEnumerator[] routines):base(routines){

        }
        public override bool MoveNext(){
            RunRoutines();
            return finished = routines.Count > 0;
        }
    }
    public class Any:MultiRoutiner{
        private int startCount;
        public Any(IEnumerator[] routines):base(routines){
            startCount = routines.Length;
        }
        public override bool MoveNext(){
            RunRoutines();
            return finished = routines.Count == startCount;
        }
    }
}