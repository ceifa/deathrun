using System.Linq;
using System.Threading.Tasks;
using MinimalExample;
using Sandbox;

namespace deathrun.Round
{
	public class ActiveState : IRoundState
	{
		[ConVar.Replicated( "deathrun_roundtime" )]
		public static float RoundTime { get; set; } = 320f;

		public RoundState RoundState => RoundState.Active;

		public void OnEnter()
		{
		}

		public void OnExit()
		{
		}

		public void OnThink()
		{
			var players = Entity.All.OfType<MinimalPlayer>().ToArray();
			if ( players.Length < 2 )
			{
				GameLogic.Instance.Round.Switch( RoundState.WaitingPlayers );
				return;
			}

			if ( Time.Now - GameLogic.Instance.Round.LastStateChange > RoundTime )
			{
				GameLogic.Instance.Round.Switch( RoundState.Over );
				return;
			}

			var alive = players.Where( p => p.LifeState == LifeState.Alive ).ToArray();
			if ( alive.All( p => p.IsDeath ) || alive.All( p => !p.IsDeath ) )
			{
				Log.Info( string.Concat(alive.Select( p => p.IsDeath )) );
				GameLogic.Instance.Round.Switch( RoundState.Over );
				return;
			}
		}
	}
}
