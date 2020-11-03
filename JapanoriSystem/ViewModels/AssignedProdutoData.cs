using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JapanoriSystem.ViewModels
{
    public class AssignedProdutoData
    {
        public int ProdutoID { get; set; }
        public string Nome { get; set; }
        public string Desc { get; set; }
        public double Preco { get; set; }
        public bool Assigned { get; set; }
    }
}