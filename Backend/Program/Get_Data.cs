namespace API
{
    static public class Products
    {
        private static char[] products_members = ['a','b','c'];
        public static char[] Get()
        {
            return products_members;
        }
    }
}