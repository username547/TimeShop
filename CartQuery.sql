SELECT
    u.UserId,
    u.UserName,
    u.UserSurname,
    u.UserEmail,
    c.CartId,
	--c.StatusId,
    ci.CartItemId,
    ci.ProductId,
    p.ProductName,
    p.ProductPrice,
	p.ProductImage,
	ci.ProductQuantity,
    ci.ProductQuantity * p.ProductPrice AS TotalProductPrice,
	SUM(ci.ProductQuantity) OVER (PARTITION BY u.UserId) AS TotalPurchasedItems,
    SUM(ci.ProductQuantity * p.ProductPrice) OVER (PARTITION BY u.UserId, c.CartId) AS TotalCartPrice
FROM Users u
JOIN Carts c ON u.UserId = c.UserId
JOIN CartItems ci ON c.CartId = ci.CartId
JOIN Products p ON ci.ProductId = p.ProductId;

SELECT
    u.UserId,
    u.UserName,
    u.UserSurname,
    u.UserEmail,
    c.CartId,
    ci.CartItemId,
    ci.ProductId,
    p.ProductName,
    p.ProductPrice,
	p.ProductImage,
	ci.ProductQuantity,
    ci.ProductQuantity * p.ProductPrice AS TotalProductPrice,
	SUM(ci.ProductQuantity) OVER (PARTITION BY u.UserId) AS TotalPurchasedItems,
    SUM(ci.ProductQuantity * p.ProductPrice) OVER (PARTITION BY u.UserId, c.CartId) AS TotalCartPrice
FROM Users u
JOIN Carts c ON u.UserId = c.UserId
JOIN CartItems ci ON c.CartId = ci.CartId
JOIN Products p ON ci.ProductId = p.ProductId

SELECT * FROM Carts

SELECT * FROM Orders

SELECT * FROM OrderItems

