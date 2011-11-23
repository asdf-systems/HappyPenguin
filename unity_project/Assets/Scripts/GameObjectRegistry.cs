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
				objects[name] = gameObject;
				return;
			}
			
			objects.Add(name, gameObject);
		}
		
		public static GameObject GetObject(string name)
		{
			if (!objects.ContainsKey(name)) {
				EditorDebug.Log(string.Format("{0} not found", name));
			}
			return objects[name];
		}
	}
}

