using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiaAppInterface
{
    public static class Controller
    {
        public static DataItemsController GetController(string Name)
        {
            switch (Name)
            {
                case ("Devices"):
                    return new DevicesController();
                case ("Companies"):
                    return new CompaniesController();
                default:
                    return null;
            }
        }
    }
}
