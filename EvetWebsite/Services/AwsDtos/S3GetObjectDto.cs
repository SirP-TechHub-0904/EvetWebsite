using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvetWebsite.Services.AwsDtos
{
    public class S3GetObjectDto
    {
        public string? Key { get; set; }
        public string? Bucket { get; set; }
    }
}
