using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    public class ProductBatchUpdateViewModel:IValidatableObject
    {//ViewModel的例外 沒執行Model Bilding時才會執行 so沒有執行Model.State 並不會丟例外 
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public Nullable<decimal> Price { get; set; }
        [Required]
        public Nullable<bool> Active { get; set; }
        [Required]
        public Nullable<decimal> Stock { get; set; }

        //實作例外
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(this.Stock<100&&this.Price>20)
            {
                yield return new ValidationResult("庫存與商品金額的條件錯誤", new string[] { "Price" });
            }
        }
    }
}