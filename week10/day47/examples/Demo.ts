class Account
{
    private _balance:number=0;

    get balance()
    {
        return this._balance;
    }
    
    set deposit(amount:number)
    {
        if(amount>0)
        {
            this._balance+=amount;
        }
        else{
            console.log("amount cannot be -ve or 0");
        }
    }
}
const acc=new Account()
    acc.deposit=5005;
    console.log(acc.balance);

