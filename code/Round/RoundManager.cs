using System.Linq;
using Sandbox;

namespace deathrun.Round
{
	public partial class RoundManager : NetworkComponent
	{
		private readonly IRoundState[] _roundStates;
		private IRoundState _currentRoundState;

		public RoundManager()
		{
			_roundStates = new IRoundState[]
			{
				new WaitingPlayersState(),
				new PreparationState(),
				new ActiveState(),
				new OverState()
			};

			_currentRoundState = _roundStates[0];
		}

		[Net]
		public RoundState CurrentState { get; set; }

		[Net]
		public float LastStateChange { get; set; }

		public void Switch(RoundState roundState)
		{
			CurrentState = roundState;
			LastStateChange = Time.Now;

			_currentRoundState?.OnExit();

			_currentRoundState = _roundStates.First( r => r.RoundState == roundState );
			_currentRoundState.OnEnter();
		}

		public void Think()
		{
			_currentRoundState.OnThink();
		}
	}
}
