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

		private List<IContextable> renderedContextableStack = new List<IContextable>();
		private void addToContextableStack(IContextable el){
			renderedContextableStack.Add(el);
			renderContext();
		}
		private List<IViewable> renderedViewableStack = new List<IViewable>();
		private void addToViewableStack(IViewable el){
			renderedViewableStack.Add(el);
			renderMain();
		}

		private List<IActOnable> renderedActOnableStack = new List<IActOnable>();
		private void addToActOnAbleStack(IActOnable el){
			renderedActOnableStack.Add(el);
			renderActions();
		}

		private clickViews callbacks;
		public void Awake(){
			callbacks = new clickViews(addToViewableStack,addToContextableStack,addToActOnAbleStack);
		}
		protected override void refresh(){
			if (lastUpdateId != _toDisplay.updateId){
				render();
			}
		}
		protected override void render(){
			if (_toDisplay != null){
				renderedContextableStack = new List<IContextable>();
				renderedViewableStack = new List<IViewable>(){_toDisplay};
				renderedActOnableStack = new List<IActOnable>();
				renderMain();
			}else{
				Debug.LogWarning(this + " called render without _toDisplay set  ");
			}
		}
		protected void renderMain(){
			var viewable = renderedViewableStack[renderedViewableStack.Count-1];
			lastUpdateId = viewable.updateId;
			mainview.render(viewable,callbacks);
		}
		public void goBackOnMain(){
			if (renderedViewableStack.Count > 1){
				renderedViewableStack.RemoveAt(renderedViewableStack.Count -1);
				renderMain();
			}
		}

		protected void renderContext(){
			var contextable = renderedContextableStack[renderedContextableStack.Count-1];
			clear(contextPane);
			var rendered = contextable.renderContext(contextPane.transform,callbacks);
		}
		public void goBackOnContext(){
			if (renderedContextableStack.Count > 1){
				renderedContextableStack.RemoveAt(renderedContextableStack.Count -1);
				renderContext();
			}
		}
		protected void renderActions(){
			var actionable = renderedActOnableStack[renderedActOnableStack.Count-1];
			renderedActOnableStack.Add(actionable);
			clear(actionPane);
			actionable.renderActionView(actionPane.transform,callbacks);
		}
		public void goBackOActions(){
			if (renderedActOnableStack.Count > 1){
				renderedActOnableStack.RemoveAt(renderedActOnableStack.Count -1);
				renderActions();
			}
		}
		private void clear(GameObject gameObject){
				foreach(Transform child in gameObject.transform){
					Destroy(child.gameObject);
				}
			}
		}

}
