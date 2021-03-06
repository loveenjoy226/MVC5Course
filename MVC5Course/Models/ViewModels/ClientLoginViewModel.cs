﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    public class ClientLoginViewModel
    {
        [Required]
        [StringLength(10, ErrorMessage = "{0}最大不得超過{1}個字元")] //{0}:欄位名稱 {1}:參數值\
        [DisplayName("名")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "{0}最大不得超過{1}個字元")]
        [DisplayName("中間名")]
        [DataType(DataType.Password)]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "{0}最大不得超過{1}個字元")]
        [DisplayName("姓")]
        public string LastName { get; set; }
        [DisplayName("生日")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]  //ApplyFormatInEditMode:使Edit時也套用此格式
        [DataType(DataType.Date)]   //現成日期選單 only Chrome use
        public Nullable<System.DateTime> DateOfBirth { get; set; }
    }
}