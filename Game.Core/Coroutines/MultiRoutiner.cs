using System.Collections;
using System.Collections.Generic;

namespace Game.Core.Coroutine
{
    public abstract class MultiRoutiner :IEnumerator{
        public HashSet<IEnumerator> routines = new HashSet<IEnumerator>();
        public bool finished{get;set;}
        public MultiRoutiner(params IEnumerator[] routines){
            finished = false;
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
}