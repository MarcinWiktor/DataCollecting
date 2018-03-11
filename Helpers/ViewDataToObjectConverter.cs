using WpfBasics.Models;

namespace WpfBasics.Helpers
{
    public static class ViewDataToObjectConverter // póżniej będzie zostanie zamieniony na binding
    {
        public static Car Create()
        {
            return new Car()
            {
            };
        }
    }
}
