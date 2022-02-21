using Microsoft.Win32;
using System.Security.Principal;

//Переменные
int TTLtoChange;

//Приветствие
Console.WriteLine("TTL Changer by MrHelenberg");
Console.WriteLine("Version 0.1");
Console.WriteLine("");
Console.WriteLine("Проверка реестра..");
Thread.Sleep(5000);

//Проверка на запуск администратора
WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
bool hasAdministrativeRight = pricipal.IsInRole(WindowsBuiltInRole.Administrator);

if (hasAdministrativeRight == false)
{
    Console.WriteLine("");
    Console.WriteLine("Алё, Сашок, запусти приложение с правами администратора.");
    Console.ReadKey();
    Environment.Exit(0);
}

//Проверка TTL
RegistryKey localMachineKey = Registry.LocalMachine;
RegistryKey SystemKey = localMachineKey.OpenSubKey("System", true);
RegistryKey CurrentControlSetKey = SystemKey.OpenSubKey("CurrentControlSet", true);
RegistryKey ServicesKey = CurrentControlSetKey.OpenSubKey("Services", true);
RegistryKey TcpipKey = ServicesKey.OpenSubKey("Tcpip", true);
RegistryKey ParametersKey = TcpipKey.OpenSubKey("Parameters", true);

if (ParametersKey.GetValue("DefaultTTL") == null)
{
    Console.WriteLine("Файла реестра не найден, ваш TTL - 128");
}
else
{
    Console.WriteLine("Текущий TTL - " + ParametersKey.GetValue("DefaultTTL").ToString());
}

//Процесс изменения реестра
Console.WriteLine("");
Console.WriteLine("Для изменения TTL введите число, на которое вы хотите изменить.");
Console.WriteLine("Для обхода ограничений операторами связи рекомендуется устанавливать значение 65");
Console.WriteLine("");
TTLtoChange = Convert.ToInt32(Console.ReadLine());
ParametersKey.SetValue("DefaultTTL", TTLtoChange);
Console.WriteLine("");
Console.WriteLine("Успешно!");
Console.WriteLine("Чтобы применить изменения требуется выполнить перезапуск. Удачи!");
Console.ReadKey();
Environment.Exit(0);


