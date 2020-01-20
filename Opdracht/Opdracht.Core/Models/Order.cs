using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Opdracht.Core.Models
{
    [Serializable()]
    public class Order : BaseEntity, ISerializable
    {
        public bool Status { get; set; }
        [DisplayName("Created At")]
        public DateTime CreatedAt { get; set; }
        [DisplayName("File Name")]
        public string Filename { get; set; }
        [DisplayName("File Content")]
        public string FileContent { get; set; }
        public Order()
        {

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Status", Status);
        }
        public Order(SerializationInfo info, StreamingContext context)
        {
            Status = (bool)info.GetValue("Status", typeof(bool));
        }
        public Order(bool status)
        {
            this.Status = status;
        }
    }
}
