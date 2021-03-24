using System;
using System.ComponentModel.DataAnnotations;

namespace genics.Models
{
    public class RequestResponse<T> 
    {       
         public string Message { get; set; }
         public bool Success { get; set; }
         public T Data { get; set; }

    }

}
