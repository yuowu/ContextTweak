using Microsoft.Win32;
using System;
using System.Threading;

class Program
{
    static void Main()
    {
        string version = "1.1.0";
        Console.Title = $"Context Tweak // {version}";
        Console.ForegroundColor = ConsoleColor.Magenta;
        Menu();
    }

    static void Menu()
    {
        Console.Clear();
        Console.Write("1 - Add extension\n2 - Remove extension\n> ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Add();
                break;

            case "2":
                Remove();
                break;

            default:
                Menu();
                break;
        }
    }

    static async void Add()
    {
        Console.Write("Extension name (with dot, for example: .psd) > ");
        string extension = Console.ReadLine();

        RegistryKey myKey = Registry.CurrentUser;
        RegistryKey addKey = myKey.OpenSubKey($"Software\\Classes\\{extension}", true);

        try
        {
            if (addKey == null)
            {
                addKey = myKey.CreateSubKey($"Software\\Classes\\{extension}");
            }

            RegistryKey newKey = addKey.CreateSubKey("ShellNew");
            newKey.SetValue("Filename", "");

            Console.WriteLine($"Done! Extension {extension} added to {newKey.Name}.");
            Thread.Sleep(3000);
            Menu();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Thread.Sleep(3000);
            Menu();
        }
        finally
        {
            myKey.Close();
            Menu();
        }
    }

    static void Remove()
    {
        Console.Write("Extension name (with dot, for example: .psd) > ");
        string extension = Console.ReadLine();

        RegistryKey myKey = Registry.CurrentUser;
        RegistryKey removeKey = myKey.OpenSubKey($"Software\\Classes\\{extension}", true);

        try
        {
            if (removeKey != null)
            {
                removeKey.DeleteSubKeyTree("ShellNew");

                Console.WriteLine($"Extension {extension} removed from {removeKey}.");
                Thread.Sleep(3000);
                Menu();
            }
            else
            {
                Console.WriteLine($"Extension {extension} not found.");
                Thread.Sleep(3000);
                Menu();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Thread.Sleep(3000);
            Menu();
        }
        finally
        {
            myKey.Close();
            Menu();
        }
    }
}
