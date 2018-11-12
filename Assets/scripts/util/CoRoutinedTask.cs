using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
namespace Util
{
    public class CoRoutinedTask
    {
        private Task task;
        IEnumerable actions;
        int i = 0;
        public IEnumerator performTasks(List<System.Action> actions){
            Debug.Log("\n performTasks Routine Called\n");
            this.actions = actions;
            var hasActions = actions.Count>0;
            while(true){
                var runningTask = task != null && !task.IsCompleted;
                var iInRange = i<actions.Count;
                var shouldBreak = (!(iInRange) || !(hasActions)) && !runningTask;
                if (shouldBreak){
                    break;
                }
                yield return new WaitForSeconds(.1f);
                if (!runningTask){
                    Debug.Log("running new task, num:"+i);
                    task = Task.Run(()=>{
                        actions[i].Invoke();
                        i++;
                    });
                }
            }
            Debug.Log("performTasks Finished");
        }
        
    }
}