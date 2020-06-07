using System;
using System.Collections.Generic;


namespace ViviPlusTT.Infrastructure.Repository
{
    public interface IResponseRepository
    {
        Model.FoundFileResult FindFile();

        Data.Response SaveResponse( string user);

        void SaveResponseData(List<Data.ResponseData> responseData);

        void UpdateResponse(string xmlFile);

        
    }
}
