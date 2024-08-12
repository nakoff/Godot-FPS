using Component;
using DefaultEcs;
using DefaultEcs.System;

namespace System
{
    [With(typeof(MoveComponent))]
    [With(typeof(VelocityComponent))]
    public sealed class CharacterMovementSystem : AEntitySetSystem<float>
    {
        public CharacterMovementSystem(World world) : base(world, false) { }

        protected override void Update(float state, in Entity entity)
        {
            ref var move = ref entity.Get<MoveComponent>();
            ref var vel = ref entity.Get<VelocityComponent>();

            vel.X += move.DirX * move.Accel;
            vel.Z += move.DirZ * move.Accel;

            if (Math.Abs(vel.X) > move.Speed) vel.X = move.Speed * Math.Sign(vel.X);
            if (Math.Abs(vel.Z) > move.Speed) vel.Z = move.Speed * Math.Sign(vel.Z);
        }
    }
}