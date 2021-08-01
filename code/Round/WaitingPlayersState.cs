using MinimalExample;
using Sandbox;
using System.Linq;
using System.Threading.Tasks;

namespace deathrun.Round
{
	public class WaitingPlayersState : IRoundState
	{
		public RoundState RoundState => RoundState.WaitingPlayers;

		public void OnEnter()
		{
			foreach ( var player in Entity.All.OfType<MinimalPlayer>() )
			{
				player.IsDeath = false;
				player.Respawn();
			}
		}

		public void OnExit()
		{
		}

		public void OnThink()
		{
			if ( Entity.All.OfType<MinimalPlayer>().Count() >= 2 )
			{
				GameLogic.Instance.Round.Switch( RoundState.Preparation );
			}
		}
	}
}
