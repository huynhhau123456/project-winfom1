CREATE TRIGGER trg_UpdateProductQuantity
ON Orders
AFTER INSERT
AS
BEGIN
    -- Update the product quantity in the Product table after an order is inserted
    UPDATE p
    SET p.Quantity = p.Quantity - i.Quantity
    FROM Product p
    INNER JOIN inserted i ON p.BranchID = i.BranchID AND p.ProductName = i.ProductName;
    
    -- Check if any products or branches do not exist
    IF EXISTS (
        SELECT *
        FROM inserted i
        LEFT JOIN Product p ON p.BranchID = i.BranchID AND p.ProductName = i.ProductName
        WHERE p.ProductID IS NULL OR p.BranchID IS NULL
    )
    BEGIN
        -- Raise an error message indicating that the product or branch does not exist
        RAISERROR('Product or branch does not exist.', 16, 1);
        ROLLBACK TRANSACTION; -- Optional: Rollback the transaction if desired
    END;
END;
