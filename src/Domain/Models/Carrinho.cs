using System;
using System.Collections.Generic;
using System.Linq;

namespace IntelligentCheckout.Backend.Domain.Models
{
    /// <summary>
    /// Carrinho
    /// </summary>
    public class Carrinho
    {
        /// <summary>
        /// Id do Carrinho
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// IdSessao
        /// </summary>
        public Guid IdSessao { get; set; }

        /// <summary>
        /// Itens de Compra
        /// </summary>
        public IList<ItemDeCompra> ItensDeCompra { get; set; }

        /// <summary>
        /// Quantidade de produtos no Carrinho
        /// </summary>
        public int QuantidadeDeItens => (this.ItensDeCompra?.Count).GetValueOrDefault();

        /// <summary>
        /// Valor Total do Carrinho
        /// </summary>
        public decimal ValorTotalDoCarrinho => (this.ItensDeCompra?.Sum(i => i.Produto.Preco)).GetValueOrDefault();

        /// <summary>
        /// Cria um carrinho
        /// </summary>
        public Carrinho()
            => this.ItensDeCompra = new List<ItemDeCompra>();

        /// <summary>
        /// Cria um carrinho de compra
        /// </summary>
        /// <param name="idSessao">Id da Sessão</param>
        public Carrinho(Guid idSessao)
            : this()
            => this.IdSessao = idSessao;

        /// <summary>
        /// Adiciona um item de compra. Se o produto já existir, incrementa a quantidade
        /// </summary>
        /// <param name="produto">Produto</param>
        /// <param name="quantidade">Quantidade</param>
        public void AdicionarItemDeCompra(Produto produto, int quantidade)
        {
            var novaQuantidade = quantidade;
            var itemExistente = this.ItensDeCompra.FirstOrDefault(p => p.Produto.Id == produto.Id);
            if (itemExistente != null)
            {
                novaQuantidade += itemExistente.Quantidade;
                this.ItensDeCompra.Remove(itemExistente);
            }
            this.ItensDeCompra.Add(new ItemDeCompra(produto, novaQuantidade));
        }

        /// <summary>
        /// Remove um item de compra
        /// </summary>
        /// <param name="idProduto">Id do Produto</param>
        public void RemoverItemDeCompra(Guid idProduto)
        {
            var itemExistente = this.ItensDeCompra.FirstOrDefault(p => p.Produto.Id == idProduto);
            if (itemExistente is null) return;
            this.ItensDeCompra.Remove(itemExistente);
        }

        /// <summary>
        /// Atualiza a quantidade de um Produto
        /// </summary>
        /// <param name="idProduto">Id do produto</param>
        /// <param name="quantidade">Quantidade</param>
        public void AtualizarQuantidadeDeUmProduto(Guid idProduto, int quantidade)
        {
            var itemExistente = this.ItensDeCompra.FirstOrDefault(p => p.Produto.Id == idProduto);
            if (itemExistente is null) return;

            var produto = itemExistente.Produto;
            this.ItensDeCompra.Remove(itemExistente);
            this.ItensDeCompra.Add(new ItemDeCompra(produto, quantidade));
        }
    }
}