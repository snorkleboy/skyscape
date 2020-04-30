using System.Collections;
using System;

namespace Game.Core.Coroutine
{

    public static class GameObjectExtensionCoRoutineHelper
    {
        public static Routiner runRoutine( IEnumerator routine)//this MonoBehaviour go,
        {
            var wroutine = new Routiner(routine);
            //wroutine.unityRoutine = go.StartCoroutine(wroutine);
            return wroutine;
        }
    }


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
        //public Coroutine unityRoutine;
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
                    //Debug.LogWarning("over writing existing subroutine");
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

    public class All : MultiRoutiner
    {
        public All(IEnumerator[] routines) : base(routines)
        {

        }
        public override bool MoveNext()
        {
            RunRoutines();
            return finished = routines.Count > 0;
        }
    }
    public class Any : MultiRoutiner
    {
        private int startCount;
        public Any(IEnumerator[] routines) : base(routines)
        {
            startCount = routines.Length;
        }
        public override bool MoveNext()
        {
            RunRoutines();
            return finished = routines.Count == startCount;
        }
    }


}