
using Sandbox;

namespace deathrun
{

	[Library( "trigger_roundgoal" )]
	[Hammer.Solid]
	public partial class TriggerRoundGoal : BaseTrigger
	{

		/// <summary>
		/// Amount of time, in seconds, after the trigger_multiple has triggered before it can be triggered again. If set to -1, it will never trigger again (in which case you should just use a trigger_once). This affects OnTrigger output.
		/// </summary>
		[Property( "wait", Title = "Delay before reset" )]
		public float Wait { get; set; } = 1;

		TimeSince TimeSinceTriggered;

		public override void Spawn()
		{
			base.Spawn();

			EnableTouchPersists = true;

			if ( Wait <= 0 ) Wait = 0.2f;
		}

		/// <summary>
		/// Called every "Delay before reset" seconds as long as at least one entity that passes filters is touching this trigger
		/// </summary>
		protected Output OnTrigger { get; set; }

		public virtual void OnTriggered( Entity other )
		{
			OnTrigger.Fire( other );

		}

		public override void Touch( Entity other )
		{
			base.Touch( other );

			if ( TimeSinceTriggered < Wait ) return;
			if ( TouchingEntityCount < 1 ) return;

			TimeSinceTriggered = 0;
			OnTriggered( other );
		}



		public override void OnTouchStart( Entity toucher )
		{
			base.OnTouchStart( toucher );

			if( GameLogic.Instance.Round.CurrentState == Round.RoundState.Active)
			{
				Log.Info( "Funcionou!" );
			}
			
		}
	}
}
