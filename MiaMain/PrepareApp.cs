﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using InterfaceToClient;
using InterfaceToDataBase;

namespace MiaMain
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