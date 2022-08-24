using Microsoft.Win32;

string version = "1.0.0";
Console.Title = $"Context Tweak (ver. {version})";

Console.ForegroundColor = ConsoleColor.Magenta;

Console.WriteLine($"Context Tweak ({version})\nhttps://github.com/maedakatoo");
Console.Write("1 - Add extension\n2 - Remove extension\n> ");
string choice = Console.ReadLine();
Console.Clear();

switch (choice)
{
    case "1":
        //Console.WriteLine("Добавление элемента в контестное меню\n");
        Add();
        break;

    case "2":
        //Console.WriteLine("Удаление элемента из контекстного меню\n");
        Remove();
        break;
    default:
        //return;
        break;
}


void Add()
{
    Console.Write("Extension name (with dot, for example: .psd) > ");
    string File = Console.ReadLine();

    RegistryKey myKey = Registry.CurrentUser;


    RegistryKey AddKey = myKey.OpenSubKey($"Software\\Classes\\{File}", true);

    try
    {
        RegistryKey newKey = AddKey.CreateSubKey("ShellNew");
        newKey.SetValue("Filename", "");

        Console.WriteLine($"Done!", newKey.Name);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
    finally
    {
        myKey.Close();
    }
}

void Remove()
{
    Console.Write("Extension name (with dot, for example: .psd) > ");
    string File = Console.ReadLine();

    RegistryKey myKey = Registry.CurrentUser;

    RegistryKey RemoveKey = myKey.OpenSubKey($"Software\\Classes\\{File}", true);

    try
    {
        RegistryKey newKey = RemoveKey.CreateSubKey("ShellNew");
        newKey.DeleteValue("Filename");

        Console.WriteLine($"Deleted {File} from \'{RemoveKey}\'", newKey.Name);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
    finally
    {
        myKey.Close();
    }
}
