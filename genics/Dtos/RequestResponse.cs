using System;
using System.ComponentModel.DataAnnotations;

namespace genics.Models
{
    public class RequestResponse<T> 
    {       
         public T Data {get; set;}
         public string Message { get; set; }
         public bool Success { get; set; }

    }

}
