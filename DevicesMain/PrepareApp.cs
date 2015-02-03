using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using InterfaceToClient;

namespace DevicesMain
{
    public static class PrepareApp
    {
        public static void Prepare(Application App)
        {
            if (ConnectionStringBuilder.SetConnectionString())
            {
                Vault.Init();
                new DataItemsExplorer().Show();
            }
            else
                App.Shutdown();
        }
    }
}
