using Sandbox.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deathrun
{
	public partial class DeathrunKillFeed : KillFeed
	{
		public DeathrunKillFeed():base()
		{

		}
		String[] killtypes = new[]
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

		public override Panel AddEntry( ulong lsteamid, string left, ulong rsteamid, string right, string method )
		{
			return base.AddEntry( lsteamid, killtypes[new Random().Next( killtypes.Length )], rsteamid, right, method );
		}
	}
}

// killtypes[new Random().Next( killtypes.Length )]
