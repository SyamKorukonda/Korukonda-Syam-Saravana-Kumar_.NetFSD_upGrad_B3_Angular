CREATE DATABASE BankAppDb;

USE BankAppDb;
CREATE PROCEDURE TransferFunds
    @FromAccount INT,
    @ToAccount INT,
    @Amount DECIMAL(18,2)
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        DECLARE @ExistingBalance DECIMAL(18,2);

        SELECT @ExistingBalance = Balance
        FROM Accounts
        WHERE AccountID = @FromAccount;

        IF @ExistingBalance < @Amount
        BEGIN
            RAISERROR('Insufficient Balance',16,1);
        END

        UPDATE Accounts
        SET Balance = Balance - @Amount
        WHERE AccountID = @FromAccount;

        UPDATE Accounts
        SET Balance = Balance + @Amount
        WHERE AccountID = @ToAccount;

        INSERT INTO FundTransferLog (FromAccount, ToAccount, Amount, TransferDate, Status)
        VALUES (@FromAccount, @ToAccount, @Amount, GETDATE(), 'Success');

        COMMIT;
        PRINT 'Fund transfer completed successfully';
    END TRY

    BEGIN CATCH
        ROLLBACK;

        INSERT INTO FundTransferLog (FromAccount, ToAccount, Amount, TransferDate, Status)
        VALUES (@FromAccount, @ToAccount, @Amount, GETDATE(), 'Failed: ' + ERROR_MESSAGE());

        PRINT 'Fund transfer failed: ' + ERROR_MESSAGE();
    END CATCH
END

EXEC TransferFunds 102,103,6500.00;

EXEC TransferFunds 102,103,6500.00;