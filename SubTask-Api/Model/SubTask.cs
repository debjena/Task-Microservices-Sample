using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SubTask_Api.Model
{
    public class SubTask
    {
        public long Id { get; set; }
        [Required(ErrorMessage="Please enter sub-task.")]
        public string SubTaskDesc { get; set; }
    }
    public class CheckDateTypeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dtout;
            if(DateTime.TryParseExact(value.ToString(),"M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,out dtout))
            {
                return true;
            }
            return false;
        }
    }
}
