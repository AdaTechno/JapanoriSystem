SELECT ProdutoComandaID FROM tbProdutoComanda
WHERE ComandaID = 100

SELECT * FROM tbProdutoComanda
SELECT * FROM tbComanda
SELECT * FROM tbProdutoComandaVendas
SELECT * FROM tbVendas

DELETE FROM tbProdutoComandaVendas
DELETE FROM tbVendas
DELETE FROM tbProdutoComanda


SELECT (FIRST) FROM tbProdutoComanda
WHERE ComandaID = 100

UPDATE tbComanda
SET ValorTotal = (SELECT SUM(tbProdutoComanda.ValorTotal)
					FROM tbProdutoComanda
					WHERE cStatus = 'Ativo' AND ComandaID = 110)
WHERE ID = 100

UPDATE tbProdutoComanda
SET cStatus = 'Ativo'
WHERE ProdutoComandaID = 6

SET IDENTITY_INSERT tbVendas ON 

/* INSERINDO NA TABELA VENDAS GERANDO UM ID IDENTITY */
INSERT INTO tbVendas (VendaID, NomeFuncionario, FormaPag) VALUES ((SELECT (SELECT TOP 1 VendaID FROM tbVendas ORDER BY VendaID DESC)+1 FROM tbVendas), 'Gustavo','Débito')

INSERT INTO tbProdutoComandaVendas (ProdutoComandaID,VendaID)
SELECT ProdutoComandaID, 1 
FROM tbProdutoComanda WHERE ComandaID = 100


SELECT (SELECT TOP 1 VendaID FROM tbVendas ORDER BY VendaID DESC)+1 FROM tbVendas