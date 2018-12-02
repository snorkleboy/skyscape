using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace util
{
    public static class GameObjectExtensionCoRoutineHelper
    {
        public static Routiner runRoutine(this MonoBehaviour go,IEnumerator routine){
            var wroutine =new Routiner(routine);
            go.StartCoroutine(wroutine);
            return wroutine;
        }
    }
    
}

namespace util
{
    public partial class Routiner {
        public static IEnumerator All(params IEnumerator[] routines){
            return new All(routines);
        }
        public static IEnumerator Any(params IEnumerator[] routines){
            return new Any(routines);
        }    
        public static IEnumerator wait(float time){
            return new WaitRoutine(time);
        }
    }
    public class WaitRoutine: IEnumerator{
        public float timeToWait;
        public float startTime;
        public float endTime;
        public WaitRoutine(float timeToWait){
            this.timeToWait = timeToWait;
            this.startTime = Time.time;
            this.endTime = startTime + timeToWait;
        }
        public bool MoveNext(){
            return Time.time < endTime;
        }
        public void Reset(){
            this.startTime = Time.time;
            this.endTime = startTime + timeToWait;
        }
        public object Current{get{return null;}}
    }
    public partial class Routiner : IEnumerator
    {
        IEnumerator mainRoutine;
        Routiner subRoutine;

        public bool finished = false;
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
            bool moved;
            if (!(moved = subRoutine.MoveNext() )){
                subRoutine = null;
            }
            return moved;
        }
        private bool runMainRoutine(){
            bool moved = mainRoutine.MoveNext();
            if(moved && mainRoutine.Current is IEnumerator)
            {
                var newSubroutine = (IEnumerator)mainRoutine.Current;
                if (subRoutine != null){
                    Debug.LogWarning("over writing existing subroutine");
                }
                subRoutine = new Routiner(newSubroutine);
            }
            if (!moved){
                finished= true;
            }
            return moved;
        }
        public void Reset()
        {
            throw new System.NotImplementedException("no reset of CR");
        }
        public object Current
        {
            get
            {
                return mainRoutine.Current;
            }
        }

    }
    public abstract class MultiRoutiner :IEnumerator{
        public HashSet<IEnumerator> routines = new HashSet<IEnumerator>();
        public MultiRoutiner(params IEnumerator[] routines){
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
            return routines.Count > 0;
        }
    }
    public class Any:MultiRoutiner{
        private int startCount;
        public Any(IEnumerator[] routines):base(routines){
            startCount = routines.Length;
        }
        public override bool MoveNext(){
            RunRoutines();
            return routines.Count == startCount;
        }
    }
}