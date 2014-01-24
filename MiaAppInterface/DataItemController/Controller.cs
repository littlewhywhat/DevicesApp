﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiaMain;

namespace MiaAppInterface
{
    public static class Controller
    {
        public static DataItemsController GetController(string Name)
        {
            if (Name == TableNames.Devices)
                return new DevicesController();
            if (Name == TableNames.Companies)
                return new CompaniesController();
            return null;
            
        }
    }
}