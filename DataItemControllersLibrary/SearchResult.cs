using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataItemControllersLibrary
{
    public class SearchResult
    {
        public string Result;
        public DataItemController Reference;
        public SearchResult(string result, DataItemController reference)
        {
            Result = result;
            Reference = reference;
        }
    }
}
