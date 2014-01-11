﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaMain
{
    public abstract class DataItem
    {
        public DataItemsFactory Factory { get; private set; }
        public int Id { get; set; }
        public DataItem(DataItemsFactory factory)
        {
            Factory = factory;
        }
    }
}
