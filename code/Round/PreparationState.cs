using System;
using System.Linq;
using MinimalExample;
using Sandbox;

namespace deathrun.Round
{
	public class PreparationState : IRoundState
	{
		public RoundState RoundState => RoundState.Preparation;

		public void OnEnter()
		{
			var players = Entity.All.OfType<MinimalPlayer>().OrderBy( x => Guid.NewGuid() ).ToArray();
			
			players[0].IsDeath = true;
			players[0].Respawn();

			GameLogic.Instance.MoveToDeath( players[0] );

			foreach ( var item in players[1..] )
			{
				item.IsDeath = false;
				item.Respawn();
				GameLogic.Instance.MoveToRunner( item );
			}
		}

		public void OnExit()
		{
		}

		public void OnThink()
		{
			if ( Time.Now - GameLogic.Instance.Round.LastStateChange > 3 )
			{
				GameLogic.Instance.Round.Switch( RoundState.Active );
			}
		}
	}
}
