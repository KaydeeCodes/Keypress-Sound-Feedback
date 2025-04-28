using SharpHook;
using SharpHook.Reactive;
using System;
using System.Diagnostics;

namespace InputClicker;

internal static class Program
{
    public static void Main()
    {
        Console.WriteLine("Starting input listener. Press Ctrl+C to stop.");

        var hook = new SimpleReactiveGlobalHook();

        hook.KeyPressed.Subscribe(e => OnInput("Key pressed: " + e.Data));
        //hook.KeyReleased.Subscribe(e => OnInput("Key released: " + e.Data));
        hook.MousePressed.Subscribe(e => OnInput("Mouse button pressed: " + e.Data));
        //hook.MouseReleased.Subscribe(e => OnInput("Mouse button released: " + e.Data));
        hook.MouseWheel.Subscribe(e => OnInput("Mouse wheel: " + e.Data));
        // hook.MouseMoved.Subscribe(e => { }); // Skip if too spammy

        hook.Run(); // Blocking call that listens forever
    }

    private static void OnInput(string message)
    {
        Console.WriteLine(message);
        PlayClickSound();
    }

    private static void PlayClickSound()
    {
        try
        {
            string Soundbite = Path.Combine(AppContext.BaseDirectory, "click2.wav");
            Process.Start("aplay", Soundbite);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error playing sound: " + ex.Message);
        }
    }
}