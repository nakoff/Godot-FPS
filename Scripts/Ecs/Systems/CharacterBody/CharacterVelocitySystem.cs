using Component;
using DefaultEcs;
using DefaultEcs.System;
using Godot;

namespace System
{
    [With(typeof(VelocityComponent))]
    [With(typeof(CharacterBodyComponent))]
    public sealed class CharacterVelocitySystem : AEntitySetSystem<float>
    {
        private int deAccel = 50;

        private Vector3 velocity = Vector3.Zero;

        public CharacterVelocitySystem(World world) : base(world, false) {
        }

        protected override void Update(float state, in Entity entity)
        {
            ref var vel = ref entity.Get<VelocityComponent>();
            ref var body = ref entity.Get<CharacterBodyComponent>().Body;

            this.velocity.X = vel.X;
            this.velocity.Y = vel.Y;
            this.velocity.Z = vel.Z;

            body.Velocity = this.velocity;
            body.MoveAndSlide();

            if (Math.Abs(vel.X) < 1) vel.X = 0;
            if (Math.Abs(vel.Z) < 1) vel.Z = 0;

            var signX = Math.Sign(vel.X);
            var signZ = Math.Sign(vel.Z);

            if (vel.X != 0) vel.X -= this.deAccel * signX * state;
            if (vel.Z != 0) vel.Z -= this.deAccel * signZ * state;

            if (body.IsOnFloor()) entity.Set<OnFloorMarker>();
            else entity.Remove<OnFloorMarker>();
        }
    }
}