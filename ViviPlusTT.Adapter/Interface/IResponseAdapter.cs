using System;
using System.Collections;

namespace ViviPlusTT.Adapter.Interface
{
    public interface IResponseAdapter
    {
        Type RecordType();    

        IEnumerable MappingRecords(object[] data);        
    }
}