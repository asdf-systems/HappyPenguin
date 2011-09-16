using System;
using HappyPenguin.Entities;
using UnityEngine;
using System.Linq;
using System.Collections;

namespace HappyPenguin.Entities
{
	public class SymbolProjector 
	
	
	{
		private TargetableEntityBehaviour entity;
		private Camera camera;
		private string symbolChain;
		private BillboardBehaviour billBoard;
		
		
		public SymbolProjector (TargetableEntityBehaviour entity ,  Camera camera){
			this.entity = entity;
			this.camera = camera;
			this.symbolChain = entity.SymbolChain;
			//billBoard = new BillboardBehaviour(camera, entity);
		}
		
		public void AttachSymbols(){
			
		}
		
		void Update(){
			
    	}
		
		private void AttachSymbolchainToParent(){
			for (int i = 0; i < symbolChain.Length; i++) {
				var character = symbolChain[i];
				var gameObject = InterpreteSymbolChain(character, i);
				gameObject.transform.parent = billBoard.transform;
			}
		}
		
		private GameObject InterpreteSymbolChain(char symbol, int symbolPosition)
		{
			var position = Vector3.up*10 + Vector3.left*9;
			var orientation = Quaternion.identity;
			
			var name = string.Format("arrow{0}", symbol);
			var resource = Resources.Load("Media/Interface"+name);
			var gameObject = GameObject.Instantiate(resource, position, orientation) as GameObject;
			return gameObject;
			
		}
		
		
	}
}



