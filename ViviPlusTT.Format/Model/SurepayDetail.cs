using System;
using FileHelpers;


namespace ViviPlusTT.Format.Model
{
    [FixedLengthRecord(FixedMode.ExactLength)]
    public class SurepayDetail
    {

        [FieldFixedLength(25)]
        public string OptField_1;

        [FieldFixedLength(10)]
        [FieldConverter(typeof(int))]
        public string CustomerId;

        [FieldFixedLength(10)]
        [FieldConverter(typeof(int))]
        public int InvoiceNumber;
                
        [FieldFixedLength(13)]
        [FieldConverter(typeof(FieldConverter.TwoDecimalConverter))]
        public decimal Amount;

        [FieldFixedLength(150)]
        public string OptField_3;
                
    }



}



