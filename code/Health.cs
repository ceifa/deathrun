using System;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace deathrun
{
	public class Health : Panel
	{
		public Label Label;
		public Panel Progress;

		public Health()
		{
			var teamGroup = Add.Panel( "team" );
			teamGroup.Add.Label( "Runners", "text" );

			var roundState = Add.Panel( "round-state" );
			roundState.Add.Label( "Waiting for players", "text" );
			roundState.Add.Label( "00:00", "text" );

			CreateGroup( "hp", "100", "hp" );
			(Label, Progress) = CreateGroup( "vl", "250", "vl" );
			CreateGroup( "tm", "00:00:00", "tm" );
		}

		public override void Tick()
		{
			var player = Local.Pawn;
			if ( player == null ) return;

			var length = (float)Math.Sqrt( Math.Pow( player.Velocity.x, 2 ) + Math.Pow( player.Velocity.y, 2 ) );

			Label.Text = $"{length:n0}";
			Progress.Style.Width = Length.Fraction( length / 1000f );

			Progress.Style.Dirty();
		}

		private (Label, Panel) CreateGroup( string name, string value, string classname )
		{
			var group = Add.Panel( "hud-group " + classname );

			var left = group.Add.Panel( "left" );
			left.Add.Label( name, "group-name" );

			var right = group.Add.Panel( "right" );
			var progress = right.Add.Panel( "right-progress" );
			var label = right.Add.Label( value, "text" );

			return (label, progress);
		}
	}
}
