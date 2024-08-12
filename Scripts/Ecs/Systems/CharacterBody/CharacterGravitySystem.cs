using Component;
using DefaultEcs;
using DefaultEcs.System;
using Godot;

namespace System
{
    [With(typeof(VelocityComponent))]
    [Without(typeof(OnFloorMarker))]
    public sealed class CharacterGravitySystem : AEntitySetSystem<float>
    {
        private Vector3 velocity = Vector3.Zero;

        public CharacterGravitySystem(World world) : base(world, false) {
        }

        protected override void Update(float state, in Entity entity)
        {
            ref var vel = ref entity.Get<VelocityComponent>();

            vel.Y -= 50 * state;
        }
    }
}