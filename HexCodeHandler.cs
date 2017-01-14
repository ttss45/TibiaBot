using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace TryingNewThings
{
    public class HexCodeHandler
    {
        #region DLL Import
        [DllImport("kernel32.dll")]
        public static extern Int32 ReadProcessMemory
            (
                IntPtr hProcess,
                IntPtr lpBaseAddress,
                [In, Out] byte[] buffer,
                UInt32 size,
                out IntPtr lpNumberOfBytesRead
            );
        #endregion

        #region Functions ReadBytes, ReadInt32 and ReadString
        public byte[] ReadBytes(IntPtr Handle, Int64 Address, uint BytesToRead)
        {
            IntPtr bytesRead;
            byte[] buffer = new byte[BytesToRead];
            ReadProcessMemory(Handle, new IntPtr(Address), buffer, BytesToRead, out bytesRead);
            return buffer;
        }
        public Int32 ReadInt32(Int64 Address, IntPtr Handle)
        {
            return BitConverter.ToInt32(ReadBytes(Handle, Address, 4), 0);
        }
        public string ReadString(long Address, IntPtr Handle, uint length = 32)
        {
            return ASCIIEncoding.Default.GetString(ReadBytes(Handle, Address, length)).Split('\0')[0];
        }
        #endregion

        private static Process _Tibia = Process.GetProcessesByName("Tibia").FirstOrDefault();
        public Process Tibia
        {
            get {return _Tibia; }
        }

        private IntPtr _Handle = _Tibia.Handle;
        public  IntPtr Handle
        {
            get { return _Handle; }
        }

        private UInt32 _Base = (UInt32)_Tibia.MainModule.BaseAddress.ToInt32();
        public  UInt32 Base
        {
            get { return _Base; }
        }

        #region Hex-Codes from Tibia
        private UInt32 _experiencePoints = 0x235F04; // Hex-Code for total Experience points
        public  UInt32 experiencePoints
        {
            get {return (UInt32)ReadInt32(_Base + _experiencePoints, _Handle); }
        }

        private UInt32 _currentLevel = 0x235F00; // Hex-Code for current Level
        public  UInt32 currentLevel
        {
            get { return (UInt32)ReadInt32(_Base + _currentLevel, _Handle); }
        }

        private UInt32 _currentMana = 0x235EF0; // Hex-Code for current Mana
        public  UInt32 currentMana
        {
            get { return (UInt32)ReadInt32(_Base + _currentMana, _Handle); }
        }

        private UInt32 _maxMana = 0x235EEC; // Hex-Code for Max Mana
        public  UInt32 maxMana
        {
            get { return (UInt32)ReadInt32(_Base + _maxMana, _Handle); }
        }

        private UInt32 _currentHp = 0x235F0C; // Hex-Code for current Health
        public  UInt32 currentHp
        {
            get { return (UInt32)ReadInt32(_Base + _currentHp, _Handle); }
        }

        private UInt32 _maxHP = 0x235F08; // Hex-code for Max HP
        public  UInt32 maxHP
        {
            get { return (UInt32)ReadInt32(_Base + _maxHP, _Handle); }
        }

        private UInt32 _currentMagicLevel = 0x235EFC; // Hex-code for current Magic Level
        public  UInt32 currentMagicLevel
        {
            get { return (UInt32)ReadInt32(_Base + _currentMagicLevel, _Handle); }
        }

        #region NotSureWhatThisIs
        //public UInt32 _firstIndexInBag = 0x242C80; //First index of the bag
        //public UInt32 _secondIndexInBag = 0x242C8C; //Second index of the bag
        //Not sure what this do, but it reads from the text field and 'exura' seems to have the code 1920301157
        //static UInt32 What = 0x3D6928;
        #endregion
        #endregion
    }
}
