using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MiaMain;

namespace MiaAppInterface
{
    public static class PrepareApp
    {

        public static void Prepare(Application App)
        {
            if (ConnectionStringBuilder.SetConnectionString())
            {
                FactoriesVault.FillFactories();
                new DataItemsExplorer().Show();
            }
            else
                App.Shutdown();
        }
    }
}
