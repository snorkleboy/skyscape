//namespace Game.Core.State.stateAction
//{
//    [System.Serializable]
//    public class SelfStateActionState : StateActionState
//    {
//        public SelfStateActionState(MonoBehaviour runSource) : base(runSource)
//        {
//        }
//        private void stopPreviousAction()
//        {
//            var msg = "stop coroutine " + stateAction.routineInstance.unityRoutine + " routiner finished:" + stateAction.routineInstance.finished;
//            Debug.Log(msg);
//            previousAction = stateAction;
//            coroutineRunSource.StopCoroutine(previousAction.routineInstance.unityRoutine);
//        }
//        public override void setStateAction(StateAction action)
//        {
//            if (stateAction != null && stateAction.routineInstance.unityRoutine != null)
//            {
//                stopPreviousAction();
//            }
//            stateAction = action;
//            run();
//        }
//    }

//}
