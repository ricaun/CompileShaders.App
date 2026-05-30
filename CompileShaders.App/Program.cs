
using System;
using System.Diagnostics;

namespace CompileShaders.App;

public class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        new CanvaView().ShowDialog();
    }
}
