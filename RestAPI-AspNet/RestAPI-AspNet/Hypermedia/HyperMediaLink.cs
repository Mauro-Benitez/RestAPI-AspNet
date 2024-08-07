using System.Text;

namespace RestAPI_AspNet.Hypermedia
{

    public class HyperMediaLink
    {




        public string Rel { get; set; }
        private string href { get; set; }

        public string Href
        {

            //cria um bloqueio (lock) para garantir a segurança de threads ao acessar a propriedade href
            //StringBuilder para substituir "%2F" por "/", o que é uma forma de garantir que as barras sejam corretamente representadas no URL.
            get
            {
                object _lock = new object();

                lock (_lock)
                {
                    StringBuilder sb = new StringBuilder(href);
                    return sb.Replace("%2F", "/").ToString();

                }

            }

            //simplesmente atribui o valor à variável href
            set
            {
                href = value;

            }

        }

        public string Type { get; set; }

        public string Action { get; set; }

    }
}
