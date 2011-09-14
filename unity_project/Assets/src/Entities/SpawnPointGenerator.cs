using System;
using UnityEngine;

namespace HappyPenguin.Entities
{
	public sealed class SpawnPointGenerator
	{
		public SpawnPointGenerator () {
		}
		
		public Vector3 CreateNext()
		{
			return new Vector3(0,0,0);
		}
	}
}

