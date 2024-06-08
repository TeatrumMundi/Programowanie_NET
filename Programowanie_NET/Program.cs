namespace Programowanie_NET
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Samochod s1 = new Samochod("Fiat", "126p", 2, 650, 6.0);
            Samochod s2 = new Samochod("Syrena", "105", 2, 800, 7.6);
            
            Garaz g1 = new Garaz("ul. Garażowa 1", 1);

            g1.WprowadzSamochod(s1);
            g1.WyprowadzSamochod();
            g1.WprowadzSamochod(s2);

            g1.WypiszInfo();
        }
    }
}