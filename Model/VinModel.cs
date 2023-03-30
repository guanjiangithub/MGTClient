using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGTClient.Model
{
    public class VinModel
    {
        public string Name { get; set; }

        public int ChannelHandle { get; set; }

        public double Value { get; set; }

        public uint Dot { get; set; }

        public bool IsWaitZero { get; set; } = true;

        public double Zero { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
