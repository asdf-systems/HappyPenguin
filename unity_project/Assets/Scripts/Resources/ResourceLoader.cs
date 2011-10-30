using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pux
{
	public static class ResourceLoader
	{
		private static readonly Dictionary<string, UnityEngine.Object> _resources;
		
		static ResourceLoader() {
			_resources = new Dictionary<string, UnityEngine.Object>();
		}
		
		public static bool IsResourceLoaded(string key)
		{
			return _resources.ContainsKey(key);
		}
		
		public static void LoadResource(string key)
		{
			var r = Resources.Load(key);
			_resources.Add(key, r);
		}
		
		public static UnityEngine.Object GetResource(string key)
		{
			return _resources[key];
		}
	}
}

