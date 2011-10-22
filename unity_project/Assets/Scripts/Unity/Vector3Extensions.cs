using System;
using UnityEngine;

namespace HappyPenguin.Unity
{
	public static class Vector3Extensions
	{
		public static bool IsCloseEnoughTo(this Vector3 first, Vector3 second, bool isYIgnored = false)
		{
			if (isYIgnored) {
				return (new Vector2(first.x, first.z) - new Vector2(second.x, second.z)).sqrMagnitude < 0.1f;
			}
			return (first - second).sqrMagnitude < 0.5;
		}
	}
}

