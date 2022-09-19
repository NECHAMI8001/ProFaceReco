using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StudentTBLModel
    {
        public string Student_ID { get; set; }
        public string Student_Name { get; set; }
        public string Student_Image { get; set; }
        public Nullable<int> Student_Grade_Code { get; set; }
        public Byte[] DistancePoint { get; set; }
        public string DistPoint { get; set; }

    }
}
