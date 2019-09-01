using System;
using System.Collections.Generic;
using System.Text;

namespace PetConsoleApp
{
    public interface IConsolePrinter
    {
        /// <summary>
        /// Prints to the user the menu and waits for user input
        /// </summary>
        void StartUI();
    }
}
