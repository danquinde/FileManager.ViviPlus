using System;
using System.Collections;
using FileHelpers;


namespace ViviPlusTT.Adapter
{
    public class IOFactory
    {
        Interface.IResponseAdapter _adapter;

        public IOFactory(string IoClassName)
        {
            var classInstance = Activator.CreateInstance(Type.GetType(IoClassName));

            _adapter = (Interface.IResponseAdapter) classInstance;            
        }

        public IEnumerable ReadFlatFile(string filePath)
        {
            var engine = new FileHelperEngine(_adapter.RecordType());
            
            object[] fileData = engine.ReadFile(filePath);

            return _adapter.MappingRecords(fileData);            
        }
    }
}
