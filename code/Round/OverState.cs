using System;
using System.Threading.Tasks;
using Sandbox;

namespace deathrun.Round
{
	public class OverState : IRoundState
	{
		public RoundState RoundState => RoundState.Over;

		public void OnEnter()
		{
		}

		public void OnExit()
		{
		}

		public void OnThink()
		{
			if ( Time.Now - GameLogic.Instance.Round.LastStateChange > 3 )
			{
				GameLogic.Instance.Round.Switch( RoundState.Preparation );
			}
		}
	}
}
