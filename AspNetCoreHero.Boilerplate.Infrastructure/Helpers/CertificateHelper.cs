using System.Security.Cryptography.X509Certificates;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Helpers
{
    public class CertificateHelper
    {
        //public static X509Certificate2 GetSystemCertificateBySubjectName(StoreName storeName, StoreLocation storeLocation, string subjectName)
        //{
        //    return GetSystemCertificate(storeName, storeLocation, X509FindType.FindBySubjectName, subjectName);
        //}
        //public static X509Certificate2 GetSystemCertificateByThumbprint(StoreName storeName, StoreLocation storeLocation, string thumbprint)
        //{
        //    return GetSystemCertificate(storeName, storeLocation, X509FindType.FindByThumbprint, thumbprint);
        //}
        //public static X509Certificate2 GetSystemCertificateBySerialNumber(StoreName storeName, StoreLocation storeLocation, string serialNumber)
        //{
        //    return GetSystemCertificate(storeName, storeLocation, X509FindType.FindBySerialNumber, serialNumber);
        //}
        public static X509Certificate2 GetSystemCertificate(StoreName storeName, StoreLocation storeLocation, X509FindType findType, string findValue)
        {
            X509Certificate2 certificate = null;
            try
            {
                X509Store store = new X509Store(storeName, storeLocation);
                store.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection certs = store.Certificates.Find(findType, findValue, true);
                store.Close();
                if (certs.Count > 0)
                    certificate = certs[0];
            }
            catch
            {
                certificate = null;
            }

            return certificate;

        }

        public static X509Certificate2Collection GetX509Certificates()
        {
            return GetX509Certificates(StoreName.My, StoreLocation.LocalMachine);
        }
        public static X509Certificate2Collection GetX509Certificates(StoreName storeName, StoreLocation storeLocation)
        {
            X509Store store = new X509Store(storeName, storeLocation);
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certs = store.Certificates;
            store.Close();
            return certs;
        }
        public static List<X509Certificate> GetSystemCertificateList(StoreName storeName, StoreLocation storeLocation)
        {
            List<X509Certificate> certificates = new List<X509Certificate>();
            try
            {
                X509Store store = new X509Store(storeName, storeLocation);
                store.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection certs = store.Certificates;
                store.Close();
                foreach (var cert in certs)
                {
                    certificates.Add(cert);
                }
            }
            catch
            {

            }
            return certificates;
        }

    }
}
