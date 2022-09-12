using Sandbox;
using Sandbox.UI;
using System;

namespace deathrun
{
	public partial class DeathrunKillFeed : Panel
	{
		private static DeathrunKillFeed Current;

		public DeathrunKillFeed() : base()
		{
			Current = this;
			StyleSheet.Load( "KillFeed.scss" );
		}

		private static readonly string[] killtypes = new[]
		{
			"Covid19",
			"Natural causes",
			"Inappropriate yelling",
			"Vehicular homicide",
			"Bio-engineered assault turtles with acid breath",
			"Dark and mysterious forces beyond our control",
			"Joe Biden",
			"The cool, refreshing taste of Pepsi®",
			"The Patriarchy",
			"The rains down in Africa",
			"The horses",
			"A saxophone solo"
		};

		public virtual Panel AddEntry( long lsteamid, string left, long rsteamid, string right, string method )
		{
			var e = Current.AddChild<KillFeedEntry>();

			e.Left.Text = left;
			e.Left.SetClass( "me", lsteamid == (Local.Client?.PlayerId) );

			e.Method.Text = method;

			e.Right.Text = right;
			e.Right.SetClass( "me", rsteamid == (Local.Client?.PlayerId) );

			return e;
		}

		public static void Add( long lsteamid, long rsteamid, string right, string method )
		{
			Current.AddEntry( lsteamid, killtypes[new Random().Next( killtypes.Length )], rsteamid, right, method );
		}
	}
}
