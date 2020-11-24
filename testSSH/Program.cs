using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using WinSCP;


namespace testSSH
{
    class Program
    {
        static void Main(string[] args)
        {
            //sftp://N0vasSTAR92:Ludi1x0@sftp.eipp-portal.com:3130/Prod/EIPPIn/
            //sftp://N0vasSTAR92:Ludi1x0;fingerprint=ssh-dss-0f-0c-6d-4a-a8-70-3f-4d-3a-18-6e-9c-09-55-6b-02@sftp.eipp-portal.com:3130/Prod/EIPPIn/
            

            string remoteDirectory = "/Prod/Test/";
            string localDirectory = @"C:\temp\*.txt";
            string test = "test";
            // Setup session options
            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Sftp,
                HostName = "sftp.eipp-portal.com",
                UserName = "N0vasSTAR92",
                Password = "Ludi1x0",
                PortNumber = 3130,
                SshHostKeyFingerprint = "ssh-dss-0f-0c-6d-4a-a8-70-3f-4d-3a-18-6e-9c-09-55-6b-02"

                //Protocol = Protocol.Ftp,
                //HostName = "160.62.95.131",
                //UserName = "NOVARTISMX@edi-ftp.citi.us.gxs.com",
                //Password = "n0v4r71s18",
                //PortNumber = 21,
            };

            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);

                // Upload files
                TransferOptions transferOptions = new TransferOptions();
                transferOptions.TransferMode = TransferMode.Binary;

                TransferOperationResult transferResult;
                transferResult = session.PutFiles(localDirectory, remoteDirectory, false, transferOptions); //method to upload files

                //get files,
                //transferResult = session.GetFiles(remoteDirectory, "C:\\download\\", false, transferOptions);
                
                // Throw on any error
                transferResult.Check();

                // Print results
                foreach (TransferEventArgs transfer in transferResult.Transfers)
                {
                    Console.WriteLine("Upload of {0} succeeded", transfer.FileName);
                }
            }

            Console.ReadLine();
           


        }
    }
}
