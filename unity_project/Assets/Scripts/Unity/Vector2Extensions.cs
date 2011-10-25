using System;
using UnityEngine;

namespace Pux.Unity2
{
	public static class Vector2Extensions
	{
		public static bool IsCloseEnoughTo(this Vector2 first, Vector2 second)
		{
			return (first - second).sqrMagnitude < 0.1;
		}
	}
}

