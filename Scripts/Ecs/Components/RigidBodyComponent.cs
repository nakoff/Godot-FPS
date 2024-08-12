using Godot;

namespace Component
{
    public struct RigidBodyComponent {
        public RigidBody3D Body;

        public RigidBodyComponent(RigidBody3D body) {
            this.Body = body;
        }
    }
}