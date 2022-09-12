using Sandbox;
using SandboxEditor;

namespace deathrun
{
	/// <summary>
	/// This entity defines the sppawn point of the player in first person shooter gamemodes.
	/// </summary>
	[HammerEntity]
	[Library( "info_player_terrorist" )]
	[EditorModel( "models/editor/playerstart.vmdl" )]
	[Title( "Death Spawnpoint" ), Category( "Player" ), Description( "Defines a point where the death player can (re)spawn" )]
	public class SpawnPointTerrorist : Entity
	{

	}

	[Library( "info_player_counterterrorist" )]
	[EditorModel( "models/editor/playerstart.vmdl" )]
	[Title( "Runner Spawnpoint" ), Category( "Player" ), Description( "Defines a point where the runner player can (re)spawn" )]
	public class SpawnPointCounterTerrorist : Entity
	{

	}
}
