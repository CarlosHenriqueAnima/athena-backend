using YamlDotNet.Serialization;

namespace AthenasAcademy.Services.Core.Configurations.Credentials
{
    public static class ReadCredentials
    {
        public static Credentials GetCredentials()
        {
            try
            {

            var deserializer = new DeserializerBuilder().Build();

            using (var reader = new StreamReader(@"./Secrets.yaml"))
            {
                return deserializer.Deserialize<Credentials>(reader);
            }
            }
            catch(Exception ex) 
            {
                throw new FileLoadException($"Não foi possível obter as credenciais do sistema: {ex}");
            }

        }
    }
}
