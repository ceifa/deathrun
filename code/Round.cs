using MinimalExample;
using Sandbox;
using System;
using System.Linq;

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
			if (CurrentState == RoundState.Preparation)
			{
				
				var players = Entity.All.OfType<MinimalPlayer>().OrderBy( x => Guid.NewGuid() ); 
				var player = players.First();
				player.IsDeath = true;

				GameLogic.Instance.MoveToDeath( player );
				var runners = players.Skip ( 1 );

				foreach ( var item in runners )
				{
					GameLogic.Instance.MoveToRunner( item );
				}
			}
		}
	}
}
