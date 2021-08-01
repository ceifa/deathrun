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
			var players = Entity.All.OfType<MinimalPlayer>().OrderBy( x => Guid.NewGuid() ); 
			var player = players.First();
			player.IsDeath = true;
			player.Respawn();

			GameLogic.Instance.MoveToDeath( player );
			var runners = players.Skip ( 1 );

			foreach ( var item in runners )
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
