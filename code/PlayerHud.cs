using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;
using System.Globalization;
using deathrun.Round;
using MinimalExample;

namespace deathrun
{
	struct PlayerHudPart
	{
		public Label Label { get; }

		public Panel Progress { get; }

		public Func<Entity, float> Getter { get; }

		public float MaxValue { get; }

		public Func<float, string> Formatter { get; }

		public PlayerHudPart( Label label, Panel progress, Func<Entity, float> getter, float maxValue, Func<float, string> formatter )
		{
			Label = label;
			Progress = progress;
			Getter = getter;
			MaxValue = maxValue;
			Formatter = formatter ?? (f => $"{f:n0}");
		}
	}

	public class PlayerHud : Panel
	{
		private readonly List<PlayerHudPart> _hudParts;

		private readonly Label RoundStateText;
		private readonly Label RoundTimeText;
		private readonly Panel TeamGroup;
		private readonly Label TeamText;

		public PlayerHud()
		{
			_hudParts = new List<PlayerHudPart>( 3 );

			TeamGroup = Add.Panel( "team" );
			TeamText = TeamGroup.Add.Label( "", "text" );

			var roundState = Add.Panel( "round-state" );
			RoundStateText = roundState.Add.Label( "", "text" );
			RoundTimeText = roundState.Add.Label( "", "text" );

			AddHudInfo( "hp", p => p.Health, 100f );
			AddHudInfo( "vl", p => (float)Math.Sqrt( Math.Pow( p.Velocity.x, 2 ) + Math.Pow( p.Velocity.y, 2 ) ), 1000 );
			AddHudInfo( "tm", p =>
			{
				if (GameLogic.Instance.Round.CurrentState == RoundState.Active)
				{
					return Time.Now - GameLogic.Instance.Round.LastStateChange;
				}

				return 0;
			}, ActiveState.RoundTime, f => TimeSpan.FromSeconds( f ).ToString(@"%m\:%s\.%f"));
		}

		public override void Tick()
		{
			if ( !(Local.Pawn is MinimalPlayer player) ) return;

			var teamname = player.IsDeath ? "death" : "runner";
			TeamGroup.Classes = "team " + teamname;
			TeamText.Text = teamname;

			RoundStateText.Text = GameLogic.Instance.Round?.CurrentState switch
			{
				RoundState.WaitingPlayers => "Waiting for players",
				RoundState.Preparation => "Preparing",
				RoundState.Active => "Time remaining",
				RoundState.Over => "Round over",
				_ => RoundStateText.Text
			};

			if ( GameLogic.Instance.Round?.CurrentState == RoundState.Active )
			{
				RoundTimeText.Text = $"{ActiveState.RoundTime - (Time.Now - GameLogic.Instance.Round.LastStateChange):n0}";
			}
			else
			{
				RoundTimeText.Text = "";
			}

			_hudParts.ForEach( part =>
			{
				var currentValue = part.Getter( player );

				part.Label.Text = part.Formatter(currentValue);
				part.Progress.Style.Width = Length.Fraction( Math.Min(currentValue, part.MaxValue) / part.MaxValue );

				part.Progress.Style.Dirty();
			} );
		}

		private void AddHudInfo( string name, Func<Entity, float> getter, float maxValue, Func<float, string> formatter = null )
		{
			var group = Add.Panel( "hud-group " + name );

			var left = group.Add.Panel( "left" );
			left.Add.Label( name, "group-name" );

			var right = group.Add.Panel( "right" );
			var progress = right.Add.Panel( "right-progress" );
			var label = right.Add.Label( "", "text" );

			_hudParts.Add( new PlayerHudPart(label, progress, getter, maxValue, formatter) );
		}
	}
}
