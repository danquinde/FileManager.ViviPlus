using System;
using System.Collections.Generic;
using ViviPlusTT.Infrastructure.Repository;
using ViviPlusTT.Infrastructure.Data;
using ViviPlusTT.Adapter;


namespace ViviPlusTT.Core.Background
{
    public class WatcherService
    {
        private readonly IResponseRepository _responseRepository;

        public WatcherService(IResponseRepository responseRepository)
        {
            _responseRepository = responseRepository;

        }

        public string GenerateResponse()
        {
            var user_name = "processAutomaticUser";
            

            var file_result = _responseRepository.FindFile();

            if (file_result == null) return "Process didn't find files.";


            var response = _responseRepository.SaveResponse(user_name);


            var _fileReader = new IOFactory(file_result.Format.IOClassName);

            var read_file_list = (List<ResponseData>)_fileReader.ReadFlatFile(file_result.FileName);
            
            _responseRepository.SaveResponseData(read_file_list);
            

            var _xmlGenerator = new Infrastructure.Service.IbsXmlGenerator();

            _xmlGenerator.CreateFile(response);
            
            _responseRepository.UpdateResponse(_xmlGenerator.xmlFile);


            return "Ok";
        }

    }
}
