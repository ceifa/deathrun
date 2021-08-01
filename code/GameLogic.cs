
using deathrun;
using Sandbox;
using System;
using System.Linq;
using System.Threading.Tasks;
using deathrun.Round;
using MinimalExample;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace deathrun
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
		public RoundManager Round { get; set; }

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

			Round = new RoundManager();

			if ( IsClient )
			{
				Log.Info( "My Gamemode Has Created Clientside!" );
			}
			else
			{
				Round.Switch( RoundState.WaitingPlayers );
			}

			_ = Tick();
		}

		/// <summary>
		/// A client has joined the server. Make them a pawn to play with
		/// </summary>
		/// 

		public virtual void MoveToDeath( Entity pawn )
		{
			var spawnpoint = Entity.All
									.OfType<SpawnPointTerrorist>()               // get all SpawnPoint entities
									.OrderBy( x => Guid.NewGuid() )     // order them by random
									.FirstOrDefault();                  // take the first one

			if ( spawnpoint == null )
			{
				Log.Warning( $"Couldn't find spawnpoint for {pawn}!" );
				return;
			}

			if ( pawn != null )
			{
				pawn.Transform = spawnpoint.Transform;
			}
			else
			{
				Log.Info( spawnpoint.Transform.Position );
			}

		}

		public virtual void MoveToRunner( Entity pawn )
		{
			var spawnpoint = Entity.All
									.OfType<SpawnPointCounterTerrorist>()               // get all SpawnPoint entities
									.OrderBy( x => Guid.NewGuid() )     // order them by random
									.FirstOrDefault();                  // take the first one

			if ( spawnpoint == null )
			{
				Log.Warning( $"Couldn't find spawnpoint for {pawn}!" );
				return;
			}

			if ( pawn != null )
			{
				pawn.Transform = spawnpoint.Transform;
			} else
			{
				Log.Info( spawnpoint.Transform.Position );
			}
		}

		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			var player = new MinimalPlayer();
			client.Pawn = player;


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

				if ( Host.IsServer )
				{
					Round.Think();
				}
			}
		}
	}
}
