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
    class Program
    {
        static void Main(string[] args)
        {
            Client TibiaClient = new Client();
            while (true)
            {
                #region DebugIds
                TibiaClient.testIfIdsAreValid();
                #endregion
                TibiaClient.useManaToHealWhenLowHp(50, 25);
                TibiaClient.useManaWasteSpell(80, 0, 20);
                TibiaClient.useHealthPotionWhenLowHp(35);
                TibiaClient.useManaPotionWhenLowMp(10);

                //Used to not let the program eat all cpu power.
                Thread.Sleep(100);
            }
        }
    }
}