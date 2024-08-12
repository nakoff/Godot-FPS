using Godot;

namespace Component
{
    public struct PositionComponent {
        private Vector3 _curPostition = Vector3.Zero;
        private Vector3 _prevPosition = Vector3.Zero;

        public Vector3 PrevPosition { get; } = Vector3.Zero;
        public Vector3 CurPosition {
            get => _curPostition;
            set {
                _prevPosition = _curPostition;
                _curPostition = value;
            }
        }

        public PositionComponent(Vector3 pos) {
            PrevPosition = pos;
            CurPosition = pos;
        }
    }
}