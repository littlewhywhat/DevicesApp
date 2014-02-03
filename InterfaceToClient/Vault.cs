using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceToClient
{
    public static class Vault
    {
        private static DictionariesVault dicsVault = new DictionariesVault();
        public static DictionariesVault DicsVault { get { return dicsVault; } }
        public static DicChangesGetter ChangesGetter { get { return dicsVault.ChangesGetter; } }
        public static void Init()
        {
            DicsVault.StartWorking();
        }
    }
}
