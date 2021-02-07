using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RowEditing.Models
{
    public class CustomersViewModel
    {
        public List<Customer> Customers { get; set; }
        public Customer SelectedCustomer { get; set; }
        public string DisplayMode { get; set; }
    }
}
