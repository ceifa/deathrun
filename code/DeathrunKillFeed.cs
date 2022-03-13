using Sandbox.UI;
using System;

namespace deathrun
{
	public partial class DeathrunKillFeed : KillFeed
	{
		public DeathrunKillFeed():base()
		{

		}

		private readonly string[] killtypes = new[]
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

		public override Panel AddEntry( long lsteamid, string left, long rsteamid, string right, string method )
		{
			return base.AddEntry( lsteamid, killtypes[new Random().Next( killtypes.Length )], rsteamid, right, method );
		}
	}
}
