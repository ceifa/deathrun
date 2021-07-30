using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;

namespace deathrun
{
	public class RunnerPlayerController : WalkController
	{
		public RunnerPlayerController()
		{
			SprintSpeed = WalkSpeed = DefaultSpeed = 250f;
			GroundFriction = 8f;
			AutoJump = true;
			AirAcceleration = 1000f;
		}
	}
}
