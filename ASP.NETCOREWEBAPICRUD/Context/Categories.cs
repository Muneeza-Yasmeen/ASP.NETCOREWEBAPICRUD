using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ASP.NETCOREWEBAPICRUD.Context
{
    public class Categories
    {
        [Key]
        public int CategoryId {  get; set; }
     public string  Name { get; set; }
    }
}
