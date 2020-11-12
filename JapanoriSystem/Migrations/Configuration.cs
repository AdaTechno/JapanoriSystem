namespace JapanoriSystem.Migrations
{
    using JapanoriSystem.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JapanoriSystem.DAL.bdJapanoriContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.bdJapanoriContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //      Initialize Data
            /*var funcionarios = new List<Funcionario>
            {
                new Funcionario 
                { 
                    FuncionarioID = 1000,
                    Nome = "Gustavo",
                    Sobrenome = "Pott Delfino",
                    DataNasc = DateTime.Parse("05/10/2000"),
                    Cargo = "Gerente",
                    CPF = "444.000.333-90",
                    Endereco = "Av. Brasil",
                    NumeroEnd = "1800",
                    Cep = "55500-888",
                    DataContratacao = DateTime.Parse("20/10/2020"),
                    EmailCorp = "gustavo.pott@japanori.com.br",
                    Senha = "123",
                    Perm = "Admin",
                    cStatus = "On" 
                }

            };
            funcionarios.ForEach(s => context.tbFuncionario.Add(s));
            context.SaveChanges();

            var comandas = new List<Comanda>
            {
                new Comanda 
                {
                    ID = 1000,
                    Situacao = "Vazia",
                    cStatus = "On"
                }
            };
            comandas.ForEach(s => context.tbComanda.Add(s));
            context.SaveChanges();

            var produtos = new List<Produto>
            {
                new Produto
                {
                    Nome = "Coca-Cola 350ml Lata",
                    cDesc = "Bebida",
                    Preco = 5.00,
                    cStatus = "On",
                }
            };
            produtos.ForEach(s => context.tbProduto.Add(s));
            context.SaveChanges();

            var itensEstoque = new List<Estoque>
            {
                new Estoque
                {
                    Nome = "Coca-Cola 350ml Lata",
                    Quantidade = 40,
                    TipoQuantidade = "Unidades",
                    Categoria = "Bebidas",
                    UltimoCarregamento = DateTime.Parse("20/10/2020"),
                    Obs = "",
                    cStatus = "On",
                }
            };
            itensEstoque.ForEach(s => context.tbEstoque.Add(s));
            context.SaveChanges();*/

        }
    }
}
