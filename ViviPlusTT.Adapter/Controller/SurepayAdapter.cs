using System;
using System.Collections;
using System.Collections.Generic;

namespace ViviPlusTT.Adapter.Controller
{
    public class SurepayAdapter : Interface.IResponseAdapter
    {
        public Type RecordType()
        {
            return typeof(Format.Model.SurepayDetail);
        }

        public IEnumerable MappingRecords(object[] data)
        {
            var tt = data;

            return (IList<Format.Model.SurepayDetail>) data;            
        }
    }
}
