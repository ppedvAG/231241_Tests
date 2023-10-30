namespace Calulator
{
    public class IBANParser
    {
        public static (string, string) ConvertIBANToBLZUndKontonummer(string iban)
        {
            if (string.IsNullOrEmpty(iban))
            {
                throw new ArgumentException("IBAN cannot be null or empty.");
            }

            if (iban.Substring(0, 2) != "DE")
            {
                throw new ArgumentException("This function is designed for German IBANs (starting with 'DE').");
            }

            string bban = iban.Substring(4);
            bban = bban.Substring(0, 8)+bban.Substring(8) ;
            string numericIBAN = "";

            foreach (char c in bban)
            {
                if (char.IsDigit(c))
                {
                    numericIBAN += c;
                }
                else
                {
                    numericIBAN += (char)(c - 'A' + 10);
                }
            }

            string blz = numericIBAN.Substring(0, 8);
            string kontonummer = numericIBAN.Substring(8);

            return (blz, kontonummer);
        }
    }
}
