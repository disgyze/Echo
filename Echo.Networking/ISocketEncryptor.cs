using System.IO;
using System.Net.Security;
using System.Threading.Tasks;

namespace Echo.Networking
{
    public interface ISocketEncryptor
    {
        Task<Stream> StartAsync(SslClientAuthenticationOptions? options, RemoteCertificateValidationCallback? certificateValidationCallback);
    }
}