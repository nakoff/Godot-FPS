using Godot;

namespace Component
{
    public struct CharacterBodyComponent {
        public CharacterBody3D Body;

        public CharacterBodyComponent(CharacterBody3D body) {
            this.Body = body;
        }
    }
}