using System;
using System.IO;
using System.Text;

namespace ViviPlusTT.Infrastructure.Service
{
    public class IbsXmlGenerator
    {
        public string xmlFile { get; protected set; }
        private string _script;
        private Data.Response _header;

        public void CreateFile(Data.Response header)
        {

            _header = header;

            GenerateScript();

            GenerateXmlName();

            SaveFile();
        }

        internal void GenerateScript()
        {
            
            var sb = new StringBuilder();
            string reason = "00", creditcard = "0", reference = "", 
                   depositDate = string.Format("{0}T{1}", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"));

            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                   "<PaymentImportFile xmlns=\"http://ibs.entriq.net/Payment\" xmlns:i=\"http://www.w3.org/2001/XMLSchema/instance\">\n" +
                   "<PaymentImportFileHeader>\n" +
                         "<BankImportFormat>" + _header.Format.IbsFormatId + "</BankImportFormat>\n" +
                         "<TotalPaymentAmount>" + _header.TotalConverted + "</TotalPaymentAmount>\n" +
                         "<TotalPayments>" + _header.ResponseData.Count + "</TotalPayments>\n" +
                   "</PaymentImportFileHeader>\n");

            foreach (var item in _header.ResponseData)
            {
                sb.Append("<PaymentImport>\n" +
                           "<Amount>" + item.AmountConverted + "</Amount>\n" +
                           "<BankAcc_Number>" + item.Account + "</BankAcc_Number>\n" +
                           "<BankReasonCode>" + reason + "</BankReasonCode>\n" +
                           "<CreditCard_Number>" + creditcard + "</CreditCard_Number>\n" +
                           "<CustomerId>" + item.CustomerId + "</CustomerId>\n" +
                           "<Customer_Reference_Numbr>" + reference + "</Customer_Reference_Numbr>\n" +
                           "<DepositDate>" + depositDate + "</DepositDate>\n" +
                           "<Extended></Extended>\n" +
                           "<InvoiceNumber>" + item.InvoiceNumber + "</InvoiceNumber>\n" +
                           "<Memo1> </Memo1>\n" +
                           "<Memo2> </Memo2>\n" +
                           "<Memo3> </Memo3>\n" +
                           "<Memo4> </Memo4>\n" +
                           "<Payment_Reference_Nr> </Payment_Reference_Nr>\n" +
                           "<Receipt_Number> </Receipt_Number>\n" +
                      "</PaymentImport>\n");
            }

            sb.Append("</PaymentImportFile>");

            _script = sb.ToString();
        }

        internal void GenerateXmlName()
        {
            var db = new Data.ViviPlusTTEntities();
            
            xmlFile = Path.Combine(_header.Format.OutputPath, string.Format("{0}_{1}_{2}.xml", _header.Format.Name.Replace(' ', '_'), _header.Id, DateTime.Now.ToString("ddMMyyyy")));
        }
        internal void SaveFile()
        {
            using (StreamWriter sw = File.CreateText(xmlFile))
            {
                sw.WriteLine(_script);
            }
        }


    }
}
