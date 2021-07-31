
using deathrun;
using Sandbox;
using System.Linq;
using System.Threading.Tasks;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace MinimalExample
{

	/// <summary>
	/// This is your game class. This is an entity that is created serverside when
	/// the game starts, and is replicated to the client. 
	/// 
	/// You can use this to create things like HUDs and declare which player class
	/// to use for spawned players.
	/// 
	/// Your game needs to be registered (using [Library] here) with the same name 
	/// as your game addon. If it isn't then we won't be able to find it.
	/// </summary>
	[Library( "deathrun" )]
	public partial class GameLogic : Sandbox.Game
	{
		public static GameLogic Instance => Current as GameLogic;

		[Net]
		public Round Round { get; set; }

		public GameLogic()
		{
			if ( IsServer )
			{
				Log.Info( "My Gamemode Has Created Serverside!" );

				// Create a HUD entity. This entity is globally networked
				// and when it is created clientside it creates the actual
				// UI panels. You don't have to create your HUD via an entity,
				// this just feels like a nice neat way to do it.
				new MinimalHudEntity();
			}

			Round = new Round();

			if ( IsClient )
			{
				Log.Info( "My Gamemode Has Created Clientside!" );
			}

			Tick();
		}

		/// <summary>
		/// A client has joined the server. Make them a pawn to play with
		/// </summary>
		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			var player = new MinimalPlayer();
			client.Pawn = player;

			if ( All.OfType<Player>().Count() >= 2 && Round.CurrentState == RoundState.WaitingPlayers )
			{
				Round.SetState( RoundState.Preparation);
			}

			if ( Round.CurrentState < RoundState.Active )
			{
				player.Respawn();
			}
		}

		public async Task Tick()
		{
			while ( true )
			{
				await Task.NextPhysicsFrame();

				if ( Round.CurrentState == RoundState.Preparation )
				{
					if ( Time.Now - Round.LastStateChange > 3 )
					{
						Round.SetState( RoundState.Active );
					}
				}
				else if ( Round.CurrentState == RoundState.Active )
				{
					if ( Time.Now - Round.LastStateChange > Round.RoundTime )
					{
						Round.SetState( RoundState.Over );
					}
				}
				else if ( Round.CurrentState == RoundState.Over )
				{
					if ( Time.Now - Round.LastStateChange > 3 )
					{
						Round.SetState( RoundState.Preparation );
					}
				}
			}
		}
	}

}
