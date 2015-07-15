
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Hid;

namespace PPPA
{
	class Program
	{
		public static int Main(string[] args)
		{
			ushort VendorID = 0x0507;
			ushort ProductID = 0x0011;
            Console.Title = "ParaPara Paradise Awakener";
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("==============================================================================");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("                            ParalistParadise Tools                            ");
            Console.WriteLine("");
            Console.WriteLine("                          ParaPara Paradise Awakener                          ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("");
            Console.WriteLine("==============================================================================");
			Console.Beep();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("");
			Console.WriteLine("- - - - - - - - -  Detectando ParaPara Paradise Controllers  - - - - - - - - -");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("                               Usando VID ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("0x{0:x}\r\n", VendorID);
            Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("                               Usando PID ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("0x{0:x}\r\n", ProductID);
            Console.WriteLine("");

			
			List<Hid.Win32.Win32Device> lista_usbs = new List<Hid.Win32.Win32Device>();
			List<Hid.Win32.Win32Device> lista_sensores = new List<Hid.Win32.Win32Device>();
			
			foreach (string str in Hid.DeviceFactory.DevicePaths)
			{
				try
				{
					lista_usbs.Add(new Hid.Win32.Win32Device(str));
				}
				catch //(Exception exception)
				{
					//Console.Error.WriteLine("Warning: {0}", exception2.Message);
				}

			}
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("                          Detectados ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0}", lista_usbs.Count);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" dispositivos HID\r\n\r\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(" - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ");
            Console.WriteLine("");

			foreach (Hid.Win32.Win32Device usb in lista_usbs)
			{
				if (usb.VendorID == VendorID && usb.ProductID == ProductID)
				{
					lista_sensores.Add(usb);
				}
			}
			
			if (lista_sensores.Count < 1)
			{
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("          No se ha encontrado ningun ParaPara Paradise Controller             ");
                Console.WriteLine("");
				Console.ReadKey(false);
				return 1;
			}

			if (lista_sensores.Count > 1)
			{
                Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("              Detectados ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("{0}", lista_sensores.Count);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" ParaPara Paradise Controllers\r\n");
                Console.WriteLine("");
			}
			
			byte[] Report = new byte[2];
			Report[0] = 0x0;
            Report[1] = 0x1;
			
            bool activado;
			foreach (Hid.Win32.Win32Device sensor in lista_sensores)
			{
				activado = HidD_SetFeature(sensor.Handle, ref Report[0], 2);
				if (activado)
				{
                    Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("+ ParaPara Paradise Controller activado");
				}
				else
				{
                    Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("- Error al activar ParaPara Paradise Controller");
				}
			}
            Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("\r\nOperacion completada");
			Console.ReadKey(true);
			return 0;
			
		}
		[DllImport("hid.dll")]
		internal static extern bool HidD_SetFeature(IntPtr HidDeviceObject, ref byte lpReportBuffer, int ReportBufferLength);
	}
	
}