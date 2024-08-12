using Godot;

namespace Service;

public class CameraService: BaseService, IInitService, IUpdateService {
    private static Camera _camera;

    public float Offset {
        get => CameraService._camera.Offset;
        set => CameraService._camera.Offset = value;
    }

    public void CreateCamera(Node3D parent, Vector2 offset) {
        if (CameraService._camera != null) {
            CameraService._camera.Destroy();
        }
        CameraService._camera = new Camera(parent, offset);
    }

    public void OnInit() {
    }

    public void OnUpdate(float dt) {
        _camera?.Update(dt);
    }
}

class Camera {
    private Node3D _origin;
    private Node3D _pivot;
    private Camera3D _camera;

    public float Offset {
        get => _pivot.Position.Z;
        set => _pivot.Position = new Vector3(0, 0, value);
    }

    public Camera(Node3D parent, Vector2 offset) {
        _origin = new Node3D();
        parent.AddChild(_origin);
        _origin.GlobalPosition = parent.GlobalPosition;

        _pivot = new Node3D();
        _pivot.Position = new Vector3(0, offset.Y, offset.X);
        _origin.AddChild(_pivot);

        var node = new Node();
        _origin.AddChild(node);

        _camera = new Camera3D();
        node.AddChild(_camera);
        _camera.Position = _pivot.Position;

        var cross = _camera.GlobalPosition.Cross(_origin.GlobalPosition);
        if (cross != Vector3.Zero) {
            _camera.LookAt(_origin.GlobalPosition, Vector3.Up);
        }
    }

    public void Update(float dt) {
        var targetPos = _pivot.GlobalPosition;
        _camera.GlobalPosition = _camera.GlobalPosition.Lerp(targetPos, 0.06f);
    }

    public void Destroy() {
        _origin.QueueFree();
    }
}