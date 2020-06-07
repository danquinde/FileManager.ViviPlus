using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using ViviPlusTT.Infrastructure.Data;
using ViviPlusTT.Infrastructure.Model;

namespace ViviPlusTT.Infrastructure.Repository
{
    public class ResponseRepository : IResponseRepository
    {        
        private Response _response;
        private int _formatId;
        private string _fileName;
        
        public FoundFileResult FindFile()
        {
            var admitedFiles = new string[] { ".txt", ".xlsx" };

            var db = new ViviPlusTTEntities();

            var formats = db.Format.Where(x => x.IsActive).ToList();

            foreach (var frm in formats)
            {
                var files = Directory.GetFiles(frm.InputPath);
                if (files != null && !files.Length.Equals(0))
                {
                    foreach (var fl in files)
                    {
                        var fileName = Path.GetFileName(fl);

                        if (admitedFiles.Contains(Path.GetExtension(fileName).ToLower()) && fileName.ToLower().Contains(frm.InputPattern.ToLower()))
                        {                            
                            _formatId = frm.Id;
                            _fileName = fileName;
                            return new FoundFileResult() { Format = frm, FileName = fileName };
                        }
                    }
                }
            }

            return null;
        }

        public Response SaveResponse(string user)
        {
            var response = new Response()
            {   
                ProcessDate = DateTime.Now.Date,
                Status = "Created",
                FormatId = _formatId,
                FileName = _fileName,
                RegisteredBy = user,
                StartDate = DateTime.Now,
                IsActive = true
            };

            var db = new ViviPlusTTEntities();
            db.Response.Add(response);
            db.SaveChanges();

            _response = response;

            return response;
        }

        public void SaveResponseData(List<ResponseData> responseData)
        {            
            var formatFormula = responseData.First().Response.Format.Convertion.Formula;

            foreach (var rd in responseData)
            {
                var amountConverted = AmountConverter(string.Format(formatFormula, rd.Account));

                rd.ResponseId = _response.Id;
                rd.AmountConverted = amountConverted;
            }

            Service.BulkCopyEntities.BulkInsert(new ViviPlusTTEntities(), responseData);            
        }

        public void UpdateResponse(string xmlFile)
        {
            var db = new ViviPlusTTEntities();

            var responseData = db.ResponseData.Where(x => x.ResponseId == _response.Id);

            var rs = db.Response.Where(x => x.Id == _response.Id).First();

            rs.Records = (short)responseData.Count();
            rs.Total = responseData.Sum(x => x.Amount);
            rs.TotalConverted = responseData.Sum(x => x.AmountConverted);
            rs.XMLGenerated = xmlFile;
            rs.Status = "Successful";
            rs.EndDate = DateTime.Now;

            db.SaveChanges();
        }


        private decimal AmountConverter(string formula)
        {
            var execute = new System.Data.DataTable().Compute(formula, null);
            var result = decimal.Parse(execute.ToString());
            return Math.Round(result, 2, MidpointRounding.AwayFromZero);
        }


    }
}
