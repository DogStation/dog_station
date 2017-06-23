using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DogStation.Utils
{
    public class AccountUtil
    {
        [DllImport("DogStationWin32.dll", EntryPoint = "checkAccountInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CheckAccountInfo(string name, string pw);
    }
}
