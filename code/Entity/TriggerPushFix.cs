
using Sandbox;

namespace deathrun
{

	[Library( "trigger_push_fix" )]
	[Hammer.Solid]
	public partial class TriggerPushFix : BaseTrigger
	{

		public override void StartTouch( Entity other )
		{
			base.Touch( other );

			if ( other.IsWorld )
				return;
			other.Velocity = Vector3.Backward * 1000;

		}
	}
}
