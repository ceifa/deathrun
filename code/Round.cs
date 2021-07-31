using Sandbox;

namespace deathrun
{
	public partial class Round : NetworkComponent
	{
		[ConVar.Replicated( "deathrun_roundtime" )]
		public static float RoundTime { get; set; } = 320f;

		[Net]
		public RoundState CurrentState { get; set; }

		[Net]
		public float LastStateChange { get; set; }

		public void SetState(RoundState roundState)
		{
			CurrentState = roundState;
			LastStateChange = Time.Now;
		}
	}
}
