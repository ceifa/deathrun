using System.Threading.Tasks;

namespace deathrun.Round
{
	public interface IRoundState
	{
		RoundState RoundState { get; }

		void OnEnter();

		void OnExit();

		void OnThink();
	}
}
