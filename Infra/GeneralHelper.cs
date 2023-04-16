using System.Reflection;

namespace DriveOfCity.Infra
{
    public static class GeneralHelper
    {

        public static void CopiarObjeto<T>(T objetoOriginal, ref T objetoDestino) where T : class
        {
            PropertyInfo[] propriedades = objetoOriginal.GetType().GetProperties();
            foreach (PropertyInfo propriedade in propriedades)
            {
                if (propriedade.CanWrite)
                {
                    object valor = propriedade.GetValue(objetoOriginal, null);
                    propriedade.SetValue(objetoDestino, valor, null);
                }
            }
        }
    }
}
