
CREATE PROCEDURE EXAMPLE

AS
BEGIN 
DECLARE @ddd int

SET @ddd = (SELECT COUNT(*) FROM tbProdutoComanda WHERE Status = NULL and ComandaID = 100 and ProdutoID = 1 )

IF (@ddd >0)
    BEGIN
        UPDATE tbProdutoComanda
        SET Quantidade = Quantidade+4, ValorTotal = (Quantidade+4)*(SELECT Preco
                                                        FROM tbProduto
                                                        WHERE ProdutoID = 1)
        WHERE ComandaID = 100 and ProdutoID = 1;
    END

ELSE IF (@ddd = 0)
    BEGIN
        INSERT INTO tbProdutoComanda (ComandaID, ProdutoID, Quantidade, ValorTotal)
        VALUES (100, 1, 4, (SELECT Preco*4
                            FROM tbProduto
                            WHERE ProdutoID = 1)
                            )
        UPDATE tbComanda
        SET Situacao = 'Ativa'
        WHERE ID = 100
    END

END

DROP PROCEDURE EXAMPLE

EXEC EXAMPLE;

