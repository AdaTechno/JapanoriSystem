SELECT [ProdutoID], SUM(Quantidade)*
								(SELECT [tbProduto].[Preco] 
								FROM [tbProduto] 
								WHERE [tbProduto].[ProdutoID] = [tbProdutoComanda].[ProdutoID]),
SUM(Quantidade)
FROM [tbProdutoComanda]
WHERE [ComandaID] = 100
AND [Status] IS NULL
GROUP BY [ProdutoID]