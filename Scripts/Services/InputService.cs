using System.Collections.Generic;
using Godot;

namespace Service;

public class InputService: BaseService, IUpdateService, IPreUpdateService {
    private static List<InputEvent> _eventsQueue = new List<InputEvent>();
    private static Vector2 _mousePos = Vector2.Zero;
    private static Vector2 _mouseRelative = Vector2.Zero;
    private static Vector2 _lastMousePos = Vector2.Zero;

    public Vector2 MousePos => InputService._mousePos;
    public Vector2 MouseRelative => InputService._mouseRelative;

    public enum ACTION {
        LEFT, RIGHT, FORWARD, BACKWARD,
        FIRE, WEAPON_USE,
    }

    private static Dictionary<ACTION, string> _actions = new Dictionary<ACTION, string>{
        {ACTION.LEFT, "Left"},
        {ACTION.RIGHT, "Right"},
        {ACTION.FORWARD, "Forward"},
        {ACTION.BACKWARD, "Backward"},
        {ACTION.WEAPON_USE, "Use"},
        {ACTION.FIRE, "Fire"},
    };

    public bool MouseCapture {
        get => Input.MouseMode == Input.MouseModeEnum.Captured;
        set {
            if (value) {
                InputService._lastMousePos = MousePos;
                Input.MouseMode = Input.MouseModeEnum.Captured;
            } else {
                Input.MouseMode = Input.MouseModeEnum.Visible;
                Input.WarpMouse(InputService._lastMousePos);
            }
        }
    }

    public bool IsActionPressed(ACTION action) => Input.IsActionJustPressed(InputService._actions[action]);
    public bool IsActionReleased(ACTION action) => Input.IsActionJustReleased(InputService._actions[action]);

    public static void InputEvent(InputEvent e) {
        _eventsQueue.Add(e);
    }

    public void OnPreUpdate(float dt) {
        InputService._mouseRelative = Vector2.Zero;
    }

    public void OnUpdate(float dt) {
        foreach (var e in InputService._eventsQueue) {
            if (e is InputEventMouseMotion mouseMotion) {
                InputService._mousePos = mouseMotion.Position;
                InputService._mouseRelative = mouseMotion.Relative;
            }
        }
        InputService._eventsQueue.Clear();
    }
}