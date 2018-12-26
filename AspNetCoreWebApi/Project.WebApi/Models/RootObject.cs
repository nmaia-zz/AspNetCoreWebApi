using System.Collections.Generic;

namespace Project.WebApi.Models
{
    public class RootObject
    {
        public int count { get; set; }
        public object next { get; set; }
        public object previous { get; set; }
        public List<Result> results { get; set; }
    }
}
