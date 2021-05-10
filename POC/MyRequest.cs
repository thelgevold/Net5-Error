using System;
using System.Collections.Generic;

namespace POC
{
    public class MyRequest
    {
        public string[] Ids {get; set;}
        public string Environment {get;set;}
       
        public string Namespace {get; set;}
        public string CurrencyCode {get;set;}
        public DateTime Date1 {get;set;}
        public DateTime Date2 {get;set;}
        public Dictionary<string, string> BuildVersions {get;set;}
    }
}