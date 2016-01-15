using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleSearch.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}
