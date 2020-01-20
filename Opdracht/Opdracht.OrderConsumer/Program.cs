using Opdracht.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opdracht.OrderConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderServices.DeSerializeAllOrders();
        }
    }
}
