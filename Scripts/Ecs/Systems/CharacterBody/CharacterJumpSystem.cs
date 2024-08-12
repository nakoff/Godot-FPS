using Component;
using DefaultEcs;
using DefaultEcs.System;
using Godot;

namespace System
{
    [With(typeof(JumpComponent))]
    [With(typeof(VelocityComponent))]
    public sealed class CharacterJumpSystem : AEntitySetSystem<float>
    {
        public CharacterJumpSystem(World world) : base(world, false) {
        }

        protected override void Update(float state, in Entity entity)
        {
            ref var jump = ref entity.Get<JumpComponent>();
            ref var vel = ref entity.Get<VelocityComponent>();

            vel.Y = jump.Force;

            GD.Print(">> JUMP");
            entity.Remove<JumpComponent>();
        }
    }
}