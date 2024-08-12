using Godot;

namespace Core
{
    public static class Logger
    {
        public static void Error(params object[] messages)
        {
            GD.PrintErr(messages);
        }

        public static void Print(params object[] messages)
        {
            GD.Print(messages);
        }
    }
}
