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
		public int Flatness { get; set; } // 1000 für fischies
		public GameObject Target {
			get;
			set;
		}
		public TargetableEntityBehaviour entity;
		#endregion

		public ArcMovementController (TargetableEntityBehaviour start, GameObject target, float timeInSeconds, int flatness)
		{
			TimeSinceStart = TimeSpan.Zero;
			MovingStartPosition = start.transform.position;
			MovingEndPosition = target.transform.position;
			Flatness = flatness;
			MovingCenterPosition = GetCenter ();
			MovingTime = timeInSeconds;
			Distance = (MovingStartPosition - MovingEndPosition).magnitude;
			Target = target;
			IsMoving = true;
		}

		public override void Update (EntityBehaviour entity)
		{
			MovingObject = entity.gameObject.transform;
			
			if (IsMoving) {
				TimeSinceStart = TimeSinceStart.Add (TimeSpan.FromSeconds ((double)Time.deltaTime));
				if (TimeSinceStart.TotalSeconds >= MovingTime) {
					IsMoving = false;
					InvokeControllerFinished(entity);
					return;
				}
				Rotate();
				Move ();
			}
			
		}

		private Vector3 GetCenter ()
		{
			var center = ((MovingStartPosition + MovingEndPosition) * 0.5f);
			// move the center a bit downwards to make the arc vertical
			center += new Vector3 (0, -Flatness, 0);
			return center;
		}
		
		private void Rotate(){
			var start = MovingObject.rotation;
			var target = Target.transform.rotation;
			var lookAt = Quaternion.LookRotation(Target.transform.position - MovingObject.position);
			var speed = 0.001f;
			MovingObject.transform.rotation = Quaternion.Slerp(start, target*lookAt, Time.time * speed);
		}

		private void Move ()
		{
			
			Vector3 startRelCenter = MovingStartPosition - MovingCenterPosition;
			Vector3 endRelCenter = MovingEndPosition - MovingCenterPosition;
			var arc = Vector3.Slerp (startRelCenter, endRelCenter, (float)(TimeSinceStart.TotalSeconds / MovingTime));
//			arc = Quaternion.LookRotation(Vector3.down.normalized)*arc;
			MovingObject.transform.position = arc;
			MovingObject.transform.position += MovingCenterPosition;

		}
	}
}
