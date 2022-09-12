using deathrun;
using Sandbox;

namespace MinimalExample
{
	partial class MinimalPlayer : Player
	{
		[Net]
		public bool IsDeath { get; set; }

		public override void Respawn()
		{
			SetModel( "models/citizen/citizen.vmdl" );

			//
			// Use WalkController for movement (you can make your own PlayerController for 100% control)
			//
			Controller = new RunnerPlayerController();

			//
			// Use StandardPlayerAnimator  (you can make your own PlayerAnimator for 100% control)
			//
			Animator = new StandardPlayerAnimator();

			//
			// Use ThirdPersonCamera (you can make your own Camera for 100% control)
			//
			CameraMode = new FirstPersonCamera();

			Inventory = new BaseInventory( this );

			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

			base.Respawn();
		}

		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public override void Simulate( Client cl )
		{
			base.Simulate( cl );
			TickPlayerUse();

			//
			// If you have active children (like a weapon etc) you should call this to 
			// simulate those too.
			//
			SimulateActiveChild( cl, ActiveChild );

			if ( IsServer && Input.Pressed( InputButton.Slot0 ) )
			{
				ViewModeToogle( cl );
			}
		}

		public static void ViewModeToogle( Client cl )
		{
			var player = cl.Pawn as Player;

			if ( player != null )
			{
				if ( player.CameraMode is not FirstPersonCamera )
				{
					player.CameraMode = new FirstPersonCamera();
				}
				else
				{
					player.CameraMode = new ThirdPersonCamera();
				}

			}
		}

		public override void OnKilled()
		{
			base.OnKilled();

			EnableDrawing = false;
		}
	}
}
