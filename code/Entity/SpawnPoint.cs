using Sandbox;
using System.Collections.Generic;

namespace deathrun 
{
	/// <summary>
	/// This entity defines the sppawn point of the player in first person shooter gamemodes.
	/// </summary>

	[Library( "info_player_terrorist" )]
	[Hammer.EditorModel( "models/editor/playerstart.vmdl" )]
	[Hammer.EntityTool( "Player Terrorist / Death Spawnpoint", "Player", "Defines a point where the death player can (re)spawn" )]
	public class SpawnPointTerrorist : Entity
	{

	}

	[Library( "info_player_counterterrorist" )]
	[Hammer.EditorModel( "models/editor/playerstart.vmdl" )]
	[Hammer.EntityTool( "Player CounterTerrorist / Runner Spawnpoint", "Player", "Defines a point where the runner player can (re)spawn" )]
	public class SpawnPointCounterTerrorist : Entity
	{

	}
}
