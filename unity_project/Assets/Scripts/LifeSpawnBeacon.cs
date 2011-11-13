using System;
using System.Linq;
using UnityEngine;

namespace Pux
{
	public sealed class LifeSpawnBeacon
	{
		private readonly GameObject _object;

		public LifeSpawnBeacon(GameObject @object) {
			_object = @object;
		}

		public bool IsOccupied {
			get { return !(_object.transform.GetChildCount() == 0); }
		}

		public GameObject UnityGameObject {
			get { return _object; }
		}

		public GameObject Balloon {
			get { return _object.transform.GetChild(0).gameObject; }
		}
		
	}
}

