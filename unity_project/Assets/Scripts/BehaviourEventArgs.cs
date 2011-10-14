using System;
using UnityEngine;

namespace HappyPenguin
{
	public class BehaviourEventArgs<T> : EventArgs where T : MonoBehaviour
	{
		public BehaviourEventArgs(T behaviour) {
			Behaviour = behaviour;
		}
		
		public T Behaviour {
			get;
			private set;
		}
	}
}

