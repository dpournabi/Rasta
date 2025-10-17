using Application.Interfaces;
using Infrastructure;
using Minio;
using Minio.DataModel.Args;
using System.IO;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Application.Implementations
{
    internal class MinIoHelperService : IMinIoHelperService
    {
        private readonly IMinioClient minioClient;
        public MinIoHelperService(IMinioClient _MinioClient)
        {
            this.minioClient = _MinioClient;
        }

        public async ValueTask<int> CalculateBucketSize(string bucketId)
        {
            int sum = 0;
            bool exists = await this.minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketId));
            if (!exists) 
            {
                return 0;
            }
            var objs = this.minioClient.ListObjectsAsync(new ListObjectsArgs().WithBucket(bucketId));
            sum = await objs.Sum(i => (int)i.Size).DistinctUntilChanged().Take(1).ToTask();
            return sum;
        }

        public async ValueTask<MemoryStream> Download(string bucketId, string fileName)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                try
                {
                    var result = await minioClient.GetObjectAsync(
                        new GetObjectArgs()
                            .WithBucket(bucketId)
                            .WithObject(fileName)
                            .WithCallbackStream(stream =>
                            {
                                stream.CopyTo(memoryStream);
                            })
                    );

                    return memoryStream;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async ValueTask<string> Upload(MemoryStream stream, ArchiveItem item)
        {
            try
            {
                string bucketId = $"bucket{item.CategoryId}";
                await this.CheckOrCreateBucket(bucketId);
                var args = new PutObjectArgs();
                args.WithStreamData(stream);
                args.WithBucket(bucketId);
                args.WithContentType(item.FileMimeType);
                args.WithObject(item.Title);
                args.WithObjectSize(stream.Length);
                var x = await this.minioClient.PutObjectAsync(args);

                return $"{bucketId}/{x.ObjectName}";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task CheckOrCreateBucket(string bucketId)
        {
            bool exists = await this.minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketId));
            if (!exists)
            {
                await this.minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketId));
            }
        }
    }
}
