using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDemo.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Cart Cart { get; set; }
    }
}
