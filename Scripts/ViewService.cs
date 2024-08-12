using System.Collections.Generic;
using Godot;

namespace Service
{
    public enum ENTITY_VIEW {
        PLAYER,

        WEAPON_BOW,

        BULLET_ARROW,
    }

    public partial class ViewService: Node
    {
        [ExportGroup(name: "Characters")]
        [Export()] private PackedScene PlayerView;

        [ExportGroup(name: "Weapons")]
        [Export()] private PackedScene BowView;

        [ExportGroup(name: "Bullets")]
        [Export()] private PackedScene ArrowView;

        private static Dictionary<ENTITY_VIEW, PackedScene> _views;
        private static ViewService _instance;

        public override void _Ready()
        {
            ViewService._instance = this;

            ViewService._views = new Dictionary<ENTITY_VIEW, PackedScene> {
                { ENTITY_VIEW.PLAYER, PlayerView },
                { ENTITY_VIEW.WEAPON_BOW, BowView },
                { ENTITY_VIEW.BULLET_ARROW, ArrowView },
            };
        }

        public static PackedScene GetViewPacked(ENTITY_VIEW view) {
            return ViewService._views[view];
        }

        public static T CreateView<T>(ENTITY_VIEW type, Node parent = null) where T: Node {
            var packed = ViewService._views[type];
            var node = packed.Instantiate<T>();
            var p = parent != null ? parent : ViewService._instance;
            p.AddChild(node);
            return node;
        }
    }
}