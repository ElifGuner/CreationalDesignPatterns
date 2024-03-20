namespace Object_Pool_Hazir_Kutuphane_ASPNET_Core.Classes
{
    public class X
    {
        public int Count { get; set; }
        public void Write()
            => Console.WriteLine(Count);

        public X()
            => Console.WriteLine("X üretim maliyeti...");
        ~X()
         => Console.WriteLine("X imha maliyeti...");
    }
}
