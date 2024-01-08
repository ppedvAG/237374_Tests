

namespace TddBank
{
    //  Bankkonto
    //- Kontostand abfragen
    //- Betrag einzahlen(nicht Negativ)
    //- Betrag abheben(nicht Negativ)
    //	- Darf nicht unter 0 fallen
    //- Neues Konto hat 0 als Kontostand

    public class BankAccount
    {
        public decimal Balance { get; private set; }

        public void Deposit(decimal v)
        {
            if (v <= 0)
                throw new ArgumentException();

            Balance += v;
        }

        public void Withdraw(decimal v)
        {
            if (v <= 0)
                throw new ArgumentException();
            if (v > Balance)
                throw new InvalidOperationException();

            Balance -= v;
        }
    }
}
