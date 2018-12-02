using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace util.Routiner
{
    public static class GameObjectExtensionCoRoutineHelper
    {

        public static Routiner runRoutine(this MonoBehaviour go,IEnumerator routine){
            var wroutine =new Routiner(routine);
            go.StartCoroutine(wroutine);
            Debug.Log("STARTED COROTUINE");
            return wroutine;
        }
    }
    public partial class Routiner {
        public static IEnumerator All(params IEnumerator[] routines){
            return new All(routines);
        }
        public static IEnumerator Any(params IEnumerator[] routines){
            return new Any(routines);
        }    
    }
    public partial class Routiner : IEnumerator
    {
        IEnumerator mainRoutine;
        IEnumerator subRoutine;
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
                this.routines.Add(new Routiner(routine));
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