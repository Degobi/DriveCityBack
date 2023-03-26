namespace DriveOfCity.Infra
{
    public class GenerateHelper
    {
        public static void CopyProperties<T>(T origin, T destiny)
        {
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    var value = property.GetValue(origin);
                    property.SetValue(destiny, value);
                }
            }
        }
    }
}
