create procedure ventas_grabar
(
	@totalImporte decimal(10,0),
	@totalDescuento decimal(10,0),
	@total decimal(10,0),
	@usuario int
)

as begin

INSERT INTO Venta(totalImporte,totalDescuento,total,usuario,estado) 

VALUES (@TotalImporte,@totalDescuento,@total,@usuario,0)
 
END

