using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pux.Resources
{
	public static class ResourceManager
	{
		private static readonly Dictionary<string, UnityEngine.Object> _resources;
		
		static ResourceManager() {
			_resources = new Dictionary<string, UnityEngine.Object>();
		}
		
		public static bool IsResourceLoaded(string key)
		{
			return _resources.ContainsKey(key);
		}
		
		public static bool UnloadResource(string key) {
			if (_resources.ContainsKey(key)) {
				_resources.Remove(key);
				return true;
			}
			return false;
		}
		
		public static void LoadResource(string key)
		{
			if (IsResourceLoaded(key)) {
				var message = string.Format("Resource {0} already loaded, skipping.", key);
				Debug.Log(message);
				return;
			}
			var r = UnityEngine.Resources.Load(key);
			if (r == null) {
				Debug.Log("resouce not found");
			}
			_resources.Add(key, r);
		}
		
		public static T GetResource<T>(string key) where T : UnityEngine.Object
		{
			return (T) _resources[key];
		}
		
		public static T CreateInstance<T>(string key) where T : UnityEngine.Object { 
			if (!IsResourceLoaded(key)) {
				Debug.Log(key + "not found");
			}
			var res = _resources[key];
			return (T) GameObject.Instantiate(res, Vector3.zero, Quaternion.identity);
		}
	}
}

