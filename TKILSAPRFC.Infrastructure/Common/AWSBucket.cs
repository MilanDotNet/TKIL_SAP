using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TKILSAPRFC.Core.Helpers;

namespace TKILSAPRFC.Infrastructure.Common
{

    public class AWSBucket
    {
        private static string bucketName;
        //private const string BucketPath = @"https://procuresens-uat.s3.amazonaws.com/procuresens-uat/";
        private static string key; // change to your file path server Storage Path
        private static string accessKey;
        private static string secretKey;
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.APSouth1; // change to your bucket's region
        private static IAmazonS3 s3Client;

        /// <summary>
        /// Initializes a new instance of the bbl_S3Bucket class.
        /// Retrieves required configuration details from the config file.
        /// </summary>
        public static void Initialize()
        {
            bucketName = AppSettings.Current.S3BucketName.ToString();
            key = AppSettings.Current.S3BucketKey.ToString();
            accessKey = AppSettings.Current.S3BucketAccessKey.ToString();
            secretKey = AppSettings.Current.S3BucketSecretKey.ToString();
        }


        public static dynamic AWSBucketStorage(Stream file, string keyname, string operationName)
        {
            AWSBucket.Initialize();
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.txt");
            dynamic result = null;
            try
            {
                var BucketPath = key + keyname;

                string LocalEnvironment = AppSettings.Current.LocalEnvironment.ToString();
                if (LocalEnvironment == "1")
                {
                    s3Client = new AmazonS3Client(accessKey, secretKey, bucketRegion);
                }
                else
                {
                    s3Client = new AmazonS3Client(bucketRegion);
                }

                switch (operationName)
                {
                    case "Upload":
                        result = UploadFile(file, BucketPath);
                        break;
                    case "Download":
                        result = DownloadFileAsync(string.Empty, BucketPath);
                        break;
                    case "List":
                        ListObjectsAsync();
                        break;
                    case "Delete":
                        result = DeleteFile(BucketPath);
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                using (FileStream fs = new FileStream(logFilePath, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " : " + "S3Bucket" + ex);
                }
                result = "Error";
                return result;
            }
            return result;
        }
        private static string UploadFile(Stream filePath, string keyName)
        {
            try
            {

                var fileTransferUtility = new TransferUtility(s3Client);
                fileTransferUtility.Upload(filePath, bucketName, (keyName));
                Console.WriteLine("Upload completed");
                return "Success";

            }
            catch (AmazonS3Exception e)
            {
                throw e;
                //  Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception ex)
            {
                throw ex;
                // Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
        }

        private static async Task<byte[]> DownloadFileAsync(string filePath, string keyName)
        {
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                GetObjectResponse response = await s3Client.GetObjectAsync(bucketName, keyName);


                using (Stream responseStream = response.ResponseStream)
                {
                    responseStream.CopyTo(memoryStream);
                }
                Console.WriteLine("Download completed");
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when reading an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when reading an object", e.Message);
            }
            return memoryStream.ToArray();
        }

        private static async Task<string> ListObjectsAsync()
        {
            ListObjectsV2Response response;
            try
            {
                ListObjectsV2Request request = new ListObjectsV2Request
                {
                    BucketName = bucketName,
                    Prefix = key + "/"// optional, to list objects under a specific "folder"
                };

                do
                {
                    response = await s3Client.ListObjectsV2Async(request);
                    foreach (S3Object entry in response.S3Objects)
                    {
                        Console.WriteLine("key = {0} size = {1}", entry.Key, entry.Size);
                    }
                    request.ContinuationToken = response.NextContinuationToken;

                } while ((bool)response.IsTruncated);

                Console.WriteLine("Listing completed");
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered ***. Message:'{0}' when listing objects", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when listing objects", e.Message);
            }
            return string.Empty;

        }

        private static string DeleteFile(string keyName)
        {
            var response = string.Empty;
            try
            {
                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                };
                var result = s3Client.DeleteObjectAsync(deleteObjectRequest);
                response = "Success";


            }
            catch (AmazonS3Exception e)
            {
                response = "Error encountered ***. Message:" + e.Message + " when deleting an object";
                return response;
                Console.WriteLine("Error encountered ***. Message:'{0}' when deleting an object", e.Message);
            }
            catch (Exception e)
            {
                response = "Unknown encountered on server. Message:" + e.Message + " when deleting an object";
                return response;
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when deleting an object", e.Message);
            }
            return response;
        }



        /// <summary>
        /// Create Zip File
        /// </summary>
        /// <param name="filesToZip"></param>
        /// <returns></returns>
        public static byte[] CreateZipFile(DataRow[] filesToZip)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var dr1 in filesToZip)
                    {
                        var fileName = dr1["FILENAME"].ToString();
                        var fileContent = AWSBucketStorage(null, dr1["UPLOADPATH"].ToString().Trim(), Enums.Download);

                        var zipEntry = zipArchive.CreateEntry(fileName, System.IO.Compression.CompressionLevel.Optimal);

                        using (var entryStream = zipEntry.Open())
                        {
                            entryStream.Write(fileContent, 0, fileContent.Length);
                        }
                    }
                }

                return memoryStream.ToArray(); // Returns a single ZIP file containing all entries
            }
        }
    }
    public class Enums
    {
        //Operation Name FileUpload S3 Bucket
        public const string Upload = "Upload";
        public const string Download = "Download";
        public const string Delete = "Delete";
        public const string List = "List";

        public const string Success = "Success";

        //FolderPath
        public const string PRAttachment = "/Documents/PRAttachment/";
        public const string POAttachment = "/Documents/POAttachment/";



    }
}
