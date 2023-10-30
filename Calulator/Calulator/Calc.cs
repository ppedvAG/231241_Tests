namespace Calulator
{
    public class Calc
    {
        public int Sum(int a, int b)
        {
            return checked(a + b);
        }

        public int Minus(int a, int b)
        {
            return checked(a - b);
        }
    }
}
