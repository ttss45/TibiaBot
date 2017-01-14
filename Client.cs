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
    class Client
    {
        #region DLL Import
        [DllImport("User32.dll")]
        public static extern int SetForegroundWindow(IntPtr point);
        #endregion

        HexCodeHandler hexCodeHandler = new HexCodeHandler();

        #region In-Game Functions
        Random rand = new Random();

        public int getManaInPercent()
        {
            double ManaInPercent = (double)hexCodeHandler.currentMana / (double)hexCodeHandler.maxMana;
            return (int)(ManaInPercent * 100);
        }
        public int getHealthInPercent()
        {
            double HpInPercent = (double)hexCodeHandler.currentHp / (double)hexCodeHandler.maxHP;
            return (int)(HpInPercent * 100);
        }
        public void useManaWasteSpell(int useManaWasteSpellAbovePercent, int minTimeBeforeUse, int maxTimeBeforeUse)
        {
            if (getManaInPercent() > useManaWasteSpellAbovePercent)
            {
                Thread.Sleep(1000 * (rand.Next(minTimeBeforeUse, maxTimeBeforeUse)));
                SetForegroundWindow(hexCodeHandler.Tibia.MainWindowHandle);
                SendKeys.SendWait("{F1}");
            }
        }
        public void useManaToHealWhenLowHp(int healBelowPercent, int manaCostOfHeal)
        {
            if (hexCodeHandler.currentHp >= 1)
            {
                if (getHealthInPercent() < healBelowPercent &&
                    hexCodeHandler.currentMana >= manaCostOfHeal)
                {
                    SetForegroundWindow(hexCodeHandler.Tibia.MainWindowHandle);
                    SendKeys.SendWait("{F1}");
                    Thread.Sleep(500);
                }
            }
        }
        public void useHealthPotionWhenLowHp(int useHealthPotionBelowPercent)
        {
            if (getHealthInPercent() <= useHealthPotionBelowPercent)
            {
                SetForegroundWindow(hexCodeHandler.Tibia.MainWindowHandle);
                SendKeys.SendWait("{F12}");
            }
            Thread.Sleep(500);
        }
        public void useManaPotionWhenLowMp(int useManaPotionBelowPercent)
        {
            if (getHealthInPercent() <= useManaPotionBelowPercent)
            {
                SetForegroundWindow(hexCodeHandler.Tibia.MainWindowHandle);
                SendKeys.SendWait("{F11}");
            }
            Thread.Sleep(500);
        }
        public void testIfIdsAreValid()
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Experience       : {0}", hexCodeHandler.experiencePoints);
            Console.WriteLine("Current Mana     : {0}", hexCodeHandler.currentMana);
            Console.WriteLine("Max Mana         : {0}", hexCodeHandler.maxMana);
            Console.WriteLine("Magic Level      : {0}", hexCodeHandler.currentMagicLevel);
            Console.WriteLine("Current HP       : {0}", hexCodeHandler.currentHp);
            Console.WriteLine("Max HP           : {0}", hexCodeHandler.maxHP);
            Console.WriteLine("Current Level    : {0}", hexCodeHandler.currentLevel);
            Console.WriteLine("HP in Percent    : {0}%", getHealthInPercent());
            Console.WriteLine("MP in Percent    : {0}%", getManaInPercent());

            Thread.Sleep(1000);
        }
        #endregion
    }
}
