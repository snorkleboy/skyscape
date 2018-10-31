using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using System;
namespace UI
{
	public struct clickViews{
		public clickViews(Action<IViewable> mainViewCallBack,Action<IContextable> contextViewCallback,Action<IActOnable> actionViewCallBack){
				this.mainViewCallBack = mainViewCallBack;
				this.contextViewCallback = contextViewCallback;
				this.actionViewCallBack = actionViewCallBack;
		}
		public Action<IViewable> mainViewCallBack{get;private set;}
		public Action<IContextable> contextViewCallback{get;private set;}
		public Action<IActOnable> actionViewCallBack{get;private set;}

	}
	public class PlanetClickView : BaseUIScript{
		public MainView mainview;
		public GameObject contextPane;
		public GameObject actionPane;

		private clickViews callbacks;
		public void Awake(){
			callbacks = new clickViews(renderMain,renderContext,renderActions);
		}
		protected override void refresh(){
			if (lastUpdateId != _toDisplay.updateId){
				render();
			}
		}
		protected override void render(){
			if (_toDisplay != null){
				renderMain(_toDisplay);
			}else{
				Debug.LogWarning(this + " called render without _toDisplay set  ");
			}
		}
		protected void renderMain(IViewable viewable){
			lastUpdateId = viewable.updateId;
			mainview.render(viewable,callbacks);
		}
		protected void renderContext(IContextable contextable){
		Debug.Log("renderContext callback called, proof:" + contextable);
			contextable.renderContext(contextPane.transform,callbacks);
		}
		protected void renderActions(IActOnable actionable){
			Debug.Log("renderActions callback called, proof:" + actionable);
			actionable.renderActionView(actionPane.transform,callbacks);
		}
	}

}
