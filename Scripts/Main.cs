using Godot;
using Core;
using System;
using DefaultEcs;
using DefaultEcs.System;
using Service;

public partial class Main : Node
{
	public static World EcsWorld { get; private set; }
	private SequentialSystem<float> _updatedSystems;
	private SequentialSystem<float> _physicsUpdatedSystems;

	public override void _EnterTree()
	{
		Main.EcsWorld = new World();

		this._updatedSystems = new SequentialSystem<float>(
			new PlayerInitSystem(Main.EcsWorld),
			new PlayerControllSystem(Main.EcsWorld),

			new CharacterGravitySystem(Main.EcsWorld),
			new CharacterJumpSystem(Main.EcsWorld),

			new WeaponActivateSystem(Main.EcsWorld),
			new WeaponActiveSystem(Main.EcsWorld),
			new WeaponDeActivateSystem(Main.EcsWorld),

			new BulletInitSystem(Main.EcsWorld)
		);

		this._physicsUpdatedSystems = new SequentialSystem<float>(
			new CharacterMovementSystem(Main.EcsWorld),
			new CharacterVelocitySystem(Main.EcsWorld),
			new CB_PositionUpdateSystem(Main.EcsWorld),
			new RB_PositionUpdateSystem(Main.EcsWorld),
			new BulletUpdateSystem(Main.EcsWorld)
		);
	}

	public override void _Ready()
	{
		Configs.ConfigManager.Init();
		var settingsCO = Configs.SettingsCO.GetSettingsCO();
		if (settingsCO == null)
		{
			Logger.Error("Settings config not loaded");
			return;
		}
		Logger.Print(settingsCO);
		BaseService.Init();
		new PlayerEntity().Create(new Vector3(0, 5, 0));
	}

	public override void _Process(double delta)
	{
		var dt = (float)delta;
		this._updatedSystems.Update(dt);
		BaseService.Update(dt);
	}

	public override void _PhysicsProcess(double delta)
	{
		this._physicsUpdatedSystems.Update((float)delta);
	}

	public override void _Input(InputEvent e)
	{
		InputService.InputEvent(e);
	}

	public override void _ExitTree()
	{
		this._updatedSystems.Dispose();
		this._physicsUpdatedSystems.Dispose();
		Main.EcsWorld.Dispose();
	}
}
