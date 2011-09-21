using System;
using UnityEngine;
using HappyPenguin.Unity;
using HappyPenguin.Entities;

namespace HappyPenguin.Controllers
{
	public class ArcMovementController : MovementController
	{

		#region Fields and Properties
		public Vector3 MovingStartPosition { get; set; }
		public Vector3 MovingEndPosition { get; set; }
		public Vector3 MovingCenterPosition { get; set; }
		public Transform MovingObject { get; set; }
		public float MovingTime { get; set; }
		public float Distance { get; set; }
		public TimeSpan TimeSinceStart { get; set; }
		public bool IsMoving { get; set; }
		#endregion

		public ArcMovementController (Vector3 start, Vector3 end, float timeInSeconds)
		{
			TimeSinceStart = TimeSpan.Zero;
			MovingStartPosition = start;
			MovingEndPosition = end;
			MovingCenterPosition = GetCenter ();
			MovingTime = timeInSeconds;
			Distance = (MovingStartPosition - MovingEndPosition).magnitude;
			IsMoving = true;
		}

		public override void Update (EntityBehaviour entity)
		{
			MovingObject = entity.gameObject.transform;
			
			if (IsMoving) {
				TimeSinceStart = TimeSinceStart.Add (TimeSpan.FromSeconds ((double)Time.deltaTime));
				if (TimeSinceStart.TotalSeconds >= MovingTime) {
					IsMoving = false;
					return;
				}
				Move ();
			}
			
		}

		private Vector3 GetCenter ()
		{
			var center = ((MovingStartPosition + MovingEndPosition) * 0.5f);
			// move the center a bit downwards to make the arc vertical
			center += new Vector3 (0, -1000, 0);
			return center;
		}


		private void Move ()
		{
			var forward = MovingObject.transform.forward;
			forward.Normalize ();
			var right = MovingObject.transform.right;
			right.Normalize ();
			MovingObject.transform.Rotate(right,0.1f);
			MovingObject.transform.Rotate(forward,0.1f);

			Vector3 startRelCenter = MovingStartPosition - MovingCenterPosition;
			Vector3 endRelCenter = MovingEndPosition - MovingCenterPosition;
			MovingObject.transform.position = Vector3.Slerp(startRelCenter, endRelCenter,(float)(TimeSinceStart.TotalSeconds/MovingTime));
			MovingObject.transform.position += MovingCenterPosition;
//			Debug.DrawLine(MovingStartPosition,MovingEndPosition);
//			Debug.DrawLine(MovingStartPosition,MovingCenterPosition);
//			Debug.DrawLine(MovingCenterPosition , MovingCenterPosition+new Vector3(0,-1,0));
//			Debug.DrawLine(Vector3.zero , new Vector3(0,100,0));
		}
	}
}
