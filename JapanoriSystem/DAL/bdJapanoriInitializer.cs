using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using JapanoriSystem.Models;

namespace JapanoriSystem.DAL
{
    /*public class bdJapanoriInitializer : DropCreateDatabaseIfModelChanges<bdJapanoriContext>
    {
        protected override void Seed(bdJapanoriContext context)
        {
            // Insert de Comandas no banco de dados
            var comandas = new List<Comanda>
            {
            new Comanda{ID=1000,Situacao="Ativa",Status="On"},
            new Comanda{ID=1010,Situacao="Ativa",Status="On"},
            new Comanda{ID=1020,Situacao="Ativa",Status="On"},
            new Comanda{ID=1030,Situacao="Ativa",Status="On"},
            new Comanda{ID=1040,Situacao="Vazia",Status="On"},
            new Comanda{ID=1050,Situacao="Vazia",Status="On"},
            new Comanda{ID=1060,Situacao="Vazia",Status="On"},
            };
            comandas.ForEach(s => context.tbComanda.Add(s));
            context.SaveChanges();

            // Insert de Produtos no banco de dados
            var produtos = new List<Produto>
            {
            new Produto{Nome="Combo Sushi 40 Peças",Desc="15 Uramaki, 15 Hossomaki, 10 Hot Rolls",Preco=40.00,Status="On"}, //ID = 1
            new Produto{Nome="Combo Sushi 20 Peças",Desc="15 Uramaki, 5 Hot Rolls",Preco=25.00,Status="On"}, //ID = 2
            new Produto{Nome="Coca-Cola Lata 350ml",Desc="Coca-Cola Lata 350ml",Preco=6.00,Status="On"}, //ID = 3
            };
            produtos.ForEach(s => context.tbProduto.Add(s));
            context.SaveChanges();

            //Insert de produtos nas comandas no banco de dados
            var comandaprodutos = new List<ComandaProduto>
            {
                new ComandaProduto{ComandaID=1000,ProdutoID=1},
                new ComandaProduto{ComandaID=1000,ProdutoID=3},
                new ComandaProduto{ComandaID=1010,ProdutoID=1},
                new ComandaProduto{ComandaID=1010,ProdutoID=2},
                new ComandaProduto{ComandaID=1020,ProdutoID=1},
                new ComandaProduto{ComandaID=1030,ProdutoID=2},
                new ComandaProduto{ComandaID=1040},
                new ComandaProduto{ComandaID=1050},
                new ComandaProduto{ComandaID=1060}
            };
            comandaprodutos.ForEach(s => context.tbComandaProduto.Add(s));
            context.SaveChanges();

            // Insert de Itens do Estoque no banco de dados
            var itens = new List<Estoque>
            {
            new Estoque{Nome="Coca-Cola Lata 350ml",Quantidade=50,TipoQuantidade=TipoQuantidade.Unidades,UltimoCarregamento=DateTime.Parse("2020-10-26"),Status="On"},
            new Estoque{Nome="Salmão",Quantidade=30,TipoQuantidade=TipoQuantidade.Quilos,UltimoCarregamento=DateTime.Parse("2020-10-26"),Status="On"},
            new Estoque{Nome="Alga Marinha",Quantidade=10,TipoQuantidade=TipoQuantidade.Quilos,UltimoCarregamento=DateTime.Parse("2020-10-26"),Status="On"},
            new Estoque{Nome="Cream Cheese",Quantidade=5,TipoQuantidade=TipoQuantidade.Litros,UltimoCarregamento=DateTime.Parse("2020-10-26"),Status="On"}
            };
            itens.ForEach(s => context.tbEstoque.Add(s));
            context.SaveChanges();

            // Insert de Itens nos Produtos
            var produtoitens = new List<ProdutoEstoque>
            {
                new ProdutoEstoque{ProdutoID=1,EstoqueID=2},
                new ProdutoEstoque{ProdutoID=1,EstoqueID=3},
                new ProdutoEstoque{ProdutoID=1,EstoqueID=4},
                new ProdutoEstoque{ProdutoID=2,EstoqueID=2},
                new ProdutoEstoque{ProdutoID=2,EstoqueID=3},
                new ProdutoEstoque{ProdutoID=2,EstoqueID=4},
                new ProdutoEstoque{ProdutoID=3,EstoqueID=1},
            };
            produtoitens.ForEach(s => context.tbProdutoEstoque.Add(s));
            context.SaveChanges();

            // Insert de Funcionários
            var funcionarios = new List<Funcionario>
            {
                new Funcionario{FuncionarioID=1450,
                    Nome="Gustavo",
                    Sobrenome="Pott Delfino",
                    Cargo="Gerente",
                    CPF="111.222.333-50",
                    Endereco="Av. Raimundo, 555",
                    Cep="33300-123",
                    EmailCorp="gustavo@japanori.com.br",
                    Senha="japanori123",
                    Perm="Admin",
                    Status="On"}
            };
            funcionarios.ForEach(s => context.tbFuncionario.Add(s));
            context.SaveChanges();
        }
    }*/
}