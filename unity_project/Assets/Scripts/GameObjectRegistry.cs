using System;
using UnityEngine;
using System.Collections.Generic;

namespace Pux
{
	public static class GameObjectRegistry
	{
		private static readonly Dictionary<string, GameObject> objects;
		static GameObjectRegistry() {
			objects = new Dictionary<string, GameObject>();
		}
		
		public static void RegisterObject(string name, GameObject gameObject)
		{
			if (objects.ContainsKey(name)) {
				var message = string.Format("object {0} already registered", name);
				throw new ApplicationException(message);
			}
			
			objects.Add(name, gameObject);
		}
		
		public static GameObject GetObject(string name)
		{
			if (!objects.ContainsKey(name)) {
				Debug.Log(string.Format("{0} not found", name));
			}
			return objects[name];
		}
	}
}

