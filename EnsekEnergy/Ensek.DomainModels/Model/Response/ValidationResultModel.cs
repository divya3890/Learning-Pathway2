using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensek.DomainModels.Model.Response
{
    public class ValidationResultModel
    {
        public List<string> Errors { get; set; }
        public ValidationResultModel()
        {
            Errors = new List<string>();
        }

        public bool IsValid
        {
            get
            {
                if (Errors != null)
                {
                    return Errors.Count() == 0;
                }

                return true;
            }
        }
    }
}
