using View;

namespace Component;

public struct PlayerViewComponent {
    public PlayerView View;

    public PlayerViewComponent(PlayerView view) {
        this.View = view;
    }
}