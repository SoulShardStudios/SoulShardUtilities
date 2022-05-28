using UnityEditor;
using System.Diagnostics;

namespace SoulShard.Editor
{
    public class OpenConsole
    {
        [MenuItem("Processes/Open Powershell")]
        public static void OpenPowershellWindow()
        {
            Process.Start("powershell.exe");
        }

        [MenuItem("Processes/Open CMD")]
        public static void OpenCommandPromptWindow()
        {
            Process.Start("CMD.exe");
        }
    }
}
