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
